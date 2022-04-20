using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneRocks : MonoBehaviour
{
    public GameObject smallRockOriginal;
    public GameObject bigRockOriginal;

    public Map script_1;
    public Player script_2;

    private float delay = 0;
    public float[] delayTime = new float[2];

    public int smallRocks = 5;

    private bool received = false;
    private bool firstTime = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime == false)
        {
            if (script_2.interval > 1)
            {
                script_1.actualSpacing = 5f;
            }
            if (script_2.interval > 2)
            {
                script_1.actualSpacing = 3f;
            }
            if (script_2.interval > 3)
            {
                if (received == false)
                {
                    StartCoroutine(Clone());
                    received = true;
                }
            }
        }
        else
        {
            if (script_2.interval > 1)
            {
                script_1.actualSpacing = 5f;
            }
            if (script_2.interval > 2)
            {
                script_1.actualSpacing = 3f;
            }
            if (script_2.interval > 2)
            {
                if (received == false)
                {
                    StartCoroutine(Clone());
                    received = true;

                    firstTime = false;
                }
            }
        }
    }

    IEnumerator Clone()
    {
        delay = Mathf.Lerp(delayTime[0], delayTime[1], script_1.difficulty);
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < smallRocks; i++)
        {
            GameObject SmallRockClone = Instantiate(smallRockOriginal);
        }

        GameObject BigRockClone = Instantiate(bigRockOriginal);

        script_2.interval = 0;
        received = false;
    }
}
