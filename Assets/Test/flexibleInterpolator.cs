using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EasingType 
{
    linear,
    easeIn,
    easeOut,
    easeInOut,
    sin,
    sinIn,
    sinOut
}

public class flexibleInterpolator : MonoBehaviour
{
    public Transform c0, c1;
    public float uMin = 0;
    public float uMax = 1;
    public float u;
    public float timeDuration = 1;

    public EasingType easingType = EasingType.linear;
    public float easingMod = 2;

    //产生周期移动
    public bool loopMove = true;


    //设置checkToCalculate为true开始移动
    public bool checkToCalculate = false;

    public bool __________________;

    public Vector3 p01;
    public Color c01;
    public Quaternion r01;
    public Vector3 s01;
    public bool moving = false;
    public float timeStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //相当于一个控制器的开关
        if (checkToCalculate)
        {
            checkToCalculate = false;
            moving = true;
            timeStart = Time.time;
        }

        if (moving)
        {
            u = (Time.time - timeStart) / timeDuration;
            if (u >= 1)
            {
                u = 1;
                if (loopMove)
                {
                    timeStart = Time.time;
                }
                else 
                {
                    moving = false;
                }
                moving = false;
            }

            //调整u的范围为uMin到uMax;
            //相当于给了一个范围，并且根据这个范围，让新的u在这个范围里生成
            //注：随时间增加的u范围是0~1
            u = (1 - u) * uMin + u * uMax;

            u = EaseU(u, easingType, easingMod);

            //标准线性插值函数
            p01 = (1 - u) * c0.position + u * c1.position;
            c01 = (1 - u) * c0.GetComponent<Renderer>().material.color + u * c1.GetComponent<Renderer>().material.color;
            s01 = (1 - u) * c0.localScale + u * c1.localScale;

            //旋转的有一些不同
            r01 = Quaternion.Slerp(c0.rotation, c1.rotation, u);

            transform.position = p01;
            this.GetComponent<Renderer>().material.color = c01;
            transform.localScale = s01;

            transform.rotation = r01;
        }
    }

    public float EaseU(float u,EasingType eType,float eMod) 
    {
        float u2 = u;
        switch (eType) 
        {
            case EasingType.linear:
                u2 = u;
                break;

            case EasingType.easeIn:
                u2 = Mathf.Pow(u, eMod);
                break;

            case EasingType.easeOut:
                u2 = 1 - Mathf.Pow(1 - u, eMod);
                break;

            case EasingType.easeInOut:
                if (u <= 0.5f)
                {
                    u2 = 0.5f * Mathf.Pow(u * 2, eMod);
                }
                else 
                {
                    u2 = 0.5f + 0.5f * (1 - Mathf.Pow(1 - (2 * (u - 0.5f)), eMod));
                }
                break;

            case EasingType.sin:
                //设eMod的值为0.16f并且EasingType.sin为0.2f
                u2 = u + eMod * Mathf.Sin(2*u*Mathf.PI);
                break;

            case EasingType.sinIn:
                //eMod在这里被忽略
                u2 = 1 - Mathf.Cos(u * Mathf.PI * 0.5f);
                break;

            case EasingType.sinOut:
                //eMod在这里被忽略
                u2 = Mathf.Sin(u * Mathf.PI * 0.5f);
                break;
        }
        return u2;
    }
}
