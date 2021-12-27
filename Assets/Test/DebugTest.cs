using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //float DegreePI = Mathf.PI * Mathf.Rad2Deg;
        
        //Debug.Log(Mathf.Sin(radiaus));
    }

    // Update is called once per frame
    void Update()
    {
        float radiaus =  Time.time;
        Debug.Log(Mathf.Sin(radiaus));
    }
}
