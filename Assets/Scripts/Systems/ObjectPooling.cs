using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{

    public Dictionary<string , Stack<GameObject>> objectsDictionary= new Dictionary<string, Stack<GameObject>>();

    public int monstorCount = 0;

    public void AddObjects(string key , GameObject gameobj , int count){

      if(objectsDictionary.ContainsKey(key)){

        for(int i = 0 ; i < count; i++){
         
         GameObject clone = Instantiate(gameobj);
         clone.SetActive(false);
         
         if(clone.GetComponent<ObjectPoolConfig>() == null){

           clone.AddComponent<ObjectPoolConfig>().key = key;

         }


         objectsDictionary[key].Push(clone);


        }
         return;
      }

       objectsDictionary.Add(key , new Stack<GameObject>());
       for(int i = 0 ; i < count; i++){
         
         GameObject clone = Instantiate(gameobj);
         clone.SetActive(false);
         

          if(clone.GetComponent<ObjectPoolConfig>() == null){

           clone.AddComponent<ObjectPoolConfig>().key = key;

         }

         objectsDictionary[key].Push(clone);


        }


    }



    public GameObject ObjectUse(string key){
        
       if(!objectsDictionary.ContainsKey(key) || objectsDictionary[key].Count <= 0)
        {

          return null;
       }

       
        GameObject useObject = objectsDictionary[key].Pop();
        useObject.SetActive(true);
        return useObject;
       
    }

    public void ObjectReturn(string key , GameObject gameobj){
           if(!objectsDictionary.ContainsKey(key)){


               AddObjects(key ,gameobj , 10);
               return;
           }


           
           gameobj.SetActive(false);           
           objectsDictionary[key].Push(gameobj);
           
    }


    public void ObjectPoolingReset(){

          foreach(var element in objectsDictionary){

              ObjectPoolConfig[] objects = FindObjectsOfType<ObjectPoolConfig>();

              for(int i = 0; i < objects.Length; i++){

                   if(element.Key == objects[i].key){

                       ObjectReturn(element.Key , objects[i].gameObject);

                   }

              }
          }


        foreach(var element in objectsDictionary)
        {
            while(element.Value.Count > 0)
            {
                GameObject obj  = element.Value.Pop();
                Destroy(obj);

            }


        }


        objectsDictionary.Clear();

    }



    public void ObjectKeySelectReset(string key)
    {
        if (!objectsDictionary.ContainsKey(key))
        {
            Debug.LogError("오브젝트 풀링 키없음");
            return;

        }


       var objectpoolingStack =  objectsDictionary[key];

       while(objectpoolingStack.Count > 0)
        {
           GameObject gameObj =   objectpoolingStack.Pop();
           Destroy(gameObj);
        }



        objectsDictionary.Remove(key);

    }
}


