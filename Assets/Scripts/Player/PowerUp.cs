﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("道具属性")]

    public float lifeTime;
    public float fadeTime;
    public float existTime;
    public float birthTime;
    public float fadingTime;
    public WeaponType type;
    public Vector2 driftMaxMin = new Vector2(0.25f, 1);
    public float u;
    public float v;

    [Header("引用组件")]
    public GameObject powerUp;
    BoxCollider2D col;
    Rigidbody2D rig;
    Renderer render;
    Material mat;

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        render = GetComponent<Renderer>();
        mat = render.material;
        powerUp = this.gameObject;

        Vector2 vel = Random.onUnitSphere;
        vel.Normalize();
        //使用向量来存储随机数
        vel *= Random.Range(driftMaxMin.x, driftMaxMin.y);
        rig.velocity = vel;
        //InvokeRepeating("CheckOffScreen", 1f, 2f);
        birthTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        existTime = Time.time;
        u = (existTime - birthTime) / lifeTime;
        if (u >= 1)
        {
            Destroy(gameObject);
            return;
        }
        //fadingTime后开始隐藏，隐藏到无的时间为fadeTime
        v = (existTime - birthTime - fadingTime) / fadeTime;
        if (v>0) 
        {
            Color c = mat.color;
            c.a = 1 - v;
            mat.color = c;            
        }
        if (v >= 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    //SetType?
    public void SetType(WeaponType wt) 
    {
        WeaponDefinition def = GameManager.GetWeaponDefinition(wt);
        //其余的都是一些对于显示的操作
        type = wt;
    }


    void CheckOffScreen() { 
        if (BoundsUtility.ScreenBoundsCheck(col.bounds,BoundsUtility.BoundTest.offScreen)!=Vector3.zero) 
        {
            Destroy(this.gameObject);
        }
    }

    public void AbsoredBy(GameObject target) 
    {
        //在Spike类收集道具之后调用本函数
        Destroy(this.gameObject);
    }
}
