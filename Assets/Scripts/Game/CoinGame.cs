using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGame : MonoBehaviour
{
    Bounds bounds;
    CircleCollider2D circle2D;
    public bool OutScreen;
    // Start is called before the first frame update
    void Start()
    {
        circle2D = GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        bounds = circle2D.bounds;
        OutScreen = BoundsUtility.isInBackGround(bounds);
        if (!OutScreen) 
        {
            Destroy(this.gameObject);
        }
    }

    public void Absorb() 
    {
        Destroy(this.gameObject);
    }
}
