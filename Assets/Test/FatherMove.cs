using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherMove : MonoBehaviour
{
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //public virtual void Move() 
    //{
    //    Vector3 pos = transform.position;
    //    pos.y -= speed * Time.deltaTime;
    //    transform.position = pos;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string s = collision.gameObject.name;
        Print(s);
    }

    void Print(string s)
    {
        Debug.Log(s);
    }
}
