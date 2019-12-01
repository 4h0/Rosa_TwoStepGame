﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    GameObject Player;
    private Rigidbody PlayerRb;
    public Animator anim;
    
    bool onGround;
    bool playerMoving;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerRb = Player.GetComponent<Rigidbody>();
        anim.SetBool("isGrounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoving();

        if (onGround)
        {
            anim.SetBool("isGliding", false);
        }
        if (Input.GetButtonDown("Jump") && onGround)
        {
            anim.SetBool("isGrounded", false);
        }
        if (Input.GetButton("Jump") && !onGround)
        {
            anim.SetBool("isGliding", true);
        }
        if (Input.GetButtonUp("Jump") && !onGround)
        {
            anim.SetBool("isGliding", false);
        } 


    }
    void LateUpdate()
    {

    }
    void PlayerMoving()
    {
        if(PlayerRb.velocity.magnitude > 0)
        {
            playerMoving = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            anim.SetBool("isGrounded", true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
            anim.SetBool("isGrounded", false);
        }
    }
}
