using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclic : MonoBehaviour
{
    public float radians;
    public float theta = 0;
    public float moveWidthX;
    public float moveWidthY;

    public bool showCosX = false;
    public bool showSinY = false;

    public bool _____________;

    public Vector3 pos;
    public Color[] colors;

    [Header("Index")]
    public float cIndexFloat;
    public int cIndex;
    public float cU;

    void Awake()
    {
        colors = new Color[]
        {
            new Color(1f,0f,0f),
            new Color(1f,0.5f,0f),
            new Color(1f,1f,0f),
            new Color(0.5f,1f,0f),
            new Color(0f,1f,0f),
            new Color(0f,1f,0.5f),
            new Color(0f,1f,1f),
            new Color(0f,0.5f,1f),
            new Color(1f,0f,1f),
            new Color(1f,0f,0.5f),
            new Color(1f,0f,0f),
        };    
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(1.1f % 1);
    }

    // Update is called once per frame
    void Update()
    {
        radians = Time.time * Mathf.PI;
        theta = Mathf.Round(radians * Mathf.Rad2Deg) % 360;

        pos = Vector3.zero;

        pos.x = Mathf.Cos(radians)*moveWidthX;
        pos.y = Mathf.Sin(radians)*moveWidthY;

        Vector3 tPos = Vector3.zero;
        if (showCosX) tPos.x = pos.x;
        if (showSinY) tPos.y = pos.y;
        transform.position = tPos;
        
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        //根据在圆圈中的位置选择颜色
        cIndexFloat = (theta / 180f)%1f*(colors.Length-1);
        cIndex = Mathf.FloorToInt(cIndexFloat);
        cU = cIndexFloat % 1.0f;

        Gizmos.color = Color.Lerp(colors[cIndex], colors[cIndex + 1], cU);

        Vector3 cosPos = new Vector3(pos.x, (theta / 360f), 0);
        Gizmos.DrawSphere(cosPos, 0.1f);
        if (showCosX) 
        {
            Gizmos.DrawLine(cosPos, transform.position);
        }
    }
}
