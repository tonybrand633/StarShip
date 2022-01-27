using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type;
    public string letter;
    public Color color;
    public GameObject Projectile;

    public float DamageOnHit;
    public float DelayBetweenShot;
    public float velocity;
}

public enum WeaponType
{
    none,
    ammoUp,
    single,
    spread,//两发炮弹
    triple,
    shiled
}
