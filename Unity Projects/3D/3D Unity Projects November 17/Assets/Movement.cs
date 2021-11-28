using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject go;
    public Transform ground_check;
    public Transform cam;
    public LayerMask ground_layer;
    public float Speed;
    public float MaxSpeed;
    public float JumpPow;
    public float Sensitivity;
    private float x_rot;
    private float y_rot;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = go.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) //W, A, S, D controls
        {
            //Vector3 v = 
            Vector3 new_v = go.transform.forward * Speed;
            rb.velocity = new Vector3(new_v.x, rb.velocity.y, new_v.z);  //adds velocity to the players rigidbody by using the objects forward vector, makes sure the players fall is not affected 
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 new_v = -go.transform.forward * Speed;
            rb.velocity = new Vector3(new_v.x, rb.velocity.y, new_v.z);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 new_v = go.transform.right * -Speed;
            rb.velocity = new Vector3(new_v.x, rb.velocity.y, new_v.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 new_v = go.transform.right * Speed;
            rb.velocity = new Vector3(new_v.x, rb.velocity.y, new_v.z);
        }

        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime; //Gets the left or right movement of the mouse 
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime; //Gets the up or down movement of the mouse 
        x_rot -= mouseY; //makes the horizontal movement of the mouse effect the players tilt
        y_rot += mouseX; //makes the vertical movement of the mouse effect the players turning
        x_rot = Mathf.Clamp(x_rot, -90, 90); //makes sure you can't look 360 degrees from above

        
        go.transform.localRotation = Quaternion.Euler(0, y_rot, 0); //turns the player using the rotations
        cam.localRotation = Quaternion.Euler(x_rot, 0, 0); //tilts the camera using the rotations


        Collider[] colliders = Physics.OverlapSphere(ground_check.position, 0.5f, ground_layer); //Lists all colliders that are in a sphere area and are also part of the ground layer
        if (colliders.Length > 0 && Input.GetKeyDown(KeyCode.Space)) //if a collider is detected the player is on the ground and can jump
        {
            rb.velocity = new Vector3(rb.velocity.x, JumpPow, rb.velocity.z); //jumping does not require the up vector as it is always constant
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(ground_check.position, 0.5f);
    }
}

