using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Alex : MonoBehaviour
{
    public LayerMask[] rayCastLayerMask;
    public Material[] savedMaterial;

    //public GameObject dashHelperReference, strengthHelperReference;
    public GameObject playerParticle;
    public Transform strengthRayEndPoint;
    public AudioSource walkSound; //This is the sound for walking
    public Animator anim;

    private UIController_Khoa uiControllerReference;
    private Transform cameraT;
    private GameObject savedGameObject;

    private Rigidbody playerRigidBody;
    private LineRenderer strengthRayCastRenderer;
    private SpriteRenderer showPlayer;
    private CapsuleCollider playerCollider;

    public bool canMove, onGround, jumping, strengthEnd, dashing;
    public int jumpCounter, maxJumpCounter;
    public float walkSpeed, runSpeed, dashMultiplier, maxDashMultiplier;
    public float jumpForce, turnSmoothTime, speedSmoothTime;
    public float dashForce;

    public float[] elementalList;
    public float[] maxElementCounter;

    private bool abilityCooling, strengthRayCastStart;
    private bool usingStrength, canGlide, gliding;
    private float gravity;

    float turnSmoothVelocity;
    float speedSmoothVelocity;
    float currentSpeed;

    private void Awake()
    {
        while (uiControllerReference == null)
        {
            uiControllerReference = FindObjectOfType<UIController_Khoa>();
        }
        
        cameraT = Camera.main.transform;

        playerRigidBody = GetComponent<Rigidbody>();
        strengthRayCastRenderer = GetComponent<LineRenderer>();
        showPlayer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider>();

        Cursor.visible = false;

        TurnOffPlayerParticle();
    }

    private void Start()
    {
        canMove = true;
        jumping = false;
        canGlide = false;
        abilityCooling = false;
        strengthRayCastStart = false;
        strengthEnd = false;
        dashing = false;
        jumpCounter = 1;
        dashMultiplier = 3;

        strengthRayCastRenderer.useWorldSpace = true;
        strengthRayCastRenderer.startColor = Color.white;
        strengthRayCastRenderer.endColor = Color.red;
        strengthRayCastRenderer.startWidth = 0;
        strengthRayCastRenderer.endWidth = 0;
    }

    private void Update()
    {
        if (canMove)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;

            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }

            //bool running = Input.GetKey(KeyCode.LeftShift);
            float targetSpeed = walkSpeed * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
                if (!walkSound.isPlaying && onGround)
                {
                    walkSound.Play();
                }
            }
            else
            {
                walkSound.Pause();
            }
            InputCheck();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            JumpingCheck();
        }
    }



    public void TurnOnPlayerParticle()
    {
        playerParticle.SetActive(true);
    }
    public void TurnOffPlayerParticle()
    {
        playerParticle.SetActive(false);
    }



    private void InputCheck()
    {
        if (!abilityCooling)
        {
            if (elementalList[0] > 0 && !dashing)
            {
                if (Input.GetButtonUp("Dash"))
                {
                    dashMultiplier = Mathf.RoundToInt(dashMultiplier);

                    StartCoroutine(DashLogic());
                    StartCoroutine(AbilityEnd(0));
                }
                if(elementalList[0] > 1)
                {
                    if (Input.GetButton("Dash"))
                    {
                        DashMultiplierIncrease();
                    }
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

                    savedGameObject.AddComponent<StrengthLogic_Khoa>();
                    savedGameObject.GetComponent<StrengthLogic_Khoa>().timerBeforeDestroy = 9;
                }
            }
        }
    }



    private void JumpingCheck()
    {
        if (!jumping)
        {
            if (!onGround)
            {
                if(!gliding)
                {
                    Ray gravityRayCast = new Ray(transform.position, transform.up * -1);
                    RaycastHit gravityRayHit;
                    
                    bool rayHit = Physics.Raycast(gravityRayCast, out gravityRayHit, Mathf.Infinity, rayCastLayerMask[0]);

                    if (rayHit)
                    {
                        gravity = (-65.4f) * jumpCounter * jumpCounter / (Vector3.Distance(this.transform.position, gravityRayHit.point));
                    }
                    else
                    {
                        gravity = -98.1f;
                    }
                }
                else
                {
                    gravity = 0;
                }

                playerRigidBody.AddForce(new Vector3(0, gravity, 0), ForceMode.Force);
            }

            if (Input.GetButtonUp("Jump") || elementalList[3] <= 0)
            {
                canGlide = false;
            }

            if (Input.GetButtonDown("Jump") && jumpCounter < maxJumpCounter)
            {
                StartCoroutine(JumpingLogic());                
            }

            if (Input.GetButton("Jump") && canGlide && elementalList[3] > 0)
            {

                elementalList[3] -= Time.deltaTime;
                uiControllerReference.UpdateElement(3);

                if (!gliding)
                {
                    StartCoroutine(GlidingLogic());
                }
            }
        }
    }

    IEnumerator JumpingLogic()
    {
        jumping = true;
        canGlide = false;
        jumpCounter++;
        gravity = 0;

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

        uiControllerReference.UpdateElement(3);

        playerRigidBody.velocity = new Vector3(0, 0, 0);

        for (int counter = 0; counter < 6; counter++)
        {
            playerRigidBody.AddForce(new Vector3(0, jumpForce * jumpCounter * counter, 0), ForceMode.Acceleration);

            yield return new WaitForEndOfFrame();
        }

        jumping = false;

        yield return new WaitForSeconds(.3f);

        canGlide = true;
    }

    IEnumerator GlidingLogic()
    {
        playerRigidBody.velocity = new Vector3(0, 0, 0);
        gliding = true;

        yield return new WaitUntil(() => !canGlide);

        gliding = false;
    }



    private void DashMultiplierIncrease()
    {
        if (dashMultiplier < maxDashMultiplier)
        {
            dashMultiplier += Time.deltaTime;
            elementalList[0] -= Time.deltaTime;
        }

        uiControllerReference.UpdateElement(0);
    }
    IEnumerator DashLogic()
    {
        abilityCooling = true;
        dashing = true;

        yield return new WaitForSeconds(.1f);

        if (dashMultiplier > 0)
        {
            playerRigidBody.AddForce(this.transform.forward * dashForce, ForceMode.Impulse);

            dashMultiplier--;

            StartCoroutine(DashLogic());
        }
        else
        {
            playerRigidBody.velocity = new Vector3(0, 0, 0);

            abilityCooling = false;
            dashing = false;
            dashMultiplier = 3;
        }
    }



    private void StrengthRayCast()
    {
        strengthRayCastStart = true;

        float strengthRayDistance = Vector3.Distance(cameraT.transform.position, strengthRayEndPoint.position);
        Ray strengthRayCast = new Ray(transform.position, transform.forward * strengthRayDistance);
        RaycastHit strengthRayHit;

        strengthRayCastRenderer.SetPosition(0, strengthRayCast.origin);
        strengthRayCastRenderer.SetPosition(1, strengthRayCast.origin + strengthRayCast.direction * strengthRayDistance);        
        strengthRayCastRenderer.startWidth = .1f;
        strengthRayCastRenderer.endWidth = .6f;

        Debug.DrawLine(strengthRayCast.origin, strengthRayCast.direction * strengthRayDistance);

        bool rayHit = Physics.Raycast(strengthRayCast, out strengthRayHit, strengthRayDistance, rayCastLayerMask[1]);

        if (rayHit)
        {
            GameObject hitGameObject = strengthRayHit.transform.gameObject;

            strengthRayCastRenderer.SetPosition(0, strengthRayCast.origin);
            strengthRayCastRenderer.SetPosition(1, strengthRayHit.point);
            Debug.Log("target: " + hitGameObject.name);

            if (hitGameObject.tag == "PickUp")
            {
                if(hitGameObject != savedGameObject && savedGameObject != null)
                {
                    hitGameObject.GetComponent<MeshRenderer>().material = savedMaterial[0];
                }

                savedGameObject = hitGameObject;
                hitGameObject.GetComponent<MeshRenderer>().material = savedMaterial[1];
            }
        }
        else
        {
            if (savedGameObject != null)
            {
                savedGameObject.GetComponent<MeshRenderer>().material = savedMaterial[0];
                savedGameObject = null;
            }
        }

        StartCoroutine(StrengthRayCooling());
    }

    IEnumerator StrengthRayCooling()
    {
        yield return new WaitForSeconds(.1f);

        strengthRayCastRenderer.startWidth = 0;
        strengthRayCastRenderer.endWidth = 0;
        strengthRayCastStart = false;
    }



    IEnumerator AbilityEnd(int abilityType)
    {
        abilityCooling = true;
        elementalList[abilityType] -= 1;
        uiControllerReference.UpdateElement(abilityType);

        switch (abilityType)
        {
            case 3:
                yield return new WaitForSeconds(.1f);
                break;
            case 2:
                yield return new WaitForSeconds(.1f);
                break;
            case 1:
                {
                    walkSpeed /= 3;

                    yield return new WaitUntil(() => strengthEnd);

                    walkSpeed *= 3;
                    break;
                }
            case 0:
                yield return new WaitForSeconds(.1f);
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
            onGround = true;
            canGlide = false;
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
