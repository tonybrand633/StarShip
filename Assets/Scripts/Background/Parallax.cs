﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject poi;
    public GameObject[] panels;
    public float scrollSpeed = -30f;
    //motionMult 变量控制前景画面对玩家运动的反馈程度
    public float motionMult = 0.25f;

    private float panelHt;  //每个前景画面的高度
    private float depth;    //前景画面的深度

    // Start is called before the first frame update
    void Start()
    {
        panelHt = panels[0].transform.localScale.y;
        depth = panels[0].transform.position.z;
        //设置前面画面的初始位置
        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, panelHt, depth);
    }

    // Update is called once per frame
    void Update()
    {
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % panelHt + (panelHt * 0.5f);
        //if (poi!=null) 
        //{
        //    tX = -poi.transform.position.x * motionMult;
        //}
        //设置panels[0]位置
        panels[0].transform.position = new Vector3(tX, tY, depth);
        //在必要的时候设置Panel[1]位置，使得背景连续
        if (tY >= 0)
        {
            panels[1].transform.position = new Vector3(tX, tY - panelHt, depth);
        }
        else 
        {
            panels[1].transform.position = new Vector3(tX, tY + panelHt, depth);
        }
    }
}
