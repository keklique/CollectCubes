// CollectorTrigger by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorTrigger : MonoBehaviour
{
	
    [Header("Level Design")]
    private int temp1;

    [Space(15)]
    [Header("General Variables")]
    private int temp2;
    [SerializeField] private string layerMask;

    [Space(15)]
    [Header("References")]
    private int temp3;


    #region UNITY_EVENTS
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            if (collectable.GetState() != CollectableState.Passive)
            {
                collectable.SetLayer(layerMask);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            if (collectable.GetState() != CollectableState.Passive)
            {
                gameObject.layer = LayerMask.NameToLayer("ActiveCube");
            }
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
