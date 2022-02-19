using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    BoxCollider2D col;
    public Rigidbody2D rig;
    [SerializeField]
    private WeaponType _type;

    public WeaponType type
    {
        get
        {
            return _type;
        }
        set
        {
            SetType(value);
        }
    }
    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        CheckOffScreen();
    }

    public void SetType(WeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = GameManager.GetWeaponDefinition(_type);

        //还可以进行子弹的相关操作
    }
    void CheckOffScreen()
    {
        if (BoundsUtility.ScreenBoundsCheck(col.bounds, BoundsUtility.BoundTest.offScreen) != Vector3.zero)
        {
            Destroy(this.gameObject);
        }
    }
}
