using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cameraPosition, strengthPosition;
    public Transform backPosition;

    private CameraController cameraReference;
    private UIController uiControllerReference;
    private Rigidbody playerRigidBody;
    private SpriteRenderer showPlayer;
    private CapsuleCollider playerCollider;
    private Quaternion facingDirection;  
    private GameObject savedGameObject;

    public bool onGround, jumping;
    public int jumpCounter;
    public float moveSpeed, turnSpeed;
    public float gravity, jumpForce, dashForce;

    public int[] coolingTimer;
    public int[] clampValue;

    private bool abilityCooling, strengthRayCastStart;
    private int dashDirection;
    private int layerMask;

    private float moveHorizontal, moveVertical;
    private float turnHorizontal, turnVertical;

    private void Awake()
    {
        cameraReference = FindObjectOfType<CameraController>();
        uiControllerReference = FindObjectOfType<UIController>();
        playerRigidBody = GetComponent<Rigidbody>();
        showPlayer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider>();

        jumping = false;
        abilityCooling = false;
        strengthRayCastStart = false;
        jumpCounter = 0;
        dashDirection = 0;
        layerMask = LayerMask.GetMask ("rayCastObject");
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
                CoolingLogic(0, coolingTimer[1]);

                uiControllerReference.StartCoolDown(0);
            }

            if (Input.GetButton("Strength") && !strengthRayCastStart)
            {
                StrengthRayCast();
            }
            if(Input.GetButtonUp("Strength") && savedGameObject != null)
            {
                moveSpeed /= 3;
                turnSpeed /= 3;

                CoolingLogic(1, coolingTimer[2]);

                if (strengthPosition.transform.position.y < savedGameObject.transform.position.y)
                {
                    strengthPosition.transform.position = new Vector3(strengthPosition.transform.position.x, savedGameObject.transform.position.y, strengthPosition.transform.position.z);
                }

                savedGameObject.AddComponent<StrengthLogic>();                 
                savedGameObject.GetComponent<StrengthLogic>().timerBeforeDestroy = coolingTimer[2];
                uiControllerReference.StartCoolDown(1);
            }
        }
    }

    private void StrengthRayCast()
    {
        strengthRayCastStart = true;

        RaycastHit strengthRayHit;                            
        Debug.DrawLine(transform.position, strengthPosition.transform.position, Color.red, 30f);
        bool rayHit = Physics.Raycast(transform.position, strengthPosition.transform.position, out strengthRayHit, Mathf.Infinity, layerMask);

        if (rayHit)
        {
            GameObject hitGameObject = strengthRayHit.transform.gameObject;
            Debug.Log("target: " + hitGameObject.name);

            if (hitGameObject.tag == "PickUp")
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

    private void CoolingLogic(int abilityType, int waitingTime)
    {
        Debug.Log(waitingTime);
        if(waitingTime != 0)
        {
            uiControllerReference.UpdateTime(abilityType, waitingTime);

            abilityCooling = true;
            waitingTime--;  
            StartCoroutine(AbilityCooling(abilityType, waitingTime)); 
        }
        else
        {
            uiControllerReference.StopCooling(abilityType);

            abilityCooling = false;

            Debug.Log("cool done");

            if (abilityType == 1)
            {
                moveSpeed *= 3;
                turnSpeed *= 3;
            }
        }
    }

    IEnumerator JumpingLogic()
    {
        jumping = true;
        jumpCounter++;

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
        yield return new WaitForSeconds(.1f);

        strengthRayCastStart = false;
    }
    IEnumerator AbilityCooling(int abilityType, int waitingTime)
    {
        yield return new WaitForSeconds(1f);

        CoolingLogic(abilityType, waitingTime);
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
