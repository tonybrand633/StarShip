using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public EnemyCollection enemyType;
    [Header("基础属性")]    
    public float Speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;


    [Header("颜色和材质")]
    public Color[] originalColors;
    public Material[] materials;

    [Header("受伤表现")]
    public int showDamageForFrames = 2;//显示伤害效果的帧数
    public int remainingDamageFrames = 0;//剩余的伤害效果帧数

    [Header("携带道具")]
    public float powerUpDropChance = 0.2f;//掉落升级道具的概率    
    public int Coins = 15;//玩家击杀后获得的金币

    [NonSerialized]
    public Bounds bounds;
    public float height;
    public bool InScreen;

    void Awake()
    {
        materials = Utils.GetAllMaterial(this.gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }

    }

    protected virtual void Start()
    {
        if (bounds.size == Vector3.zero)
        {
            bounds = BoundsUtility.CombineBoundsOfChildren(this.gameObject);
        }
        //通过bounds获取敌人的高
        height = bounds.max.y - bounds.min.y;
    }

    protected virtual void Update()
    {
        Move();
        CheckScreen();
        CheckOffScreen();
        DisplayDamage();

        
    }

    public void CheckScreen() 
    {
        bounds.center = transform.position;
        if (BoundsUtility.isInBackGround(bounds))
        {
            InScreen = true;
        }
        if (InScreen == true)
        {
            CheckOffScreen();
        }
    }

    void DisplayDamage() 
    {
        if (remainingDamageFrames > 0)
        {
            remainingDamageFrames--;
            if (remainingDamageFrames == 0)
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

    public EnemyCollection GetEnemyType() 
    {
        return enemyType;
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

        if (bounds.max.y<BoundsUtility.backBounds.min.y) 
        {
            Destroy(this.gameObject);
        }
        //Vector3 offset = BoundsUtility.BackBoundsCheck(bounds, BoundsUtility.BoundTest.offScreen);
        //if (offset != Vector3.zero) 
        //{
            
        //    if (height<Mathf.Abs(offset.y)) 
        //    {
        //        Debug.Log(offset);
        //        Destroy(this.gameObject);
        //    }            
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        //Debug.Log(other.name);
        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();
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
                    Debug.Log(enemyType);
                    switch (enemyType) 
                    {                        
                        case EnemyCollection.EnemyRed:
                            GameManager.S.ShipDestoryed(this);
                            //Debug.Log("RedDestroy");
                            Destroy(this.gameObject);
                            break;
                        case EnemyCollection.EnemyOrange:
                            GameManager.S.ShipDestoryed(this);
                            Destroy(this.gameObject);
                            break;
                        case EnemyCollection.EnemyYellow:
                            GameManager.S.ShipDestoryed(this);
                            ParentDestroy(this.gameObject);
                            break;
                        default:
                            GameManager.S.ShipDestoryed(this);
                            Destroy(this.gameObject);
                            break;
                    }
                }
                Destroy(other);
                break;
        }
    }

    public void ParentDestroy(GameObject go)
    {
        Debug.Log("ParentDestoryed");
        GameObject parent = go.transform.parent.gameObject;        
        Destroy(parent);
    }
}
