using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject pipe;

    public float[] gap_sizes = new float[2]; //Declares 3 arrays with a limit of 2 that store floats
    public float[] spaces = new float[2];
    public float[] speeds = new float[2];
    public Vector2[] spawn_area = new Vector2[2]; //Declares an array with a limit of 2 that stores vector2 positions
    public float difficulty_curve_time; //Declares a public float
    public float pipe_x_size; 
    private float difficulty = 0; //Declares a private float

    private float pipe_speed; 
    private float spacing;
    private float gap_size;

    private float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        pipe_speed = speeds[0]; //Assigns all 3 private floats to their respective arrays, starting value (arrays start at 0)
        spacing = spaces[0];
        gap_size = gap_sizes[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(difficulty < 1)//prevents the difficulty from being added to once it reaches 1
        {
            difficulty += Time.deltaTime / Mathf.Max(difficulty_curve_time, 1);//Mathf.Max will prevent 0 division errors 
        }
        else
        {
            difficulty = 1; //Sets the difficulty to 1 when it's greater or equal
        }
       
        if (cooldown <= 0) //the cooldown controls how often a pipe will spawn
        {
            gap_size = Mathf.Lerp(gap_sizes[0], gap_sizes[1], difficulty); //the gap_size controls how large the vertical gap between the pipes are
            spacing = Mathf.Lerp(spaces[0], spaces[1], difficulty); //the spacing controls how far apart the pipes are
            pipe_speed = Mathf.Lerp(speeds[0], speeds[1], difficulty); //the pipe_speed controls how fast the pipes move

            float top_spawn = Mathf.Max(spawn_area[0].y, spawn_area[1].y) - gap_size; //Picks the position at which the pipe can spawn without makeing the gap obstructed above the screen
            float bottom_spawn = Mathf.Min(spawn_area[0].y, spawn_area[1].y) + gap_size; //Picks the position at which the pipe can spawn without the gap obstructed below the screen
            float left_spawn = Mathf.Min(spawn_area[0].x, spawn_area[1].x) + pipe_x_size; //Picks the position that allows the pipe to be hidden

            float spawnY = Random.Range(bottom_spawn, top_spawn); //Randomizes the location that the top pipe spawns between the 2 limit positions
            float spawnX = left_spawn; //Spawns the top pipe at the left_spawn

            GameObject new_pipe = Instantiate(pipe, new Vector2(spawnX, spawnY), Quaternion.identity); //Creates a new pipe at the top position
            GameObject new_pipe2 = Instantiate(pipe, new Vector2(spawnX, spawnY + gap_size), Quaternion.identity); //Creates a new pipe at the bottom position

            new_pipe2.transform.Rotate(Vector3.forward, 180); //Flips the bottom pipe so it dosen't cover the top pipe
            new_pipe2.GetComponent<BoxCollider2D>().enabled = false; //Disables the score collider for the bottom pipe

            new_pipe.GetComponent<Pipe>().speed = pipe_speed; //Assigns the speed for both pipes using the pipes public speed variables
            new_pipe2.GetComponent<Pipe>().speed = pipe_speed;


            cooldown = spacing / Mathf.Min(pipe_speed, 1);//Mathf.Min will prevent 0 division errors 
        }
        else
        {
            cooldown -= Time.deltaTime; //If the cooldown is not less or equal to zero it will be subtracted by the change in time. e.g. (cooldown = 3) (cooldown - (3 seconds * Time.deltaTime) = 0)
        }


        



    }
}
