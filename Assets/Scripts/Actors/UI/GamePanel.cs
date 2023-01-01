// GamePanel by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : Actor<UIManager>
{

    [Header("General Variables")]
    public PanelType panelType;

    [Space(15)]
    [Header("References")]
    private int temp3;


    #region UNITY_EVENTS

    protected override void Start()
    {
        base.Start();
        manager.OnPanelTypeChanged += OnPanelTypeChanged;
        if (panelType != PanelType.Loading) gameObject.SetActive(false);
    }
    #endregion

    #region EVENTS
    private void OnPanelTypeChanged(object sender, UIManager.OnPanelTypeChangedArgs e)
    {
        if(e.currentPanelType == panelType)
        {
            gameObject.SetActive(true);
        }else
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
