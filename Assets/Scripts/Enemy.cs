using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    //玩家击杀后获得的金币
    public int Coins = 1;

    public Bounds bounds;
    public float height;
    public bool InScreen;

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

    }
    private void Awake()
    {
        //InvokeRepeating("CheckOffScreen", 0f, 2f);    
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
    public virtual void Move() 
    {
        Vector3 pos = transform.position;
        pos.y -= Speed * Time.deltaTime;
        transform.position = pos;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;        

        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();
                Debug.Log(p.type.ToString());
                //让敌机在进入屏幕之前不受伤害
                if (!InScreen)
                {
                    Destroy(other);
                    break;
                }
                //否则，就给该敌机造成伤害
                health -= GameManager.W_DEFS[p.type].DamageOnHit;
                if (health <= 0)
                {
                    Destroy(this.gameObject);
                }
                Destroy(other);
                break;
        }
    }
}
