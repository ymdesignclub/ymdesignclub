using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Must have this engine to use User Interphase syntax

public class PlayNote : MonoBehaviour
{
    public GameObject source; //The main camera bacause it has the audio source
    public Slider scale; //The slider that switches the keys
    public GameObject canvas; //The UI Canvas

    private float[] note_frequency = { 27.5f, 29.135f, 30.868f, 32.703f, 34.648f, 36.708f, 38.891f, 41.203f, 43.654f, 46.249f, 48.999f, 51.913f, 55.0f, 58.27f, 61.735f, 65.406f, 69.296f, 73.416f, 77.782f, 82.407f, 87.307f, 92.499f, 97.999f, 103.826f, 110.0f, 116.541f, 123.471f, 130.813f, 138.591f, 146.832f, 155.563f, 164.814f, 174.614f, 184.997f, 195.998f, 207.652f, 220.0f, 233.082f, 246.942f, 261.626f, 277.183f, 293.665f, 311.127f, 329.628f, 349.228f, 369.994f, 391.995f, 415.305f, 440.0f, 466.164f, 493.883f, 523.251f, 554.365f, 587.33f, 622.254f, 659.255f, 698.456f, 739.989f, 783.991f, 830.609f, 880.0f, 932.328f, 987.767f, 1046.502f, 1108.731f, 1174.659f, 1244.508f, 1318.51f, 1396.913f, 1479.978f, 1567.982f, 1661.219f, 1760.0f, 1864.655f, 1975.533f, 2093.005f, 2217.461f, 2349.318f, 2489.016f, 2637.021f, 2793.826f, 2959.955f, 3135.964f, 3322.438f, 3520.0f, 3729.31f, 3951.066f, 4186.009f };
    //A long list of all the notes frequency(hz). Used to play a wide variety of tones

    private string[] note_names = { "A", "A# / Bb", "B", "C", "C# / Db", "D", "D# / Eb", "E", "F", "F# / Gb", "G", "G# / Ab", "A", "A# / Bb", "B", "C", "C# / Db", "D", "D# / Eb", "E", "F", "F# / Gb", "G", "G# / Ab", "A", "A# / Bb", "B", "C", "C# / Db", "D", "D# / Eb", "E", "F", "F# / Gb", "G", "G# / Ab", "A", "A# / Bb", "B", "C", "C# / Db", "D", "D# / Eb", "E", "F", "F# / Gb", "G", "G# / Ab", "A", "A# / Bb", "B", "C", "C# / Db", "D", "D# / Eb", "E", "F", "F# / Gb", "G", "G# / Ab", "A", "A# / Bb", "B", "C", "C# / Db", "D", "D# / Eb", "E", "F", "F# / Gb", "G", "G# / Ab", "A", "A# / Bb", "B", "C", "C# / Db", "D", "D# / Eb", "E", "F", "F# / Gb", "G", "G# / Ab", "A", "A# / Bb", "B", "C"  };
    //A long list of all the names of notes. Some are duplicated but are in order from the lowest note to the highest on an 88 key piano
    private float default_frequency; //The frequency of middle C

    // Start is called before the first frame update
    void Start()
    {
        default_frequency = note_frequency[39]; //Sets the default frequency
    }

    // Update is called once per frame
    void Update()
    {
        int first_key = Mathf.FloorToInt(scale.value * 8.75f); //Uses the sliders value to find the left most key
        //The slider moves from 0 - 9 to make the 0th seciton reach 0 - 10 and the 9th section reach 77 - 82 you multiple the scale value by 8.75;
        //Mathf.FloorToInt() just rounds the number down to the nearest integer 1.9 -> 1

        float x_offset = 1; //Saves the correct offset for the notes
        float increment = 69.6549f; //If another key is added it is this far away from the previous one
        for (int i = first_key; i < first_key + source.transform.childCount; i++)//For loop that loops though all the availiable keys
        {
            int selected_object_index = i - first_key; //Used to retrive the correct child. Ranges from 0 - 9
            AudioSource audio = source.transform.GetChild(selected_object_index).GetComponent<AudioSource>(); //The current audio source
            audio.pitch = note_frequency[i] / default_frequency; //Changes the pitch variable based on the ratio between the custom hz and the default


            GameObject selected_key = canvas.transform.GetChild(selected_object_index).gameObject; //The current key on the piano
            selected_key.transform.position = new Vector3(x_offset, selected_key.transform.position.y, selected_key.transform.position.z); //Moves the key to the correct offset from the left side
            selected_key.name = note_names[i] + " Note"; //Changes the name of the key to it's corresponding note

            //The color of the key is controled from the "Key" script
            if (IsKeySharp(note_names[i])) //Checks if the key should be a black sharp
            {
                x_offset += increment / 2; //Sharp keys are half the size and therefore require half the increment to be removed
            }
            else
            {
                x_offset += increment; //Normal keys require the full increment
            }
            Text note_display = selected_key.transform.GetChild(0).GetComponent<Text>(); //The key's text component from the Text GameObject
            note_display.text = note_names[i]; //Changes the text of the key to match it's note
        }
    }

    //public void can be accessed from other scripts. private or void alone cannot be accessed
    public void TriggerNote(int key) //Plays a selected key
    {
        AudioSource audio = source.transform.GetChild(key).GetComponent<AudioSource>(); //References the corresponding GameObject AudioSource component
        AudioClip clip = audio.clip; //References the AudioSource's clip
        audio.PlayOneShot(clip); //Plays the clip once from the AudioSource. There are many audio sources because the pitch changes
        
    }

    public void StopNote(int key) //Stops a selected key
    {
        AudioSource audio = source.transform.GetChild(key).GetComponent<AudioSource>(); //References the corresponding GameObject AudioSource component
        audio.Stop(); //Stops the clip from playing

    }

    public bool IsKeySharp(string name) //Returns a boolean. True if the name is a sharp
    {
        return name.Contains("#"); //Returns True is a string contains a hashtag
    }
}
