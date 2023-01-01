// StartPanel by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPanel : Actor<UIManager>, IPointerDownHandler
{
	
    #region UNITY_EVENTS
    protected override void Start()
    {
        base.Start();
        InitVaraibles();
    }
    #endregion

    #region EVENTS

    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    private void InitVaraibles()
    {

    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        manager.StartLevel();
    }
    #endregion
}
