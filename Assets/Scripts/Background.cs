using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Bounds backBounds;
    public SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        backBounds = spr.bounds;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
