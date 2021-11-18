using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private WeaponType _type;

    BoxCollider2D col;


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

    public void SetType(WeaponType eType) 
    {
        _type = eType;
        WeaponDefinition def = GameManager.GetWeaponDefinition(_type);
        
        //还可以进行子弹的相关操作
    }

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        type = WeaponType.triple;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOffScreen();              
    }

    void CheckOffScreen() 
    {
        if (BoundsUtility.ScreenBoundsCheck(col.bounds, BoundsUtility.BoundTest.offScreen) != Vector3.zero)
        {
            Destroy(this.gameObject);
        }
    }
}
