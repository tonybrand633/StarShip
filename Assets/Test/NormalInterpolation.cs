using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalInterpolation : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    public float u;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p01 = p1.transform.position;
        Vector3 p02 = p2.transform.position;
        transform.position = u * p01 + (1 - u) * p02;
    }
}
