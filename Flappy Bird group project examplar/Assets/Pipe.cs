using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float speed; //Declares the speed the pipes move at. Is Assigned by the Map script
    public float life_time; //Declares how long the pipes will last until "despawning"
    private Transform player_pos; //Declares the Player transform component
 
    // Start is called before the first frame update
    void Start()
    {
        player_pos = FindObjectOfType<Player>().transform; //References the Player(script) -> objects transform
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + -speed * Time.deltaTime, transform.position.y); //Moves the pipe by the assigned speed


        if (life_time <= 0)//when life_time reaches 0 it is DESTROYED
        {
            Destroy(transform.gameObject); //DESTROYED
        }
        else
        {
            life_time -= Time.deltaTime; //If the life_time is not less or equal to zero it will be subtracted by the change in time. e.g. (life_time = 3) (life_time - (3 seconds * Time.deltaTime) = 0)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//A special function that runs when a trigger collider touches another trigger collider
    {
        Player player = FindObjectOfType<Player>(); //finds a player component in the hierarchy / list of all GameObjects
        player.GivePoint(); //Gives a point to the Player by calling it's public function
    }
}
