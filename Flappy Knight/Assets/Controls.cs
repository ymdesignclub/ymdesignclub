using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Rigidbody2D rb; //Declares a Rigidbody2D component
    public float jump_speed; //Declares a public float jump_speed
    public Animator animator;
    public Player player;

    public AudioSource Flap;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>(); //Assigns the rb variable a value of the current Rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.timer > 70) //Detects a space key press
        { 
            rb.velocity = new Vector3(rb.velocity.x, jump_speed); //Changes the velocity to simulate a jump
            animator.SetBool("Trigger", true);
            playFlap();
        }

        if (isPlaying(animator, "Flap"))
        {
            animator.SetBool("Trigger", false);
        }
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void playFlap()
    {
        Flap.Play();
    }
}
