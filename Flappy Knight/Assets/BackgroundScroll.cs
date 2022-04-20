using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float[] Scroll_lerp;
    public float CycleTime;
    public float offset;
    private float CurrentCycleTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentCycleTime -= Time.deltaTime;
        float lerp_t = CurrentCycleTime / CycleTime;
        transform.position = new Vector3(Mathf.Lerp(Scroll_lerp[1], Scroll_lerp[0], lerp_t) + offset, transform.position.y, transform.position.z);
        if(lerp_t <= 0) 
        {
            CurrentCycleTime = CycleTime;
        }
    }
}
