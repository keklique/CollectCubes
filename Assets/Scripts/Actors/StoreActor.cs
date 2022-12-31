// StoreActor by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreActor : Actor<LevelManager>
{
	
    [Header("Level Design")]
    private int temp1;

    [Space(15)]
    [Header("General Variables")]
    private int temp2;

    [Space(15)]
    [Header("References")]
    private int temp3;


    #region UNITY_EVENTS
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
           if(collectable.GetState() != CollectableState.Passive) collectable.MoveToStore(transform);
        }
    }
    #endregion

    #region EVENTS

    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
