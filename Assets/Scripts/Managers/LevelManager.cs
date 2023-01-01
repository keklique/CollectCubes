// LevelManager by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    [SerializeField] private Level[] levels;
    private int currentLevel = 0;
    private LevelState levelState = LevelState.Init;

    [Header("References")]
    [SerializeField] private CubeActor cubePrefab;
    [SerializeField] private PoolManager poolManager;

    [Space(15)]
    [Header("General")]
    private List<ICollectable> activeCollectables = new List<ICollectable>();
    private List<ICollectable> passiveCollectables = new List<ICollectable>();

    #region UNITY_EVENTS

    private void Start()
    {
        LoadLevel(currentLevel);
    }
    private void OnEnable()
    {
        poolManager.OnCollectableStateChanged += OnCollectableStateChanged;
    }

    private void OnDisable()
    {
        poolManager.OnCollectableStateChanged -= OnCollectableStateChanged;
    }
    #endregion

    #region EVENTS
    public event EventHandler<OnLevelStateChangeArgs> OnLevelStateChange;
    public class OnLevelStateChangeArgs : EventArgs
    {
        public int levelID;
        public LevelState levelState;
    }

    private void OnCollectableStateChanged(object sender, PoolManager.OnCollectableStateChangedArgs e)
    {
        if(activeCollectables.Contains(e.collectable) && e.state == CollectableState.Passive)
        {
            activeCollectables.Remove(e.collectable);
            passiveCollectables.Add(e.collectable);
            if (activeCollectables.Count <= 0 && levels[currentLevel % levels.Length].LevelType == LevelType.Standard)
            {
                LoadLevel(currentLevel + 1);
            }
        }

        
    }
    #endregion

    #region PUBLIC_METHODS
    public void StartLevel()
    {
        SetLevelState(LevelState.Play);
    }
    #endregion

    #region PRIVATE_METHODS

    private void SetLevelState(LevelState _levelState)
    {
        levelState = _levelState;
        OnLevelStateChange?.Invoke(this, new OnLevelStateChangeArgs { levelID = currentLevel, levelState = levelState });
    }
    private void InitLevel()
    {
        OnLevelStateChange?.Invoke(this, new OnLevelStateChangeArgs { levelID = currentLevel, levelState= levelState } ) ;
    }

    private void ResetLevel()
    {
        for (int i = 0; i<activeCollectables.Count;i++)
        {
            poolManager.AddToPool(activeCollectables[i]);
        }
        for (int i = 0; i < passiveCollectables.Count; i++)
        {
            poolManager.AddToPool(passiveCollectables[i]);
        }
        activeCollectables = new List<ICollectable>();
        passiveCollectables = new List<ICollectable>();
    }
    private void LoadLevel(int _levelID)
    {
        currentLevel = _levelID;
        SetLevelState(LevelState.Init);
        ResetLevel();
        Level _level = levels[currentLevel % levels.Length];
        
        for(int i = 0; i<_level.Layers.Count;i++)
        {
            CreateLayer(i, _level.Layers[i], cubePrefab);

        }
        SetLevelState(LevelState.Start);
    }

    // Create vertical layers of level
    private void CreateLayer(int _layerNo, Texture2D _layerTexture, ICollectable _collectable)
    {
        Color32 _tempColor;
        ICollectable collectable;
        int _width = _layerTexture.width;
        int _height = _layerTexture.height;
        int xOffset = -25;
        int zOffset = -10;
        Color32[] _pixels;
        _pixels = _layerTexture.GetPixels32(0);

        for (int i = 0; i<_height;i++)
        {
            for(int j = 0; j < _width; j++)
            {
                _tempColor = _pixels[i*_width + j] ;
                if (_tempColor.a == 0) continue;
                collectable = poolManager.FetchFromPool();
                activeCollectables.Add(collectable);
                collectable.Spawn(new Vector3(j + xOffset, _layerNo + 0.5f, i + zOffset), _tempColor);
            }
        }
    }

    #endregion
}
