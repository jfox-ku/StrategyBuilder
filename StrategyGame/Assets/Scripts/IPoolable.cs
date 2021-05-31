using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    GameObject GetPoolPrefab();
    string PoolName();
    int PoolCount();

}
