using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private cameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
            cam.MoveToNewRoom(nextRoom);
        else
            cam.MoveToNewRoom(previousRoom);
    }
}
