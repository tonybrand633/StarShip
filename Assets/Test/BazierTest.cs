using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazierTest : MonoBehaviour
{
    public Transform t0, t1, t2;
    public List<Transform> tList;
    public Vector3 p01,p12,p012;
    public List<Vector3> vList;

    public float u;
    public bool StartCaculate;
    public bool Move;

    public float StartTime;
    public float LifeTime;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tList.Count-1; i++)
        {
            vList.Add(tList[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCaculate) 
        {
            StartCaculate = false;
            Move = true;
            StartTime = Time.time;                       
        }




        if (Move) 
        {
            u = (Time.time - StartTime )/ LifeTime;
            if (u >= 1)
            {
                u = 1;
                Move = false;
            }
            else
            {
                Vector3 res = BazierTraverse.Bazier(u, vList);
                transform.position = res;
            }
        }
        
    }
}
