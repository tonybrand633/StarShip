﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int showDamageForFrames = 2;//显示伤害效果的帧数
    public Color[] originalColors;
    public Material[] materials;
    public int remainingDamageFrames = 0;//剩余的伤害效果帧数
    
    public int Coins = 1;//玩家击杀后获得的金币

    public Bounds bounds;
    public float height;
    public bool InScreen;

    private void Awake()
    {
        materials = Utils.GetAllMaterial(this.gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }

    }

    private void Start()
    {
        if (bounds.size == Vector3.zero)
        {
            bounds = BoundsUtility.CombineBoundsOfChildren(this.gameObject);
        }
        //通过bounds获取敌人的高
        height = bounds.max.y - bounds.min.y;
    }

    private void Update()
    {
        Move();
        bounds.center = transform.position;
        if (BoundsUtility.isInCamera(bounds))
        {
            InScreen = true;
        }
        if (InScreen == true) 
        {
            CheckOffScreen();
        }

        if (remainingDamageFrames>0) 
        {
            remainingDamageFrames--;
            if (remainingDamageFrames==0) 
            {
                UnShowDamage();
            }
        }

    }

    public virtual void Move()
    {
        Vector3 pos = transform.position;
        pos.y -= Speed * Time.deltaTime;
        transform.position = pos;
    }

    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        remainingDamageFrames = showDamageForFrames;
    }

    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
    }

    //屏幕检测，出了屏幕后销毁飞机
    void CheckOffScreen()
    {
        Vector3 offset = BoundsUtility.ScreenBoundsCheck(bounds, BoundsUtility.BoundTest.offScreen);

       
        if (offset != Vector3.zero) 
        {
            //Debug.Log(offset);
            if (height<Mathf.Abs(offset.y)) 
            {
                Destroy(this.gameObject);
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;        

        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();
                //Debug.Log(p.type.ToString());
                //让敌机在进入屏幕之前不受伤害
                if (!InScreen)
                {
                    Destroy(other);
                    break;
                }
                //否则，就给该敌机造成伤害
                health -= GameManager.W_DEFS[p.type].DamageOnHit;
                ShowDamage();
                if (health <= 0)
                {
                    Destroy(this.gameObject);
                }
                Destroy(other);
                break;
        }
    }


}
