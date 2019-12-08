using System.Collections;
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
        if((Input.GetButton("Horizontal") || Input.GetButton("Vertical") && onGround))
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = true;
            anim.SetBool("isGrounded", true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = true;
            anim.SetBool("isGrounded", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = false;
            anim.SetBool("isGrounded", false);
        }
    }
}
