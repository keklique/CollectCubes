// PoolManager by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Manager<PoolManager>
{
	
    [Header("Level Design")]
    private int temp1;

    [Space(15)]
    [Header("General Variables")]
    [SerializeField] int poolCount = 150;
    Queue<ICollectable> poolQueue;

    [Space(15)]
    [Header("References")]
    [SerializeField] private Transform poolHolder;
    [SerializeField] private CubeActor cubePrefab;


    #region UNITY_EVENTS
    private void Start()
    {
        InitPool();
    }
    #endregion

    #region EVENTS

    #endregion

    #region PUBLIC_METHODS
    public ICollectable FetchFromPool()
    {
        return poolQueue.Count > 0 ? poolQueue.Dequeue() : null;
    }

    public void AddToPool(ICollectable _collectable)
    {
        if (!poolQueue.Contains(_collectable))
        {
            _collectable.ToPool(poolHolder);
            poolQueue.Enqueue(_collectable);
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void InitPool()
    {
        poolQueue = new Queue<ICollectable>();

        for (int i = 0; i < poolCount; i++)
        {
            CubeActor _cubeActor = Instantiate(cubePrefab, poolHolder);
            _cubeActor.ToPool(poolHolder);
            poolQueue.Enqueue(_cubeActor);
        }
    }
    #endregion
}
