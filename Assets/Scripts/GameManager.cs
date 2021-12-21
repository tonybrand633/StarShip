using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager S;
    public static Dictionary<WeaponType, WeaponDefinition> W_DEFS;
    [Header("游戏控制")]
    public float SpawnRate;
    public float SpawnPadding;
    public float RestartTime;
    public GameObject[]prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[] { WeaponType.ammoUp, WeaponType.shiled };


    public GameObject[] enemyObjects;

    Bounds camBounds;
    Bounds backBounds;

    [Header("武器控制")]
    public WeaponDefinition[] weaponDefinitions;
    public WeaponType[] activeWeaponTypes;


    private void Awake()
    {
        S = this;
        W_DEFS = new Dictionary<WeaponType, WeaponDefinition>();
        foreach  (WeaponDefinition def in weaponDefinitions)
        {
            W_DEFS[def.type] = def;
        }
        backBounds = BoundsUtility.backBounds;
        //camBounds = BoundsUtility.camBounds;
        SpawnEnemy();
    }

    // Start is called before the first frame update
    void Start()
    {
        activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
        for (int i = 0; i < activeWeaponTypes.Length; i++)
        {
            activeWeaponTypes[i] = weaponDefinitions[i].type;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public static WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        //检查对应的key是否存在于字典内
        if (W_DEFS.ContainsKey(wt))
        {
            return W_DEFS[wt];
        }
        //如果不存在，将会返回一个WeaponType.none
        else
        {
            return new WeaponDefinition();
        }
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, enemyObjects.Length);
        GameObject go = Instantiate(enemyObjects[index]);
        string name = go.name;
        float xRange;
        float yRange;
        if (go.GetComponent<Enemy>().GetEnemyType()==EnemyCollection.EnemyRed)
        {
            xRange = Random.Range(backBounds.min.x + 4f, backBounds.max.x - 4f);            
            yRange = backBounds.max.y + SpawnPadding;
        }
        else 
        {
            xRange = Random.Range(backBounds.min.x + 0.4f, backBounds.max.x - 0.4f);
            yRange = backBounds.max.y + SpawnPadding;
        }
        
        
        Vector3 pos;
        pos = new Vector3(xRange, yRange, 0);
        go.transform.position = pos;
        Invoke("SpawnEnemy", SpawnRate);
    }

    void Restart() 
    {
        SceneManager.LoadScene("Scene_OneLevel");
    }

    public void DelayedRestart() 
    {
        Invoke("Restart", RestartTime);
    }

    public void ShipDestoryed(Enemy e) 
    {
        if (Random.value<=e.powerUpDropChance) 
        {
            //在这里Random.value生成一个0到1之间的数字
            int indx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[indx];
            //生成道具
            GameObject go = Instantiate(prefabPowerUp[indx]) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            //设置正确的武器类型
            pu.SetType(puType);
            //将其位置摆放在消灭敌人飞机的位置
            pu.transform.position = e.transform.position;        
        }
    }
}
