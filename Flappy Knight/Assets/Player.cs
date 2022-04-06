using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour
{
    public LayerMask danger; //Declares a LayerMask that is considered a hazard
    public LayerMask projectile;
    public Transform hitbox_center; //Declares a transform component representing the center of the player's hitbox
    public float hitbox_radius; //Declares the radius of said hitbox
    private Canvas canvas; //Declares a Canvas component variable
    public Text score_text; //Declares a text component variable
    public Text game_over;
    private int score; 

    public int interval = 0;
    public bool gameOver = false;
    public float slowSpeed = 0f;
    public int slowLength = 1000;
    private int slowCounter = 0;

    private Rigidbody2D rb;

    public int timer;
    public GameObject topCollider;
    public AudioSource Hit;
    public AudioSource Ding;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>(); //Finds a canvas object in the hierarchy / list of all GameObjects
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders1 = Physics2D.OverlapCircleAll(hitbox_center.position, hitbox_radius, danger); //Creates a list of colliders that must be from the danger layer and touching the hitbox
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(hitbox_center.position, hitbox_radius, projectile);
        if ((colliders1.Length > 0 || colliders2.Length > 0) && timer > 199) //If a collision is detected you touched a pipe
        {
            Time.timeScale = slowSpeed; //Time.deltaTime is now always 0 which means the game is paused
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            gameOver = true;
            playHit();
        }

        if (gameOver == true)
        {
            game_over.gameObject.SetActive(true); //enables the game over message
            if (Input.GetKeyDown(KeyCode.Space) && slowCounter > 100) //detects when the player wants to start the game again
            {
                gameOver = false;
                Time.timeScale = 1; //Unpauses the game
                SceneManager.LoadScene(0, LoadSceneMode.Single); //Reloads the entire game
            }

            slowCounter++;
            if (slowCounter > slowLength)
            {
                Time.timeScale = 0;
                slowCounter = 0;
            }
        }

        if (Time.timeScale != 0)
        {
            timer++;
            if (timer < 200)
            {
                Physics2D.IgnoreCollision(topCollider.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
        else
        {
            timer = 0;
        }
    }

    public void GivePoint() //Public void's can be referenced by any other scripts. Used by pipes to give the player points
    {
        score++; //Short way of adding the score by one. score += 1; longer, score--; also works with subtracting
        score_text.text = "SCORE " + score.ToString(); //sets the points display text to be the new number of points. ToString() converts the integer to a usable string

        interval++;
        playDing();
    }

    void playHit()
    {
        Hit.Play();
    }
    void playDing()
    {
        Ding.Play();
    }
}
