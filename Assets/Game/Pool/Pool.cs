using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private T prefab;
    private List<T> pool;
    private Transform poolParent;

    public Pool(T prefab, int poolLenght, Transform poolParent = null)
    {
        this.prefab = prefab;
        this.poolParent = poolParent;
        pool = new List<T>();

        for (int i = 0; i < poolLenght; i++)
        {
            CreateObject(prefab);
        }
    }

    public T GetObject()
    {
        var obj = pool.FirstOrDefault(x => !x.isActiveAndEnabled);

        if (obj == null)
            obj = CreateObject(prefab);

        return obj;
    }

    private T CreateObject(T prefab)
    {
        var obj = GameObject.Instantiate(prefab);
        obj.gameObject.SetActive(false);
        if (poolParent != null)
            obj.transform.SetParent(poolParent);
        pool.Add(obj);
        return obj;
    }
}
