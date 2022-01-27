using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float rotationsPerSecond;

    //MeshRenderer->matertial[]
    public Material mat;
    public MeshRenderer meshRenderer;
    public bool _____________________;

    int curLevel;
    public int LevelShown;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();        
        mat = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //读取Spike单例对象的当前护盾等级
        curLevel =  Mathf.FloorToInt(Spike.S.shieldLevel);

        if (LevelShown!=curLevel) 
        {
            LevelShown = curLevel;
            
            mat.mainTextureOffset = new Vector2(0.2f * LevelShown, 0);
        }

        //让护盾旋转
        float rZ = (rotationsPerSecond * Time.time*360)%360f;        
        //将rotation.z限制在0-360之间
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
