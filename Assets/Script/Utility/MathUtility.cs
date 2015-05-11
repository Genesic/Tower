using UnityEngine;
using System.Collections;

public static class MathUtility
{
    public static Vector3 GetRandomRadiusPoint(float radius)
    {
        var angle = Random.Range(0f, 360f);
        var x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        var z = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        return new Vector3(x, 0f, z);
    }

}
