using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_02 : Enemy
{
    Vector3[] birthPos;
    [SerializeField]
    [Header("正弦线性插值")]
    public bool StartCalculate;
    public bool Moving;
    public float lifeTime;
    public float birthTime;
    public float birthOffset;
    public float u1;
    public float loop;
    public bool isIncrease;
    public float sinRes;
    public float SinRes 
    {
        get { return sinRes; }

        set 
        {
            if (sinRes < value)
            {
                isIncrease = false;
            }
            else 
            {
                isIncrease = true;
            }
            sinRes = value;            
        }
    }
    Quaternion rotation = Quaternion.identity;



    float backMax;
    float backMin;

    // Start is called before the first frame update
     protected override void Start()
    {
        enemyType = EnemyCollection.EnemyYellow;
        birthPos = new Vector3[2];
        GetBirthPos();
        base.Start();
        birthTime = Time.time;
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        //可以在编辑器里多次观察运动轨迹的一个开关
        if (StartCalculate) 
        {
            GetBirthPos();
            StartCalculate = false;
            birthTime = Time.time;
        }
        base.Update();
        ChangeForward();
    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime;
        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }
        else 
        {
            SinRes = Mathf.Sin(u * loop * Mathf.PI);
            u1 = u + Speed * sinRes;            
            Vector3 moveAddition = (1-u1) * birthPos[0] + u1 * birthPos[1];
            transform.position = moveAddition;
        }
        
    }

    void GetBirthPos() 
    {
        backMax = BoundsUtility.backBounds.max.y;
        backMin = BoundsUtility.backBounds.min.y;
        float p1 = Random.Range((backMin+backMax)/2, backMax);
        birthPos[0]= new Vector3(BoundsUtility.backBounds.max.x+birthOffset, p1, 0);
        float p2;
        if (p1 > (backMax + backMin) / 4 *3)
        {
            p2 = Random.Range((backMax + backMin) / 4 * 3, backMax);
        }
        else 
        {
            p2 = Random.Range(p1, (backMax + backMin) / 4 * 3);
        }
        birthPos[1] = new Vector3(BoundsUtility.backBounds.min.x-birthOffset, p2, 0);
    }

    void ChangeForward() 
    {
        float temp = sinRes;
        
        
        if (isIncrease)
        {
            this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            //rotation.eulerAngles = new Vector3(0f, 0f, 0f);
            //this.transform.rotation = rotation;
        }
        else 
        {
            this.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            //rotation.eulerAngles = new Vector3(0f, 0f, 180f);
            //this.transform.rotation = rotation;
        }
    }
}
