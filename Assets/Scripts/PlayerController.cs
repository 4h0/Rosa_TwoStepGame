using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cameraPosition, strengthPosition, dashHelperReference;
    public LayerMask strengthLayerMask;

    private CameraController cameraReference;
    private UIController uiControllerReference;
    private GameObject savedGameObject;

    private Rigidbody playerRigidBody;
    private SpriteRenderer showPlayer;
    private CapsuleCollider playerCollider;
    private Quaternion facingDirection;

    public bool canMove, onGround, jumping, haveFire;
    public int jumpCounter, glider;
    public float moveSpeed, turnSpeed, dashMultiplier;
    public float gravity, jumpForce, dashForce;

    public int[] clampValue;
    public float[] coolingTimer;
    public float[] elementalList;

    private bool abilityCooling, strengthRayCastStart;
    private bool usingStrength;
    private int dashDirection;
    private float savedMoveSpeed, savedTurnSpeed, savedGravity;

    private float moveHorizontal, moveVertical;
    private float turnHorizontal, turnVertical;

    private void Awake()
    {
        cameraReference = FindObjectOfType<CameraController>();
        uiControllerReference = FindObjectOfType<UIController>();

        playerRigidBody = GetComponent<Rigidbody>();
        showPlayer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider>();

        canMove = true;
        jumping = false;
        abilityCooling = false;
        strengthRayCastStart = false;
        jumpCounter = 0;
        dashDirection = 0;
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
        GravityCheck();
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

        /*
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerRigidBody.velocity = transform.forward * moveHorizontal * moveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidBody.velocity = transform.right * moveVertical * moveSpeed;
        }

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
        */
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

    private void GravityCheck()
    {
        if (Input.GetButton("Jump"))
        {
            glider = 120;
            moveSpeed = savedMoveSpeed * 3;
            turnSpeed = savedTurnSpeed / 3;
        }
        if (Input.GetButtonUp("Jump"))
        {
            glider = 12;
            moveSpeed = savedMoveSpeed;
            turnSpeed = savedTurnSpeed;
        }

        if (!jumping)
        {
            if (Input.GetButtonDown("Jump") && jumpCounter < 2)
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
            if (Input.GetButton("Dash"))
            {
                DashMultiplierIncrease();
            }
            if(Input.GetButtonUp("Dash"))
            {
                StartCoroutine(DashLogic());
                CoolingLogic(0, coolingTimer[1]);

                uiControllerReference.StartCoolDown(0);
            }

            if (Input.GetButton("Strength") && !strengthRayCastStart)
            {
                StrengthRayCast();
            }
            if (Input.GetButtonUp("Strength") && savedGameObject != null)
            {
                usingStrength = true;

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

    private void DashMultiplierIncrease()
    {
        if(dashMultiplier < 9)
        {
            dashMultiplier += Time.deltaTime;
        }

        uiControllerReference.DashMultiplierOn();
    }

    private void StrengthRayCast()
    {
        strengthRayCastStart = true;
        float strengthRayDistance = Vector3.Distance(transform.position, strengthPosition.transform.position);
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

    private void CoolingLogic(int abilityType, float waitingTime)
    {
        if (waitingTime != 0)
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
            usingStrength = false;
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

        playerRigidBody.velocity = new Vector3(0, 0, 0);

        for (int counter = 0; counter < 12; counter++)
        {
            playerRigidBody.AddForce(new Vector3(0, jumpForce * jumpCounter * counter, 0), ForceMode.Acceleration);
            yield return new WaitForEndOfFrame();
        }

        jumping = false;
    }

    IEnumerator DashLogic()
    {
        Debug.Log(dashMultiplier);
        dashHelperReference.GetComponent<DashHelper>().StartChecking();

        yield return new WaitUntil(() => dashHelperReference.GetComponent<DashHelper>().canStart);

        uiControllerReference.DashMultiplierOff();

        for (int counter = 0; counter < dashMultiplier; counter++)
        {
            playerRigidBody.AddForce(dashHelperReference.transform.forward * dashForce * 1 * counter, ForceMode.Force);
            yield return new WaitForSeconds(.03f);
        }

        dashHelperReference.GetComponent<DashHelper>().canStart = false;
        canMove = true;
        dashMultiplier = 3;
    }

    IEnumerator StrengthRayCooling()
    {
        yield return new WaitForSeconds(.1f);

        strengthRayCastStart = false;
    }
    IEnumerator AbilityCooling(int abilityType, float waitingTime)
    {
        yield return new WaitForSeconds(1f);

        CoolingLogic(abilityType, waitingTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            onGround = true;
            gravity = 0;
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
