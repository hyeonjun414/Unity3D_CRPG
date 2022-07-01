using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [Header("오브젝트 풀")]
    public Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();
    public ObjectPool poolObj;
    public GameObject poolPos;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    // 오브젝트를 사용 요청
    public GameObject UseObj(GameObject obj)
    {
        // 오브젝트풀 매니저에 등록된 오브젝트가 아니라면
        //print(obj.name);
        if(!pools.ContainsKey(obj.name))
        {
            // 해당 오브젝트의 풀을 만들어준다.
            ObjectPool pool = Instantiate(poolObj);
            // 오브젝트풀이 생성할 오브젝트를 지정
            // 생성된 오브젝트풀의 부모를 풀이 모여있는 곳으로 지정 ( 한눈에 볼 수 있도록 )
            pool.transform.SetParent(poolPos.transform, true);
            pool.SetUp(obj);
            // 딕셔너리에 해당 풀을 오브젝트의 이름을 키로 두어 추가해줌.
            pools.Add(obj.name, pool);
        }
        // 해당 오브젝트 이름에 해당하는 풀을 찾아 오브젝트를 하나 꺼내온다.
        return pools[obj.name].Dequeue();

    }
    // 사용된 오브젝트 반환
    public void ReturnObj(GameObject obj)
    {
        ObjectKey key = obj.GetComponent<ObjectKey>();
        // 반환되는 오브젝트를 
        pools[key.objKey].Enqueue(obj);
    }
}
