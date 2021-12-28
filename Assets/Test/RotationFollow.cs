using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollow : MonoBehaviour
{
    public Transform follower;
    public Vector3 relative;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        relative = this.transform.InverseTransformPoint(follower.position);
        angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        this.transform.Rotate(0, 0, angle);
    }
}
