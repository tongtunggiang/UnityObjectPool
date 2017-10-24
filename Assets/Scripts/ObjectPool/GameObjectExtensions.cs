using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static GameObject Spawn(this GameObject prefab, Vector3 worldPosition)
    {
        return PoolManager.Spawn(prefab,
            worldPosition, Quaternion.identity, prefab.transform.localScale,
            null);
    }

    public static GameObject Spawn(this GameObject prefab)
    {
        return PoolManager.Spawn(prefab, 
            Vector3.zero, Quaternion.identity, prefab.transform.localScale,
            null);
    }

    public static GameObject Spawn(this GameObject prefab, Vector3 worldPosition, Transform lookAtTarget)
    {
        return PoolManager.Spawn(prefab,
            worldPosition, Quaternion.identity, prefab.transform.localScale,
            null, lookAtTarget);
    }

    public static GameObject Spawn(this GameObject prefab, Transform parent)
    {
        return PoolManager.Spawn(prefab,
            Vector3.zero, Quaternion.identity, prefab.transform.localScale,
            parent, null, true, true);
    }

    public static GameObject Spawn(this GameObject prefab,
        Vector3 position, Quaternion rotation, Vector3 scale,
        Transform parent = null,
        bool useLocalPosition = false, bool useLocalRotation = false)
    {
        return PoolManager.Spawn(prefab, position, rotation, scale,
            parent, null, useLocalPosition, useLocalRotation);
    }

    public static void Despawn(this GameObject obj, bool surpassWarning = false)
    {
        PoolManager.Kill(obj, surpassWarning);
    }
}
