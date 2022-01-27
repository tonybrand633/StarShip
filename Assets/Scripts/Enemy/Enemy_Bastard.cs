using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Part 
{
    //下面三个字段需要在面板进行定义
    public string name;
    public float health;
    public string[] protectedBy;//保护该组件的其他组件                                    
    public GameObject go;
    public Material mat;
}

public class Enemy_Bastard : Enemy
{
    [Header("循环运动")]
    public bool changeSpeed;
    public Vector3[] points;
    public float StartTime;
    public float DurationTime;
    public Part[] parts;
    public GameObject other;
    public float selfLength;
    public float u;

    // Start is called before the first frame update
    protected override void Start()
    {
        //初始化Parts
        InitParts();

        //初始化敌人类型
        enemyType = EnemyCollection.EnemyBastard;

        //初始化运动所需的点
        points = new Vector3[2];
        points[0] = Vector3.zero;
        points[1] = transform.position;
        InitMove();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (this.gameObject.GetComponent<Transform>().childCount==0) 
        {
            Destroy(this.gameObject);
        }
        base.Update();
    }

    void InitParts() 
    {
        Transform t;
        foreach (Part par in parts) 
        {
            t = transform.Find(par.name);
            if (t!=null) 
            {
                //注意这里使用的临时变量是transform，通过transform我们发现，可以找到gameObject
                par.go = t.gameObject;
                par.mat = par.go.GetComponent<Renderer>().material;
            }
        }
    }

    void InitMove() 
    {
        
        Vector3 tempPos = Vector3.zero;
        float xMax = BoundsUtility.backBounds.max.x-selfLength;
        float xMin = BoundsUtility.backBounds.min.x+selfLength;
        float yMax = BoundsUtility.backBounds.max.y;
        float yMin = (BoundsUtility.backBounds.min.y+BoundsUtility.backBounds.max.y)/2;
        float yRandom = Random.Range(yMin, yMax);
        //float xRandom = Random.Range(xMin, xMax);
        
        

        points[0] = points[1];
        if (points[0].x == xMax)
        {
            tempPos.x = xMin;
        }
        else
        {
            tempPos.x = xMax;
        }
        tempPos.y = yRandom;
        points[1] = tempPos;

        StartTime = Time.time;
    }

    public override void Move()
    {
        u = (Time.time - StartTime) / DurationTime;
        if (u>=1) 
        {
            InitMove();
            if (!changeSpeed)
            {
                changeSpeed = true;
            }
            else 
            {
                changeSpeed = false;
            }
            u = 0;
        }
        if (changeSpeed)
        {
            u = 1 - Mathf.Pow(1 - u, 2);
        }
        else 
        {
            u = Mathf.Pow(u, 2);
        }
        transform.position = (1 - u) * points[0] + u * points[1];
    }


    //对于MonoBehaviour中OnCollisionEnter等常规函数
    //这里不需要添加override即可达到override的效果
    void OnCollisionEnter2D(Collision2D collision)
    {
        other = collision.gameObject;
        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();
                if (!InScreen)
                {
                    Destroy(other);
                    break;
                }
                //goHit代表了击中部位的名字
                GameObject goHit = collision.contacts[0].collider.gameObject;
                Part hitPart = FindPart(goHit.name);
                if (hitPart == null)
                {
                    goHit = collision.contacts[0].otherCollider.gameObject;
                    hitPart = FindPart(goHit.name);
                }
                if (isAllDestoryed(hitPart))
                {
                    //Debug.Log("Destroy Check:True");
                    hitPart.health -= GameManager.W_DEFS[p.type].DamageOnHit;
                    ShowLocalizedDamage(hitPart.mat);
                    if (hitPart.health<0) 
                    {
                        Destroy(goHit);
                    }
                }
                else 
                {
                    Destroy(other);
                    return;   
                }
                Destroy(other);
                break;
        }
    }

    bool isAllDestoryed(Part p) 
    {
        foreach (string s in p.protectedBy) 
        {
            Part sPart = FindPart(s);
            if (sPart.health>0) 
            {
                return false;
            }
        }
        return true;
    }

    Part FindPart(string n) 
    {
        foreach (Part p in parts) 
        {
            if (p.name == n) 
            {
                return p;
            }
        }
        return null;
    }

    Part FindPart(GameObject go) 
    {
        foreach (Part p in parts)
        {
            if (p.go == go) 
            {
                return p;
            }
        }
        return null;
    }

    void ShowLocalizedDamage(Material mat) 
    {
        mat.color = Color.red;
        remainingDamageFrames = showDamageForFrames;
    }

    //public void MoveFTS() 
    //{
    //    u = (Time.time - StartTime) / DurationTime;
    //    if (u >= 1)
    //    {
    //        InitMove();
    //        u = 0;
    //    }
    //    u = 1 - Mathf.Pow(1 - u, 2);
    //    transform.position = (1 - u) * points[0] + u * points[1];
    //    changeSpeed = false;
    //}

    //public void MoveSTF() 
    //{
    //    u = (Time.time - StartTime) / DurationTime;
    //    if (u >= 1)
    //    {
    //        InitMove();
    //        u = 0;
    //    }
    //    u = Mathf.Pow(u, 2);
    //    transform.position = (1 - u) * points[0] + u * points[1];
    //    changeSpeed = true;
    //}

}
