// Actor by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor<T> : MonoBehaviour where T : Manager<T>
{

    public T manager;
    #region UNITY_EVENTS
    protected virtual void Start()
    {
        manager = Manager<T>.Instance;
    }
    #endregion

    #region EVENTS

    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
