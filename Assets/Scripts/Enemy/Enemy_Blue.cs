﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Blue : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        enemyType = EnemyCollection.EnemyBlue;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
