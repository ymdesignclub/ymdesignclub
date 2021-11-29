using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Must have this engine to use User Interphase syntax

public class Key : MonoBehaviour
{
    private Button button; //The Button component of the key
    private Image image; //The Image component of the key
    public PlayNote pn; //The script attached to the main camera that plays the notes
    public int key; //The place it has in the row of keys. First key is 0 and last key is 9
    public float click_play_duration; //The waited time in deltaTime which the key should return back to the rest position
    private KeyCode[] keycode = //A list of number keyboard inputs from 1 - 0 (many keyboards go from 1 - 9 - 0 instead of 0 - 1 - 9)
    { 
        KeyCode.Alpha1, 
        KeyCode.Alpha2, 
        KeyCode.Alpha3,
        KeyCode.Alpha4, 
        KeyCode.Alpha5, 
        KeyCode.Alpha6, 
        KeyCode.Alpha7, 
        KeyCode.Alpha8, 
        KeyCode.Alpha9, 
        KeyCode.Alpha0 
    };
    
    private RectTransform rect;  //The RectTransform component of the key

    private float click_started = -1; //Stores the time when the key was clicked by the cursor. Does not apply to Keyboard inputs

    // Start is called before the first frame update
    void Start()
    {
        button = transform.gameObject.GetComponent<Button>(); //Assigns the Button component of the GameObject
        rect = transform.GetComponent<RectTransform>(); //Assigns the RectTransform of the GameObject
        image = transform.GetComponent<Image>(); //Assigns the Image component of the GameObject

        button.onClick.AddListener(Play); //Links the void "Play" to the button being clicked
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keycode[key])) //If cooresponding number is pressed the note will play
        {
            Play(); //Plays the note
        }

        if (Input.GetKeyUp(keycode[key])) //If cooresponding number is released the note will stop
        {
            Stop(); //Stops the key's note
        }

        if (pn.IsKeySharp(transform.name))//If the button contains a # character it is a sharp note
        {
            image.color = Color.black; //Makes the key black
            rect.sizeDelta = new Vector2(99.6955f, 697.565f); //Makes the key smaller
        }
        else
        {
            image.color = Color.white; //Makes the key white
            rect.sizeDelta = new Vector2(199.391f, 797.565f); //Makes the key its original size
        }

        if(click_started > 0 && Time.time - click_started > click_play_duration)//If the key is pressed for too long it will rest
        {
            click_started = -1; //Sets the start to it's default, this makes sure you can still play the key
            Stop(); //Stops the note from playing
        }
    }

    void Play() //Plays the key's note and makes the key look pressed
    {
        pn.TriggerNote(key); //Accesses the public void of the PlayNote    
        click_started = Time.time;//Sets the variable to the current time which can be compared later
        transform.position = new Vector3(transform.position.x, 35, transform.position.z); //Moves the key down to show it's pressed

    }

    void Stop() //Stops the note from playing and reverts the key back
    {
        pn.StopNote(key); //Accesses the public void of the PlayNote
        transform.position = new Vector3(transform.position.x, 0, transform.position.z); //Moves the key up to show it's pressed

    }
}
