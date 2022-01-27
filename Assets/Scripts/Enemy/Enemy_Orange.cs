using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Orange : Enemy
{
    //Enemy_03使用的是贝塞尔曲线运动
    [Header("贝塞尔运动")]
    public Vector3[] points;
    public float birthTime;
    public float LifeTime;
   
    // Start is called before the first frame update
    protected override void Start()
    {
        //初始化敌人类型
        enemyType = EnemyCollection.EnemyOrange;

        //初始化运动所需的点points[0]为本身的位置
        points = new Vector3[3];
        points[0] = transform.position;

        float MaxX = BoundsUtility.backBounds.max.x;
        float MinX = BoundsUtility.backBounds.min.x;

        Vector3 v;
        //points[1]为屏幕下方的一点
        v = Vector3.zero;
        v.x = Random.Range(MinX, MaxX);
        v.y = BoundsUtility.backBounds.min.y-GameManager.S.SpawnPadding;
        points[1] = v;

        //points[2]为屏幕上方的一点
        v = Vector3.zero;
        v.x = Random.Range(MinX, MaxX);
        v.y = BoundsUtility.backBounds.max.y;
        points[2] = v;
        birthTime = Time.time;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / LifeTime;
        if (u>1) 
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 p01, p12, p012;
        //u = u - 0.4f * Mathf.Sin(u * Mathf.PI * 2);
        //u = u-0.2f*Mathf.Sin(u*Mathf.PI*2);
        p01 = (1 - u) * points[0] + u * points[1];
        p12 = (1 - u) * points[1] + u * points[2];

        p012 = (1 - u) * p01 + u * p12;
        
        transform.position = p012;
        ChangeEularAngle(u);
    }

    public void ChangeEularAngle(float u ) 
    {
        if (u < 0.5f)
        {
            this.transform.eulerAngles = new Vector3(0f, 0f, -180f);
        }
        else 
        {
            this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}
