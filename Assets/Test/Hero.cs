using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        //float Horizontal = Input.GetAxisRaw("Horizontal");
        //float Vertical = Input.GetAxisRaw("Vertical");
        //Vector3 pos = transform.position;
        //pos.x += speed * Horizontal * Time.deltaTime;
        //pos.y += speed * Vertical * Time.deltaTime;
        //transform.position = pos;
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rig.velocity = Vector2.up * speed;
        }
        
        
    }
}
