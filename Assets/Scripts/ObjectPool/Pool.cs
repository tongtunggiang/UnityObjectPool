using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private const string ON_PRE_DISABLE_METHOD = "OnPreDisable";
    private const string ON_SPAWN_METHOD = "OnSpawn";

    [SerializeField]
    GameObject prefab;

    [SerializeField]
    int initialPoolsize = 10;

    Stack<GameObject> pooledInstances = new Stack<GameObject>();
    List<GameObject> aliveInstances = new List<GameObject>();

    public GameObject Prefab { get { return prefab; } }

    void Awake()
    {
        for (int i = 0; i < initialPoolsize; i++)
        {
            GameObject instance = Instantiate(prefab);
            instance.transform.SetParent(transform);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localScale = Vector3.one;
            instance.transform.localEulerAngles = Vector3.zero;

            instance.SendMessage(ON_PRE_DISABLE_METHOD, SendMessageOptions.DontRequireReceiver);

            instance.SetActive(false);

            pooledInstances.Push(instance);
        }
    }

    /// <summary>
    /// Bring a new game object to life by taking a dead one from
    /// the waiting pool. If all objects in the pool are alive, 
    /// create a new one and add it to the pool.
    /// </summary>
    public GameObject Spawn(Vector3 position, 
        Quaternion rotation, 
        Vector3 scale, 
        Transform parent = null,
        Transform lookAtTarget = null,
        bool useLocalPosition = false,
        bool useLocalRotation = false)
    {
        if (pooledInstances.Count <= 0) // Every game object has been spawned!
        {
            GameObject freshObject = Instantiate(prefab);
            pooledInstances.Push(freshObject);
        }

        GameObject obj = pooledInstances.Pop();

        obj.transform.SetParent(parent);

        if (useLocalPosition)
            obj.transform.localPosition = position;
        else
            obj.transform.position = position;

        if (lookAtTarget != null)
        {
            obj.transform.LookAt(lookAtTarget);
        }
        else
        {
            if (useLocalRotation)
                obj.transform.localRotation = rotation;
            else
                obj.transform.rotation = rotation;
        }
        obj.transform.localScale = scale;

        obj.SetActive(true);

        aliveInstances.Add(obj);
        obj.SendMessage(ON_SPAWN_METHOD, SendMessageOptions.DontRequireReceiver);

        return obj;
    }

    /// <summary>
    /// Deactivate an object and add it back to the pool, given that it's
    /// in alive objects array.
    /// </summary>
    /// <param name="obj"></param>
    public void Kill(GameObject obj)
    {
        int index = aliveInstances.FindIndex(o => obj == o);
        if (index == -1)
        {
            Destroy(obj);
            return;
        }

        obj.SendMessage("OnPreDisable", SendMessageOptions.DontRequireReceiver);

        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.localEulerAngles = Vector3.zero;
        
        pooledInstances.Push(obj);
        aliveInstances.RemoveAt(index);

        obj.SetActive(false);
    }

    public bool IsResponsibleForObject(GameObject obj)
    {
        int index = aliveInstances.FindIndex(o => obj == o);
        if (index == -1)
        {
            return false;
        }

        return true;
    }
}
