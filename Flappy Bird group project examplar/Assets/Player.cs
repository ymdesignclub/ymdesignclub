using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public LayerMask danger; //Declares a LayerMask that is considered a hazard
    public Transform hitbox_center; //Declares a transform component representing the center of the player's hitbox
    public float hitbox_radius; //Declares the radius of said hitbox
    private Canvas canvas; //Declares a Canvas component variable
    public Text score_text; //Declares a text component variable
    public Text game_over;
    private int score; 

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>(); //Finds a canvas object in the hierarchy / list of all GameObjects
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitbox_center.position, hitbox_radius, danger); //Creates a list of colliders that must be from the danger layer and touching the hitbox
        if(colliders.Length > 0) //If a collision is detected you touched a pipe
        {
            Time.timeScale = 0; //Time.deltaTime is now always 0 which means the game is paused
            game_over.gameObject.SetActive(true); //enables the game over message
            if (Input.GetKeyDown(KeyCode.Space)) //detects when the player wants to start the game again
            {
                
                Time.timeScale = 1; //Unpauses the game
                SceneManager.LoadScene(0, LoadSceneMode.Single); //Reloads the entire game
                
            }
        }
    }

    public void GivePoint() //Public void's can be referenced by any other scripts. Used by pipes to give the player points
    {
        score++; //Short way of adding the score by one. score += 1; longer, score--; also works with subtracting
        score_text.text = score.ToString(); //sets the points display text to be the new number of points. ToString() converts the integer to a usable string
    }
}
