using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Vector3Extension
{

    public static Vector3 Rotate(this Vector3 v, float degrees)
    {
      /*  float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return new Vector3(cos * tx - sin * ty, sin * tx + cos * ty, 0);*/
        
        return Quaternion.Euler(0, 0, degrees) * v;
    }
}
