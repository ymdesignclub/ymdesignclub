using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;    
    public float initVelocityX;
    public float initVelocityY;
    public float random;
    private float randomScale;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(initVelocityX + Random.Range(-random, random), initVelocityY + Random.Range(-random, random));

        transform.Rotate(0, 0, Random.Range(0, 360));

        if (gameObject.tag == "Small Rock") 
        {
            randomScale = Random.Range(1.5f, 2.5f);
            transform.localScale = new Vector2(randomScale, randomScale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -rotationSpeed * Mathf.Ceil(Time.deltaTime));
        
        if (transform.position.y < -10) 
        {
            Destroy(gameObject);
        } 
    }
}
