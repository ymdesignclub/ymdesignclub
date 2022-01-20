using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapper : MonoBehaviour
{
    private Transform player;
    public float turn_off_distance;
    private Animator anim;
    private bool hidden = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.position;
        Vector3 p2 = player.position;
        float magnitude = Mathf.Sqrt(Mathf.Pow(p.x - p2.x, 2) + Mathf.Pow(p.y - p2.y, 2));
        if (turn_off_distance >= magnitude && !hidden)
        {
            //Plays a short animation then turns off
            anim.SetBool("Hide", true);
            hidden = true;
        }
    }
}
