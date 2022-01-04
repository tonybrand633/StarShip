using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Part 
{
    
}

public class Enemy_04 : Enemy
{    
    [Header("循环运动")]
    public Vector3[] points;
    public float StartTime;
    public float DurationTime;
    public float selfLength;
    public float u;

    // Start is called before the first frame update
    protected override void Start()
    {
        points = new Vector3[2];
        enemyType = EnemyCollection.EnemyPurple;
        points[0] = new Vector3(BoundsUtility.backBounds.max.x,BoundsUtility.backBounds.max.y);
        points[1] = transform.position;
        InitMove();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
            u = 0;
        }

        transform.position = (1 - u) * points[0] + u * points[1];

    }

}
