using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public delegate void WeaponFireDelegate();

    public WeaponFireDelegate fireDelegate;
    //单例对象
    public static Spike S;

    //边界框
    Bounds bounds;

    //飞船参数
    [Header("ParametersForShip")]
    public float Speed;

    //For 3D
    //public float rollMult;
    //public float pitchMult;

    //飞船状态信息
    [Header("StateForShip")]
    float _shieldLevel = 1;

    public float shieldLevel 
    {
        get 
        {
            return _shieldLevel;
        }
        set 
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value<0)
            {
                Destroy(this.gameObject);
                GameManager.S.DelayedRestart();
            }
        }
    }


    public bool _______________________________________;

    void Awake()
    {
        S = this;
        bounds = BoundsUtility.CombineBoundsOfChildren(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

            
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * Speed * Time.deltaTime;
        pos.y += yAxis * Speed * Time.deltaTime;

        transform.position = pos;

        //Bounds检测使飞船保持在屏幕内,bounds不会随着位置实时更新
        bounds.center = transform.position;
        Vector3 offset = BoundsUtility.ScreenBoundsCheck(bounds);
        if (offset != Vector3.zero)
        {
            pos -= offset;
            transform.position = pos;
        }

        if (Input.GetAxis("Jump")==1&&fireDelegate!=null) 
        {
            fireDelegate();
        }
    }

    //防止误撞，防止一个敌人对玩家造成多次伤害
    public GameObject LastTriggerGameObject;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (LastTriggerGameObject == other.gameObject) 
        {
            return;
        }
        LastTriggerGameObject = other.gameObject;

        if (other.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(other.gameObject);
        }
        if (other.tag=="PowerUp") 
        {
            
        }
        else
        {
            Debug.Log(other.gameObject.name);
        }
    }
}
