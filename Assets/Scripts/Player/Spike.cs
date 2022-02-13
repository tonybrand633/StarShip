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

    //武器信息
    public bool _________________________;
    public Weapon weapon;

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

    void Awake()
    {
        S = this;
        bounds = BoundsUtility.CombineBoundsOfChildren(this.gameObject);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        //重置武器
        ClearWeapon();
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
        Vector3 offset = BoundsUtility.BackBoundsCheck(bounds);
        if (offset != Vector3.zero)
        {
            pos -= offset;
            transform.position = pos;
        }

        if (Input.GetAxis("Jump")==1&&fireDelegate!=null) 
        {
            fireDelegate();
        }
        weapon.type = GetComponentInChildren<Weapon>().type;
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
            GameManager.S.PlayExplosionAnim(other.gameObject);
            Destroy(other.gameObject);
        }
        //else if (other.tag == "PowerUp")
        //{
        //    Debug.Log("Absorb");
        //    AbsorbPowerUp(other.gameObject);
        //}
        else if (other.tag == "Coins") 
        {
            CollectCoins(other.gameObject);
        }
        else
        {
            Debug.Log(other.gameObject.name);
        }
    }

    public void WeaponChange(GameObject go) 
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type) 
        {
            case WeaponType.shiled:
                shieldLevel++;
                break;
            case WeaponType.single:
                if (weapon!=null) 
                {
                    weapon.type = WeaponType.single;                    
                }
                break;
            case WeaponType.spread:
                if (weapon!=null) 
                {
                    weapon.type = WeaponType.spread;                    
                }
                break;
            case WeaponType.triple:
                if (weapon!=null) 
                {
                    weapon.type = WeaponType.triple;
                }
                break;
        }
        pu.AbsoredBy(this.gameObject);
    }

    void CollectCoins(GameObject go) 
    {
        CoinGame coinGame = go.GetComponent<CoinGame>();
        coinGame.Absorb();
        GameManager.S.coinsCollect++;
        
    }

    void ClearWeapon() 
    {
        weapon.SetType(WeaponType.single);
    }
}
