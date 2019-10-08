using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject strengthPosition;

    private Rigidbody playerRigidBody;
    private SpriteRenderer showPlayer;
    private CapsuleCollider playerCollider;
    private Quaternion facingDirection;

    public int strengthAbilityDistance;
    public float moveSpeed, turnSpeed;
    public float gravity, jumpForce, dashForce;

    public float[] coolingTimer;
    public int[] clampValue;

    private bool onGround, jumping;
    private bool abilityCooling;
    private int jumpCounter, dashDirection, strengthWaitingCounter;

    private float moveHorizontal, moveVertical;
    private float turnHorizontal, turnVertical;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        showPlayer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider>();

        jumping = false;
        abilityCooling = false;
        jumpCounter = 0;
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
        moveHorizontal = Input.GetAxis("MoveHorizontal");
        moveVertical = Input.GetAxis("MoveVertical");

        /*
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
            dashDirection = 2;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidBody.velocity = transform.right * moveSpeed;
            dashDirection = 3;
        }
        else
        {
            playerRigidBody.velocity = new Vector3(0f, 0f, 0f);
            dashDirection = 0;
        }
    }

    private void GravityCheck()
    {
        if (!onGround && !jumping)
        {
            playerRigidBody.AddForce(new Vector3(0, gravity * (jumpCounter + 1), 0), ForceMode.Acceleration);
        }
        //jumping
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
                switch (dashDirection)
                {
                    case 0:
                        playerRigidBody.AddForce(transform.forward * dashForce * 1, ForceMode.Impulse);
                        break;
                    case 1:
                        playerRigidBody.AddForce(transform.forward * dashForce * -1, ForceMode.Impulse);
                        break;
                    case 2:
                        playerRigidBody.AddForce(transform.right * dashForce * -1, ForceMode.Impulse);
                        break;
                    case 3:
                        playerRigidBody.AddForce(transform.right * dashForce * 1, ForceMode.Impulse);
                        break;
                }

                StartCoroutine(CoolingLogic(false, coolingTimer[1], "dash"));
            }

            if (Input.GetButton("Strength"))
            {
                StartCoroutine(CoolingLogic(true, coolingTimer[2], "strength"));
                StrengthRayCast();
            }
        }
    }

    private void StrengthRayCast()
    {
        strengthWaitingCounter++;

        Vector3 rayStartPoint = transform.position;
        Vector3 rayEndPoint = transform.forward;
        Ray strengthRay = new Ray(rayStartPoint, rayEndPoint);
        RaycastHit strengthRayHit;

        Debug.DrawLine(rayStartPoint, rayEndPoint * strengthAbilityDistance, Color.red, 30f);

        bool rayHit = Physics.Raycast(strengthRay, out strengthRayHit, strengthAbilityDistance);

        if (abilityCooling)
        {
            if (rayHit)
            {
                GameObject hitGameObject = strengthRayHit.transform.gameObject;
                Debug.Log("target: " + hitGameObject.name);

                if (hitGameObject.transform.tag == "PickUp")
                {
                    hitGameObject.AddComponent<StrengthLogic>();
                    hitGameObject.GetComponent<StrengthLogic>().timerBeforeDestroy = (float)(coolingTimer[2] - .3 * strengthWaitingCounter);
                }
                else
                {
                    StartCoroutine(StrengthRayCooling());
                }
            }
            else
            {
                StartCoroutine(StrengthRayCooling());

                Debug.Log("nothing hit");

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
        */

        playerRigidBody.velocity = new Vector3(0, jumpForce * jumpCounter, 0);

        yield return new WaitForSeconds(.15f);

        jumping = false;
    }

    IEnumerator CoolingLogic(bool waitingForRemoval, float waitingTime, string abilityName)
    {
        Debug.Log("cooling: " + abilityName);
        abilityCooling = true;

        yield return new WaitForSeconds(waitingTime);

        abilityCooling = false;
        strengthWaitingCounter = 0;

        Debug.Log("cool done");
    }

    IEnumerator StrengthRayCooling()
    {
        yield return new WaitForSeconds(.3f);

        if(strengthWaitingCounter != 0)
        {
            StrengthRayCast();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
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
