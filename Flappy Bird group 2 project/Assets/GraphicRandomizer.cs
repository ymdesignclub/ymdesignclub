using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicRandomizer : MonoBehaviour
{
    public Sprite[] sprites;
    public int chosen_sprite;
    public SpriteRenderer sp;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        sp.sprite = sprites[chosen_sprite];
    }
}
