using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityOverloads
{
    public static bool Vec3Equality(Vector3 v1, Vector3 v2)
    {
        {
            if (v1.x == v2.x)
            {
                if (v1.y == v2.y)
                {
                    if (v1.z == v2.z)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}