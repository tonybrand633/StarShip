using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsUtility : MonoBehaviour
{

    //===================================BoundsTest==========================================
    public enum BoundTest
    {
        center, //游戏对象中心是否位于屏幕
        onScreen,   //游戏对象是否完全位于屏幕之中
        offScreen  //游戏对象是否完全位于屏幕之外
    }


    //===================================Bounds函数==========================================
    //接收两个Bounds变量，返回包含这两个变量的新Bounds
    public static Bounds BoundsUnion(Bounds b1, Bounds b2)
    {
        if (b1.size == Vector3.zero && b2.size != Vector3.zero)
        {
            return b2;
        } else if (b1.size != Vector3.zero && b2.size == Vector3.zero)
        {
            return b1;
        } else if (b1.size == Vector3.zero && b2.size == Vector3.zero)
        {
            return b1;
        }

        //扩展b1，使其完全包含b2
        b1.Encapsulate(b2.min);
        b1.Encapsulate(b2.max);
        return b1;
    }
    public static Bounds CombineBoundsOfChildren(GameObject go)
    {
        Bounds b = new Bounds(Vector3.zero, Vector3.zero);

        //此处是根据Renderer大小来决定Bounds
        //if (go.GetComponent<Renderer>()!=null) 
        //{
        //    b = BoundsUnion(b, go.GetComponent<Renderer>().bounds);
        //}
        if (go.GetComponent<Collider2D>() != null)
        {
            b = BoundsUnion(b, go.GetComponent<Collider2D>().bounds);
        }

        foreach (Transform t in go.transform)
        {
            b = BoundsUnion(b, CombineBoundsOfChildren(t.gameObject));
        }

        return b;
    }

    //返回单个BoxCollider的Bounds
    public static Bounds BoundsSingle(GameObject go)
    {
        Bounds bounds = go.GetComponent<Collider2D>().bounds;
        return bounds;
    }

    //创建相机的Bounds属性
    static Bounds _camBounds;
    static Bounds _backBounds;

    public static Bounds backBounds 
    {
        get 
        {
            if (_backBounds.size==Vector3.zero) 
            {
                SetBackBounds();
            }
            return _backBounds;
        }
    }
    public static Bounds camBounds
    {
        get
        {
            if (_camBounds.size == Vector3.zero)
            {
                SetCameraBounds();
            }
            return _camBounds;
        }
    }

    public static void SetBackBounds(Camera cam = null)
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        //根据左上角和右下角坐标创建两个三维向量
        Vector3 topRight = new Vector3(0, 0, 0);
        Vector3 bottomLeft = new Vector3(Screen.width/2, Screen.height, 0);

        //把这两个三维向量的坐标从屏幕坐标转为世界坐标
        Vector3 topTO = cam.ScreenToWorldPoint(topRight);
        Vector3 bottomBO = cam.ScreenToWorldPoint(bottomLeft);

        //找到相机的中点
        topTO.z += cam.nearClipPlane;
        bottomBO.z += cam.farClipPlane;
        Vector3 center = (topTO + bottomBO) / 2;
        _backBounds = new Bounds(center, Vector3.zero);
        //使得相机的边界框扩展到对应的点
        _backBounds.Encapsulate(topTO);
        _backBounds.Encapsulate(bottomBO);

    }

    //设置相机的Bounds属性
    public static void SetCameraBounds(Camera cam = null)
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        //根据左上角和右下角坐标创建两个三维向量
        Vector3 topRight = new Vector3(0, 0, 0);
        Vector3 bottomLeft = new Vector3(Screen.width, Screen.height, 0);

        //把这两个三维向量的坐标从屏幕坐标转为世界坐标
        Vector3 topTO = cam.ScreenToWorldPoint(topRight);
        Vector3 bottomBO = cam.ScreenToWorldPoint(bottomLeft);

        //找到相机的中点
        topTO.z += cam.nearClipPlane;
        bottomBO.z += cam.farClipPlane;
        Vector3 center = (topTO + bottomBO) / 2;
        _camBounds = new Bounds(center, Vector3.zero);
        //使得相机的边界框扩展到对应的点
        _camBounds.Encapsulate(topTO);
        _camBounds.Encapsulate(bottomBO);

    }

    //外界调用方法，检查传入物体Bounds与相机的Bounds的关系
    public static Vector3 ScreenBoundsCheck(Bounds bnd, BoundTest test = BoundTest.onScreen)
    {
        return BoundInBoundCheck(camBounds, bnd);
    }

    public static Vector3 BackBoundsCheck(Bounds bnd, BoundTest test = BoundTest.onScreen) 
    {
        return BoundInBoundCheck(backBounds, bnd);
    }

    //根据枚举的情况检查边界框bnd是否位于camBounds之内
    public static Vector3 BoundInBoundCheck(Bounds big, Bounds lib, BoundTest test = BoundTest.onScreen)
    {
        //获取被检测物体的中心
        Vector3 centerPos = lib.center;

        Vector3 offset = Vector3.zero;


        switch (test)
        {
            //如果onScreen，需要把lib整体移动到big之内
            case BoundTest.onScreen:
                if (big.Contains(lib.max) && big.Contains(lib.min))
                {
                    return Vector3.zero;
                }
                if (lib.max.x > big.max.x)
                {
                    offset.x = lib.max.x - big.max.x;
                }
                else if (lib.min.x < big.min.x)
                {
                    offset.x = lib.min.x - big.min.x;
                }
                if (lib.max.y > big.max.y)
                {
                    offset.y = lib.max.y - big.max.y;
                }
                else if (lib.min.y < big.min.y)
                {
                    offset.y = lib.min.y - big.min.y;
                }
                if (lib.max.z > big.max.z)
                {
                    offset.z = lib.max.z - big.max.z;
                }
                else if (lib.min.z < big.min.z)
                {
                    offset.z = lib.min.z - big.min.z;
                }
                return offset;

            //如果offScreen，offset则是确认需要把lib整体移动到big之内需要的距离
            case BoundTest.offScreen:
                bool cMax = big.Contains(lib.max);
                bool cMin = big.Contains(lib.min);
                if (cMax || cMin)
                {
                    return Vector3.zero;
                }
                if (big.max.x < lib.min.x)
                {
                    offset.x = lib.min.x - big.min.x;
                }
                else if (big.min.x > lib.max.x)
                {
                    offset.x = big.min.x - lib.max.x;
                }
                if (big.max.y < lib.min.y)
                {
                    offset.y = lib.min.y - big.min.y;
                }
                else if (big.min.y > lib.max.y)
                {
                    offset.y = big.min.y - lib.max.y;
                }
                if (big.max.z < lib.min.z)
                {
                    offset.z = lib.min.z - big.min.z;
                }
                else if (big.min.z > lib.max.z)
                {
                    offset.z = big.min.z - lib.max.z;
                }
                return offset;

            //如果为center
            case BoundTest.center:
                return Vector3.zero;
        }

        return offset;
    }

    //物体是否进入相机内
    public static bool isInCamera(Bounds bounds)
    {
        if (camBounds.Contains(bounds.max) || camBounds.Contains(bounds.min))
        {
            return true;
        }        
        return false;
    }

    //根据物体信息自动寻找父物体
    public static GameObject FindParent(GameObject go)
    {
        if (go.transform.parent == null)
        {
            return go;
        }
        else
        {
            go = go.transform.parent.gameObject;
        }
        return FindParent(go);
    }



}
