using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform strengthPosition;

    private CameraController cameraReference;
    private Rigidbody playerRigidBody;
    private SpriteRenderer showPlayer;
    private CapsuleCollider playerCollider;
    private Quaternion facingDirection;  
    private GameObject savedGameObject;

    public int strengthAbilityDistance;
    public float moveSpeed, turnSpeed;
    public float gravity, jumpForce, dashForce;

    public float[] coolingTimer;
    public int[] clampValue;

    private bool onGround, jumping;
    private bool abilityCooling, strengthRayCastStart;
    private int jumpCounter, dashDirection;
    private int layerMask;

    private float moveHorizontal, moveVertical;
    private float turnHorizontal, turnVertical;

    private void Awake()
    {
        cameraReference = FindObjectOfType<CameraController>();
        playerRigidBody = GetComponent<Rigidbody>();
        showPlayer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider>();

        jumping = false;
        abilityCooling = false;
        strengthRayCastStart = false;
        jumpCounter = 0;
        dashDirection = 0;
        layerMask = 1 << 8;
    }

    private void Update()
    {
        TurningLogic();
        AbilityCheck();
    }

    private void FixedUpdate()
    {
        MovingLogic();
        GravityCheck();
    }

    private void TurningLogic()
    {
        turnHorizontal += Input.GetAxis("TurnHorizontal") * turnSpeed;
        turnVertical += Input.GetAxis("TurnVertical") * turnSpeed;
        //turnHorizontal = Mathf.Clamp(turnHorizontal, clampValue[0], clampValue[1]);
        turnVertical = Mathf.Clamp(turnVertical, clampValue[2], clampValue[3]);

        facingDirection = Quaternion.Euler(turnVertical, turnHorizontal, 0f);
        transform.rotation = facingDirection;
    }

    private void MovingLogic()
    {
        /*
        moveHorizontal = Input.GetAxis("MoveHorizontal");
        moveVertical = Input.GetAxis("MoveVertical");

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerRigidBody.velocity = transform.forward * moveHorizontal * moveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidBody.velocity = transform.right * moveVertical * moveSpeed;
        }
        */
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerRigidBody.velocity = transform.forward * moveSpeed;
            dashDirection = 0;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            playerRigidBody.velocity = -transform.forward * moveSpeed;
            dashDirection = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidBody.velocity = -transform.right * moveSpeed;
            dashDirection = 3;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidBody.velocity = transform.right * moveSpeed;
            dashDirection = 2;
        }
        else
        {
            playerRigidBody.velocity = new Vector3(0f, 0f, 0f);
            dashDirection = 0;
        }
    }

    private void GravityCheck()
    {
        if (!onGround)
        {
            if (jumping)
            {
                playerRigidBody.AddForce(new Vector3(0, gravity * (jumpCounter + 1), 0), ForceMode.Force);
            } 
            else
            {   
                cameraReference.offsetHeight = new Vector3(0, 1.5f, 0);

                playerRigidBody.AddForce(new Vector3(0, gravity * (jumpCounter + 1), 0), ForceMode.Force);
            }
        }
        
        if (jumpCounter < 2 && !jumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(JumpingLogic());
            }
        }
    }

    private void AbilityCheck()
    {
        if (!abilityCooling)
        {
            if (Input.GetButtonDown("Dash"))
            {
                StartCoroutine(DashLogic());
                StartCoroutine(CoolingLogic(false, coolingTimer[1], "dash"));
            }

            if (Input.GetButton("Strength") && !strengthRayCastStart)
            {
                StrengthRayCast();
            }
            if(Input.GetButtonUp("Strength") && savedGameObject != null)
            {
                moveSpeed /= 3;
                turnSpeed /= 3;

                StartCoroutine(CoolingLogic(false, coolingTimer[2], "strength"));

                if(strengthPosition.position.y < savedGameObject.transform.position.y)
                {
                    strengthPosition.transform.Translate(strengthPosition.position.x, savedGameObject.transform.position.y, strengthPosition.position.z);
                }

                savedGameObject.AddComponent<StrengthLogic>();                 
                savedGameObject.GetComponent<StrengthLogic>().timerBeforeDestroy = coolingTimer[2];
            }
        }
    }

    private void StrengthRayCast()
    {
        strengthRayCastStart = true;

        RaycastHit strengthRayHit;                            
        Debug.DrawLine(transform.position, strengthPosition.transform.forward * strengthAbilityDistance, Color.red, 30f);
        bool rayHit = Physics.Raycast(transform.position, strengthPosition.transform.forward * strengthAbilityDistance, out strengthRayHit, Mathf.Infinity, layerMask);

        if (rayHit)
        {
            GameObject hitGameObject = strengthRayHit.transform.gameObject;
            Debug.Log("target: " + hitGameObject.name);

            if (hitGameObject.transform.tag == "PickUp")
            {
                savedGameObject = hitGameObject;
                hitGameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            }  
        }
        else
        {
            if(savedGameObject != null)
            {
                savedGameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                savedGameObject = null; 
            }
        }

        StartCoroutine(StrengthRayCooling());
    }     

    IEnumerator CoolingLogic(bool waitingForRemoval, float waitingTime, string abilityName)
    {
        Debug.Log("cooling: " + abilityName);
        abilityCooling = true;

        yield return new WaitForSeconds(waitingTime);

        abilityCooling = false;

        if(abilityName == "strength")
        {
            moveSpeed *= 3;
            turnSpeed *= 3;
        }

        Debug.Log("cool done");
    }

    IEnumerator JumpingLogic()
    {
        jumping = true;
        jumpCounter++;

        cameraReference.offsetHeight = new Vector3(0, 6f, 0);
        
        /*
        playerRigidBody.AddForce(new Vector3(0, 0, 0), ForceMode.VelocityChange);
        playerRigidBody.AddForce(new Vector3(0, jumpForce * jumpCounter, 0), ForceMode.Acceleration);
        playerRigidBody.velocity = new Vector3(0, jumpForce * jumpCounter, 0);
        */
        for (int counter = 0; counter < 4; counter++)
        {
            playerRigidBody.AddForce(new Vector3(0, jumpForce * jumpCounter * counter, 0), ForceMode.Acceleration);
            yield return new WaitForEndOfFrame();
        }

        jumping = false;
    }

    IEnumerator DashLogic()
    {
        switch (dashDirection)
        {
            case 0:
                for (int counter = 0; counter < 4; counter++)
                {
                    playerRigidBody.AddForce(transform.forward * dashForce * 1 * counter, ForceMode.Force);
                    yield return new WaitForSeconds(.03f);
                }
                break;
            case 1:
                for (int counter = 0; counter < 4; counter++)
                {
                    playerRigidBody.AddForce(transform.forward * dashForce * -1 * counter, ForceMode.Force);
                    yield return new WaitForSeconds(.03f);
                }
                break;
            case 2:
                for (int counter = 0; counter < 4; counter++)
                {
                    playerRigidBody.AddForce(transform.right * dashForce * 1 * counter, ForceMode.Force);
                    yield return new WaitForSeconds(.03f);
                }
                break;
            case 3:
                for (int counter = 0; counter < 4; counter++)
                {
                    playerRigidBody.AddForce(transform.right * dashForce * -1 * counter, ForceMode.Force);
                    yield return new WaitForSeconds(.03f);
                }
                break;
        }
    }

    IEnumerator StrengthRayCooling()
    {
        yield return new WaitForSeconds(.15f);

        strengthRayCastStart = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            jumpCounter = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }
}
