using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject score;
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            score.SetActive(true);
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
