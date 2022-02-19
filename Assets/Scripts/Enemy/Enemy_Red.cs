using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Red :Enemy
{
    //Enemy_01使用的是正弦运动
    //正弦曲线的宽度
    [Header("正弦运动")]
    //做完一个完整周期的正弦曲线的时间
    public float waveFrequency = 2;
    public float waveWidth = 4;
    public float waveRotY = 45;
    public float x0 = -12345;
    public float birthTime;

    // Start is called before the first frame update
    protected override void Start()
    {
        //初始化敌人类型
        enemyType = EnemyCollection.EnemyRed;

        //取得运动之前x的坐标
        x0 = transform.position.x;
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
        Vector3 tempPos = transform.position;
        //基于时间调整theta值
        float existTime = Time.time - birthTime;
        float theta = Mathf.PI * 2 * existTime / waveFrequency;
        float sin = Mathf.Sin(theta);
        //Debug.Log("Theta:"+theta+":"+"Sin:"+sin);
        
        tempPos.x = x0 + waveWidth * sin;
        transform.position = tempPos;
        //绕Y轴转动
        //Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        //this.transform.rotation = Quaternion.Euler(rot);
        base.Move();
    }

}
