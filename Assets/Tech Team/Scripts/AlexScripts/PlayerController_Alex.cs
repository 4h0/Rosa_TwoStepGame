﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Alex : MonoBehaviour
{
    public GameObject dashHelperReference, strengthHelperReference;
    public Transform strengthRayEndPoint;
    public LayerMask strengthLayerMask;

    private CameraController_Khoa cameraReference;
    private UIController_Khoa uiControllerReference;
    private Animator animator;
    private Transform cameraT;
    private GameObject savedGameObject;

    private Rigidbody playerRigidBody;
    private SpriteRenderer showPlayer;
    private CapsuleCollider playerCollider;
    private Quaternion facingDirection;

    public bool canMove, onGround, jumping, strengthEnd;
    public int jumpCounter, maxJumpCounter;
    public float walkSpeed, runSpeed, dashMultiplier, maxDashMultiplier;
    public float jumpForce, turnSmoothTime, speedSmoothTime;
    public float dashForce, maxElementCounter;

    public float[] elementalList;

    private bool abilityCooling, strengthRayCastStart;
    private bool usingStrength, canGlide, gliding;
    private float gravity;

    float turnSmoothVelocity;
    float speedSmoothVelocity;
    float currentSpeed;

    private void Awake()
    {
        cameraReference = FindObjectOfType<CameraController_Khoa>();
        uiControllerReference = FindObjectOfType<UIController_Khoa>();
        //animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;

        playerRigidBody = GetComponent<Rigidbody>();
        showPlayer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider>();

        canMove = true;
        jumping = false;
        canGlide = false;
        abilityCooling = false;
        strengthRayCastStart = false;
        strengthEnd = false;
        jumpCounter = 0;
        dashMultiplier = 3;
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

            bool running = Input.GetKey(KeyCode.LeftShift);
            float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

            // float animationSpeedPercent = ((running)? 1:.5f) * inputDir.magnitude;
            // animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        }

        InputCheck();
    }

    private void FixedUpdate()
    {
        JumpingCheck();
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

                    savedGameObject.AddComponent<StrengthLogic_Khoa>();
                    savedGameObject.GetComponent<StrengthLogic_Khoa>().timerBeforeDestroy = 9;
                }
            }
        }
    }

    private void JumpingCheck()
    {
        Debug.Log(gravity);

        if (!jumping)
        {
            if (!onGround)
            {
                if(!gliding)
                {
                    gravity -= 3.27f;
                }
                else
                {
                    gravity = 0;
                }

                playerRigidBody.AddForce(new Vector3(0, gravity, 0), ForceMode.Force);
            }

            if (Input.GetButtonDown("Jump") && jumpCounter < maxJumpCounter)
            {
                StartCoroutine(JumpingLogic());
            }

            if(Input.GetButton("Jump") && canGlide)
            {
                StartCoroutine(GlidingLogic());
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            canGlide = false;
        }
    }

    private void DashMultiplierIncrease()
    {
        if (dashMultiplier < maxDashMultiplier)
        {
            dashMultiplier += Time.deltaTime;
        }

        uiControllerReference.DashMultiplierOn();
    }

    private void StrengthRayCast()
    {
        strengthRayCastStart = true;
        float strengthRayDistance = Vector3.Distance(cameraT.transform.position, strengthRayEndPoint.position);
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
                if(hitGameObject != savedGameObject && savedGameObject != null)
                {
                    savedGameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                }

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

    IEnumerator DashLogic()
    {
        dashHelperReference.GetComponent<DashHelper_Khoa>().StartChecking();

        yield return new WaitUntil(() => dashHelperReference.GetComponent<DashHelper_Khoa>().canStart);

        uiControllerReference.DashMultiplierOff();

        for (int counter = 0; counter < dashMultiplier; counter++)
        {
            playerRigidBody.AddForce(dashHelperReference.transform.forward * dashForce * counter, ForceMode.Force);
            yield return new WaitForEndOfFrame();
        }

        canMove = true;
        dashHelperReference.GetComponent<DashHelper_Khoa>().canStart = false;
        dashMultiplier = 3;
    }

    IEnumerator StrengthRayCooling()
    {
        yield return new WaitForSeconds(.1f);

        strengthRayCastStart = false;
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
        canGlide = true;
    }

    IEnumerator GlidingLogic()
    {
        playerRigidBody.velocity = new Vector3(0, 0, 0);

        gliding = true;

        yield return new WaitUntil(() => !canGlide);

        gliding = false;
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
                break;
            case 1:
                {
                    walkSpeed /= 3;

                    yield return new WaitUntil(() => strengthEnd);

                    walkSpeed *= 3;
                    break;
                }
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