using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public ParticleSystem[] on;
    public ParticleSystem[] off;
    public float transition_delay;
    private float current_transition_delay;
    private bool played_off_effect;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            for (int i = 0; i < on.Length; i++)
            {
                on[i].Play();
            }

            for (int i = 0; i < off.Length; i++)
            {
                off[i].Stop();
            }

            current_transition_delay = transition_delay;
            played_off_effect = false;
        }
        else
        {
            if (current_transition_delay <= 0 && played_off_effect == false)
            {
                for (int i = 0; i < on.Length; i++)
                {
                    on[i].Stop();
                }

                for (int i = 0; i < off.Length; i++)
                {
                    off[i].Play();
                }

                played_off_effect = true;
            }
            else 
            {
                current_transition_delay -= Time.deltaTime;
            }
        }

        
        
    }
}
