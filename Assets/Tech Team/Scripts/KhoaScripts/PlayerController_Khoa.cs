using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Khoa : MonoBehaviour
{
    public GameObject cameraPosition, dashHelperReference;
    public Transform strengthPosition;
    public LayerMask strengthLayerMask;

    private CameraController_Khoa cameraReference;
    private UIController_Khoa uiControllerReference;
    private GameObject savedGameObject;

    private Rigidbody playerRigidBody;
    private SpriteRenderer showPlayer;
    private CapsuleCollider playerCollider;
    private Quaternion facingDirection;

    public bool canMove, onGround, jumping, strengthEnd;
    public int jumpCounter, maxJumpCounter, glider;
    public float moveSpeed, turnSpeed, dashMultiplier, maxDashMultiplier;
    public float gravity, jumpForce, dashForce, maxElementCounter, canGlide;

    public int[] clampValue;
    public float[] elementalList;

    private bool abilityCooling, strengthRayCastStart, gliding;
    private bool usingStrength;
    private float savedMoveSpeed, savedTurnSpeed, savedGravity;

    private float moveHorizontal, moveVertical;
    private float turnHorizontal, turnVertical;

    private void Awake()
    {
        cameraReference = FindObjectOfType<CameraController_Khoa>();
        uiControllerReference = FindObjectOfType<UIController_Khoa>();

        playerRigidBody = GetComponent<Rigidbody>();
        showPlayer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider>();

        canMove = true;
        jumping = false;
        abilityCooling = false;
        strengthRayCastStart = false;
        strengthEnd = false;
        jumpCounter = 0;
        dashMultiplier = 3;
        savedMoveSpeed = moveSpeed;
        savedTurnSpeed = turnSpeed;
        savedGravity = gravity;
    }

    private void Update()
    {
        TurningLogic();
        InputCheck();
    }

    private void FixedUpdate()
    {
        MovingLogic();
        JumpingCheck();
    }

    private void MovingLogic()
    {
        moveHorizontal = Input.GetAxis("MoveHorizontal");
        moveVertical = Input.GetAxis("MoveVertical");

        if (!usingStrength)
        {
            if (moveHorizontal != 0)
            {
                moveSpeed = savedMoveSpeed / 3 * 2;
            }
            else if (moveVertical < 0)
            {
                moveSpeed = savedMoveSpeed / 2;
            }
            else
            {
                moveSpeed = savedMoveSpeed;
            }
        }
        else
        {
            if (moveHorizontal < 0)
            {
                moveSpeed = savedMoveSpeed / 9 * 2;
            }
            else if (moveVertical < 0)
            {
                moveSpeed = savedMoveSpeed / 4;
            }
            else
            {
                moveSpeed = savedMoveSpeed / 2;
            }
        }

        if(canMove)
        {
            playerRigidBody.velocity = new Vector3(moveHorizontal, 0, moveVertical) * moveSpeed;
        }
    }

    private void TurningLogic()
    {
        turnHorizontal += Input.GetAxis("TurnHorizontal") * turnSpeed;
        turnVertical += Input.GetAxis("TurnVertical") * turnSpeed;
        //turnHorizontal = Mathf.Clamp(turnHorizontal, clampValue[0], clampValue[1]);
        turnVertical = Mathf.Clamp(turnVertical, clampValue[2], clampValue[3]);

        facingDirection = Quaternion.Euler(turnVertical, turnHorizontal, 0f);
        this.transform.rotation = facingDirection;
    }

    private void JumpingCheck()
    {
        if (Input.GetButton("Jump"))
        {
            if(!onGround)
            {
                canGlide += .1f; Debug.Log(canGlide);

                if (!jumping && !gliding && canGlide > .3f && elementalList[3] > 0)
                {
                    StartCoroutine(GlidingLogic());
                }

            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            glider = 12;
            moveSpeed = savedMoveSpeed;
            turnSpeed = savedTurnSpeed;
        }

        if (!jumping)
        {
            if (Input.GetButtonDown("Jump") && jumpCounter < maxJumpCounter)
            {
                StartCoroutine(JumpingLogic());
            }

            if (!onGround)
            {
                gravity += savedGravity / glider;

                playerRigidBody.AddForce(new Vector3(0, gravity, 0), ForceMode.Force);
            }
        }
    }

    private void InputCheck()
    {
        if (!abilityCooling)
        {
            if (elementalList[3] > 0)
            {
                if (Input.GetButton("Dash"))
                {
                    DashMultiplierIncrease();
                }
                if (Input.GetButtonUp("Dash"))
                {
                    StartCoroutine(DashLogic());
                    StartCoroutine(AbilityEnd(3));
                }
            }

            if (elementalList[1] > 0)
            {
                if (Input.GetButton("Strength") && !strengthRayCastStart)
                {
                    StrengthRayCast();
                }
                if (Input.GetButtonUp("Strength") && savedGameObject != null)
                {
                    usingStrength = true;

                    StartCoroutine(AbilityEnd(1));

                    if (strengthPosition.position.y < savedGameObject.transform.position.y)
                    {
                        strengthPosition.position = new Vector3(strengthPosition.position.x, savedGameObject.transform.position.y, strengthPosition.position.z);
                    }


                    savedGameObject.AddComponent<StrengthLogic_Khoa>();
                    savedGameObject.GetComponent<StrengthLogic_Khoa>().timerBeforeDestroy = 9;
                }
            }
        }
    }

    private void DashMultiplierIncrease()
    {
        if(dashMultiplier < maxDashMultiplier)
        {
            dashMultiplier += Time.deltaTime;
        }

        uiControllerReference.DashMultiplierOn();
    }

    private void StrengthRayCast()
    {
        strengthRayCastStart = true;
        float strengthRayDistance = Vector3.Distance(transform.position, strengthPosition.position);
        Ray strengthRayCast = new Ray(transform.position, transform.forward * strengthRayDistance);
        RaycastHit strengthRayHit;

        Debug.DrawLine(strengthRayCast.origin, strengthRayCast.origin + strengthRayCast.direction * strengthRayDistance, Color.green);

        bool rayHit = Physics.Raycast(strengthRayCast, out strengthRayHit, strengthRayDistance, strengthLayerMask);

        if (rayHit)
        {
            GameObject hitGameObject = strengthRayHit.transform.gameObject;

            Debug.DrawLine(strengthRayCast.origin, strengthRayHit.point, Color.red, 3f);
            Debug.Log("target: " + hitGameObject.name);

            if (hitGameObject.tag == "PickUp")
            {
                savedGameObject = hitGameObject;
                hitGameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
        else
        {
            if (savedGameObject != null)
            {
                savedGameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                savedGameObject = null;
            }
        }

        StartCoroutine(StrengthRayCooling());
    }

    IEnumerator JumpingLogic()
    {
        //call animation here
        jumping = true;
        gliding = true;
        jumpCounter++;

        if (elementalList[3] > 0)
        {
            maxJumpCounter = 2;
        }
        else
        {
            maxJumpCounter = 1;
        }

        if (jumpCounter > 1)
        {
            elementalList[3] -= 1;
        }


        /*
        playerRigidBody.AddForce(new Vector3(0, 0, 0), ForceMode.VelocityChange);
        playerRigidBody.AddForce(new Vector3(0, jumpForce * jumpCounter, 0), ForceMode.Acceleration);
        */

        playerRigidBody.velocity = new Vector3(0, 0, 0);

        for (int counter = 0; counter < 12; counter++)
        {
            playerRigidBody.AddForce(new Vector3(0, jumpForce * jumpCounter * counter, 0), ForceMode.Acceleration);
            yield return new WaitForEndOfFrame();
        }

        jumping = false;
        gliding = false;
    }

    IEnumerator GlidingLogic()
    {
        gliding = true;

        glider = 120;
        moveSpeed = savedMoveSpeed * 3;
        turnSpeed = savedTurnSpeed / 3;
        elementalList[3] -= 1;

        yield return new WaitUntil(() => onGround);

        gliding = false;
    }

    IEnumerator DashLogic()
    {
        dashHelperReference.GetComponent<DashHelper_Khoa>().StartChecking();

        yield return new WaitUntil(() => dashHelperReference.GetComponent<DashHelper_Khoa>().canStart);

        uiControllerReference.DashMultiplierOff();

        for (int counter = 0; counter < dashMultiplier; counter++)
        {
            playerRigidBody.AddForce(dashHelperReference.transform.forward * dashForce * 1 * counter, ForceMode.Force);
            yield return new WaitForSeconds(.03f);
        }

        dashHelperReference.GetComponent<DashHelper_Khoa>().canStart = false;
        canMove = true;
        dashMultiplier = 3;
    }

    IEnumerator StrengthRayCooling()
    {
        yield return new WaitForSeconds(.1f);

        strengthRayCastStart = false;
    }

    IEnumerator AbilityEnd(int abilityType)
    {
        abilityCooling = true;
        elementalList[abilityType] -= 1;
        uiControllerReference.UpdateElement(abilityType);

        switch(abilityType)
        {
            case 3:
                yield return new WaitForSeconds(.1f);
                break;
            case 2:
                break;
            case 1:
                yield return new WaitUntil(() => strengthEnd);
                break;
            case 0:
                break;
        }

        abilityCooling = false;
        strengthEnd = false;
        usingStrength = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //call animation idle true + jumping false
            onGround = true;
            gravity = 0;
            jumpCounter = 0;
            canGlide = 0;
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