using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float divider;
    public float clampValue;

    public Player script;

    private Camera cam;
    public float deathZoomSize = 3f;
    public int shakeDuration = 3;
    public float shakeIntensity = 0.01f;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.enabled = true;
        cam.orthographicSize = 4.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (script.gameOver == false)
        {
            transform.position = new Vector3(0, Mathf.Clamp(player.transform.position.y / divider, -clampValue, clampValue), -10);
        } 
        else 
        {
            if (shakeDuration > 0)
            {
                transform.position = new Vector3(0, player.transform.position.y / divider, -10) + Random.insideUnitSphere * shakeIntensity;
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -clampValue, clampValue), transform.position.z);
                shakeDuration--;
            } 
            else
            {
                Vector3 targetPos = new Vector3(player.transform.position.x, Mathf.Clamp(player.transform.position.y, -1, 1), -10);
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.3f);

                cam.orthographicSize += (deathZoomSize - cam.orthographicSize) * Mathf.Ceil(Time.deltaTime) * 0.001f; 
            }
        } 
    }
}
