// UIManager by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : Manager<UIManager>
{
    LevelManager levelManager;

    [Header("General Varaibles")]
    private Dictionary<PanelType, GamePanel> panelDictionary = new Dictionary<PanelType, GamePanel>();
    private PanelType currentPanelType = PanelType.Loading;

    [Header("References")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GamePanel[] panels;
    #region UNITY_EVENTS
    protected void Start()
    {
        levelManager = LevelManager.Instance;
        levelManager.OnLevelStateChange += OnLevelStateChange;
        RegisterPanels();
    }

    protected void OnDisable()
    {
        levelManager.OnLevelStateChange -= OnLevelStateChange;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetCurrentPanelType(PanelType.Loading);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetCurrentPanelType(PanelType.Start);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetCurrentPanelType(PanelType.InGame);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetCurrentPanelType(PanelType.Success);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCurrentPanelType(PanelType.Fail);
        }
    }
    #endregion

    #region EVENTS
    public event EventHandler<OnPanelTypeChangedArgs> OnPanelTypeChanged;
    public class OnPanelTypeChangedArgs : EventArgs
    {
        public PanelType currentPanelType;
    }

    private void OnLevelStateChange(object sender, LevelManager.OnLevelStateChangeArgs e)
    {
        print(e.levelState);

        switch (e.levelState)
        {
            case LevelState.Init:
                SetCurrentPanelType(PanelType.Loading);
                break;
            case LevelState.Start:
                SetCurrentPanelType(PanelType.Start);
                break;
            case LevelState.Play:
                SetCurrentPanelType(PanelType.InGame);
                break;
            case LevelState.Success:
                SetCurrentPanelType(PanelType.Success);
                break;
            case LevelState.Fail:
                SetCurrentPanelType(PanelType.Fail);
                break;
            default:
                break;
        }
    }
    #endregion

    #region PUBLIC_METHODS
    public void StartLevel()
    {
        print("Level Started");
        levelManager.StartLevel();
    }

    #endregion

    #region PRIVATE_METHODS

    private void SetCurrentPanelType(PanelType _panelType)
    {
        currentPanelType = _panelType;
        OnPanelTypeChanged?.Invoke(this, new OnPanelTypeChangedArgs { currentPanelType = currentPanelType });
    }

    private void RegisterPanels()
    {
        for(int i = 0; i < panels.Length;i++)
        {
            RegisterPanel(panels[i]);
        }
    }
    private void RegisterPanel(GamePanel _panel)
    {
        if(!IsPanelExists(_panel.panelType))
        {
            panelDictionary.Add(_panel.panelType,_panel);
        }
        else
        {
            Debug.Log($"[UIManager]: {_panel.panelType} panel allready exists");
        }
    }

    private bool IsPanelExists(PanelType _panelType)
    {
        bool r = false;
        foreach (KeyValuePair<PanelType, GamePanel> panel in panelDictionary)
        {
            if (panel.Key == _panelType) return r = true;
        }
        return r;
    }
    #endregion
}
