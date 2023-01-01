// Timer by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : Actor<UIManager>
{
	
    [Header("Level Design")]
    private int temp1;

    [Space(15)]
    [Header("General Variables")]
    private float time;
    private bool istimerWork;

    [Space(15)]
    [Header("References")]
    [SerializeField] private TMPro.TMP_Text textTimer;
    [SerializeField] private GameObject timerObject;


    #region UNITY_EVENTS

    protected override void Start()
    {
        base.Start();
        manager.OnOnTimerInit += OnOnTimerInit;
        manager.OnPanelTypeChanged += OnPanelTypeChanged;
    }
    private void Update()
    {
        TimeCount();
    }
    #endregion

    #region EVENTS
    private void OnOnTimerInit(object sender, UIManager.OnTimerInitArgs e)
    {
        if(e.isTimer)
        {
            timerObject.SetActive(true);
            time = e.level.Duration;
        }
        else
        {
            timerObject.SetActive(false);
            time = 0;
        }
    }

    private void OnPanelTypeChanged(object sender, UIManager.OnPanelTypeChangedArgs e)
    {
        if (e.currentPanelType == PanelType.InGame)
        {
            istimerWork = true;
        }
        else
        {
            istimerWork = false;
        }
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS
    private void TimeCount()
    {
        if (!istimerWork) return;
        if (time <= 0)
        {
            textTimer.text = ".00";
        }
        else
        {
            time -= Time.deltaTime;
            textTimer.text = time.ToString("#.00");
        }
        
    }
    #endregion
}
