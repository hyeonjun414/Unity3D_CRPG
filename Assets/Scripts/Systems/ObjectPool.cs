using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject poolObj;
    private string objKey;
    private Queue<GameObject> objQueue = new Queue<GameObject>();

    public void SetUp(GameObject obj)
    {
        gameObject.name = obj.name + " Pool";
        poolObj = obj;
        objKey = obj.name;

        GameObject go = Instantiate(poolObj);
        go.AddComponent<ObjectKey>().SetUp(objKey);
        objQueue.Enqueue(go);
    }
    public GameObject Dequeue()
    {
        GameObject go = null;
        if (objQueue.Count == 0)
        {
            go = Instantiate(poolObj);
            go.AddComponent<ObjectKey>().SetUp(objKey);
            objQueue.Enqueue(go);
        }
        go = objQueue.Dequeue();
        go.SetActive(true);
        return go;
    }
    public void Enqueue(GameObject obj)
    {
        obj.transform.SetParent(transform, false);
        obj.transform.position = Vector3.down * 10f;
        objQueue.Enqueue(obj);
    }
}
