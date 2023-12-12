using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictrionary;

    private void Awake()
    {
        poolDictrionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictrionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictrionary.ContainsKey(tag))
            return null;

        GameObject obj = poolDictrionary[tag].Dequeue();

        if (obj != null)
        {
            obj.SetActive(true);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            poolDictrionary[tag].Enqueue(obj);
        }

        return obj;
    }
}
