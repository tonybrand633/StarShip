using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazierTraverse
{
    //因为Unity自带的Lerp函数不支持外插，所以在这里自己定义
    static public Vector3 Lerp(Vector3 from, Vector3 to, float u)
    {
        Vector3 res = (1 - u) * from + u * to;
        return res;
    }

    static public Vector3 Bazier(float u,List<Vector3>vList) 
    {
        if (vList.Count==1) 
        {
            return vList[0];
        }
        List<Vector3> vListR = vList.GetRange(1, vList.Count - 1);

        List<Vector3> vListL = vList.GetRange(0, vList.Count - 1);

        Vector3 res = Lerp(Bazier(u, vListL), Bazier(u, vListR), u);
        return res;
    }
}
