using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    //Set prefabs in the inspector
    public List<GameObject> ObjectsToPool;

    //Instances of blueprints for easy access
    public Dictionary<string,IPoolable> TempDict;

    Dictionary<string, Stack<GameObject>> StackPoolDict;

    private void Start() {
        
        StackPoolDict = new Dictionary<string, Stack<GameObject>>();
        TempDict = new Dictionary<string, IPoolable>();

        foreach (var prefab in ObjectsToPool) {
            //Need a temporary instance since interfaces can't be serialized
            var temp = Instantiate(prefab,this.transform);
            temp.gameObject.SetActive(false);
            temp.gameObject.name += "_blueprint";

            var item = temp.GetComponent<IPoolable>();
            Debug.Log(item.PoolName());

            StackPoolDict.Add(item.PoolName(),new Stack<GameObject>());
            TempDict.Add(item.PoolName(),item);

            Populate(item);
            
        }
    }




    private void Populate(IPoolable poolable) {
        var prefab = poolable.GetPoolPrefab();
        var pool = StackPoolDict[poolable.PoolName()];
        if(pool == null) StackPoolDict.Add(poolable.PoolName(), new Stack<GameObject>());


        for (int i = 0; i < poolable.PoolCount(); i++) {
            var t = Instantiate(prefab,this.transform);
            t.SetActive(false);
            pool.Push(t);


        }
    }

    public GameObject GetFromPoolString(string poolName) {
        var stack = StackPoolDict[poolName];
        if (stack != null && stack.Count != 0) {
            var ret = stack.Pop();
            ret.gameObject.SetActive(true);
            return ret;
        }
        return Instantiate(TempDict[poolName].GetPoolPrefab(), this.transform);
    }

    public GameObject GetFromPool(IPoolable poolable) {
        var stack = StackPoolDict[poolable.PoolName()];
        if (stack != null && stack.Count != 0) {
            var ret = stack.Pop();
            return ret;
        }
        return Instantiate(poolable.GetPoolPrefab(),this.transform);
    }

    public void ReturnToPool(GameObject obj,IPoolable pool) {
        var stack = StackPoolDict[pool.PoolName()];
        if (stack != null) {
            obj.SetActive(false);
            stack.Push(obj);
        }
    }
}
