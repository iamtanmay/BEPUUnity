using BEPUutilities;
using UnityEngine;
using System.Collections;

public static class BEPUextensions
{
    public static UnityEngine.Vector3 FromBEPU(this BEPUutilities.Vector3 vec)
    {
        return new UnityEngine.Vector3(vec.X, vec.Y, vec.Z);
    }

    public static BEPUutilities.Vector3 ToBEPU(this UnityEngine.Vector3 vec)
    {
        return new BEPUutilities.Vector3(vec.x, vec.y, vec.z);
    }

    public static UnityEngine.Quaternion FromBEPU(this BEPUutilities.Quaternion quaternion)
    {
        return new UnityEngine.Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    }

    public static BEPUutilities.Quaternion ToBEPU(this UnityEngine.Quaternion quaternion)
    {
        return new BEPUutilities.Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
    }
}