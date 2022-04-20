using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWarning : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Player player;

    [SerializeField] private float alpha;

    private bool firstTime = true;
    private bool completed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime == false)
        {
            if (player.interval > 3)
            {
                alpha = Mathf.Clamp(Mathf.Sin(Time.realtimeSinceStartup * 15) - 0.7f, 0, 1);
                _spriteRenderer.color = new Color (255, 0, 0, alpha);
            }
            else
            {
                _spriteRenderer.color = new Color (255, 0, 0, 0);
            }
        }
        else
        {
            if (player.interval > 2)
            {
                alpha = Mathf.Clamp(Mathf.Sin(Time.realtimeSinceStartup * 15) - 0.7f, 0, 1);
                _spriteRenderer.color = new Color (255, 0, 0, alpha);

                completed = true;
            }
            else
            {
                _spriteRenderer.color = new Color (255, 0, 0, 0);

                if (completed == true)
                {
                    firstTime = false;
                }
            }
        }
    }
}
