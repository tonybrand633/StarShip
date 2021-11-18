using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

}

public enum WeaponType
{
    single,
    spread,//两发炮弹
    triple
}

[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type;
    public string letter;
    public Color color;
    public GameObject Projectile;

    public float DamageOnHit;
    public float velocity;
}


