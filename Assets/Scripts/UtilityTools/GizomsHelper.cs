using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizomsHelper : MonoBehaviour
{
    Camera cam;
    //相机的Bounds属性
    Bounds camBounds;

    Vector3 topRight= new Vector3(Screen.width, Screen.height, 0);
    Vector3 bottomLeft = new Vector3(0, 0, 0);
    Vector3 topRightGamePoint;
    Vector3 bottomLeftGamePoint;

    //飞船
    public GameObject spike;
    public Bounds spikeBounds;

    void Awake()
    {
        camBounds = BoundsUtility.camBounds;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
        cam = FindObjectOfType<Camera>();
        topRightGamePoint = cam.ScreenToWorldPoint(topRight);
        bottomLeftGamePoint = cam.ScreenToWorldPoint(bottomLeft);

        //找到飞船
        //spike = GameObject.Find("Spike");
        //spikeBounds = BoundsUtility.CombineBoundsOfChildren(spike);
}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(BoundsUtility.backBounds.center, BoundsUtility.backBounds.size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BoundsUtility.camBounds.center, BoundsUtility.camBounds.size);
        
        
    }    
}
