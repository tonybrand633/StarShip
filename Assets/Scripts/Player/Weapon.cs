﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //static public Transform firePosition;

    public bool _________________________;
    [SerializeField]
    private WeaponType _type;
    public WeaponType type 
    {
        get { return _type; }
        set { SetType(value); }
    }

    public GameObject StoreProjectile;
    public WeaponDefinition def;
    public GameObject FirePosition;
    public float lastShot;

    private void Start()
    {
        //找到射击位置
        FirePosition = transform.Find("FirePosition").gameObject;
        //设置子弹类型
        SetType(_type);
        GameObject parentGO = transform.parent.gameObject;
        if (parentGO.tag == "Hero") 
        {
            Spike.S.fireDelegate += Fire;
        }
    }

    public void SetType(WeaponType wt) 
    {
        _type = wt;
        //得到的是spread
        def = GameManager.GetWeaponDefinition(_type);
        //此处可以再对子弹进行一系列操作
        //你总是能在改变武器类型的一瞬间进行射击
        //这里出现一个问题是如果一直在更改type，那么lastshot就一直为0
        //lastShot = 0;
    }

    public void Fire() 
    {
        if (!gameObject.activeInHierarchy) 
        {
            return;
        }
        if (Time.time - lastShot < def.DelayBetweenShot) 
        {
            return;
        }

        Projectile p;
        switch (type) 
        {
            case WeaponType.single:
                p = InstantiateProjectile();
                p.rig.velocity = Vector3.up * def.velocity;
                break;
            case WeaponType.spread:
                p = InstantiateProjectile();    
                p.rig.velocity = Vector3.up * def.velocity;
                break;
            case WeaponType.triple:
                p = InstantiateProjectile();
                p.rig.velocity = Vector3.up * def.velocity;
                break;
                
        }
    }

    public Projectile InstantiateProjectile() 
    {
           GameObject go = Instantiate(def.Projectile) as GameObject;
        if (transform.parent.gameObject.tag == "Hero")
        {
            go.tag = "ProjectileHero";
            go.layer = LayerMask.NameToLayer("BulletPlayer");
        }
        else if(transform.parent.gameObject.tag == "Enemy")
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("BulletEnemy");
        }
        go.transform.position = FirePosition.transform.position;
        go.transform.parent = StoreProjectile.transform;
        //单独返回一个生成的子弹（go）的Projectile脚本
        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        lastShot = Time.time;
        return p;
    }    

    
}





