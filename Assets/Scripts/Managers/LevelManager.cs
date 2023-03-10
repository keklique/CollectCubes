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
    [SerializeField] private RivalActor rivalPrefab;

    [Space(15)]
    [Header("General")]
    private List<ICollectable> activeCollectables = new List<ICollectable>();
    private List<ICollectable> passiveCollectables = new List<ICollectable>();
    private Color32 _tempColor;
    private ICollectable collectable;
    private RivalActor rivalActor;

    [Header("Timer")]
    private bool isSpawning;
    private int score;
    private float initialCooldown;
    private float cooldown;
    private float timer;

    #region UNITY_EVENTS


    private void Update()
    {
        SpawnContiniously();
    }
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
        public Level level;
    }

    public event EventHandler<OnScoreChangeArgs> OnScoreChange;
    public class OnScoreChangeArgs : EventArgs
    {
        public int score;
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
            if(levels[currentLevel % levels.Length].LevelType == LevelType.Timer || levels[currentLevel % levels.Length].LevelType == LevelType.Rival)
            {
                score++;
                OnScoreChange?.Invoke(this, new OnScoreChangeArgs { score = score });
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
        Level _level = levels[currentLevel % levels.Length];
        OnLevelStateChange?.Invoke(this, new OnLevelStateChangeArgs { levelID = currentLevel, levelState = levelState, level = _level });
        if((levels[currentLevel % levels.Length].LevelType == LevelType.Timer || levels[currentLevel % levels.Length].LevelType == LevelType.Rival) && levelState == LevelState.Play)
        {
            isSpawning = true;
            initialCooldown = 1f/ (float)_level.SpawnPerSecond;
            timer = _level.Duration;
        }
        else
        {
            isSpawning = false;
        }
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
        score = 0;
        OnScoreChange?.Invoke(this, new OnScoreChangeArgs { score = score });
        if (rivalActor != null) Destroy(rivalActor.gameObject);

    }
    private void LoadLevel(int _levelID)
    {
        currentLevel = _levelID;
        SetLevelState(LevelState.Init);
        ResetLevel();
        Level _level = levels[currentLevel % levels.Length];
        
        switch (_level.LevelType)
        {
            case LevelType.Standard:
                LoadLevelFromImage(_level);
                break;
            case LevelType.Timer:
                LoadLevelWithTimer(_level);
                break;
            case LevelType.Rival:
                LoadLevelWithTimer(_level);
                rivalActor = Instantiate(rivalPrefab, Vector3.zero, Quaternion.identity);
                break;
            default:
                break;
        }
        
        SetLevelState(LevelState.Start);
    }

    private void LoadLevelFromImage(Level _level)
    {
        initialCooldown = _level.Duration;
        for (int i = 0; i < _level.Layers.Count; i++)
        {
            CreateLayer(i, _level.Layers[i], cubePrefab);

        }
    }

    private void LoadLevelWithTimer(Level _level)
    {
        
    }

    private void SpawnContiniously()
    {
        if (!isSpawning) return;
        timer -= Time.deltaTime;
        if (timer <= 0f) LoadLevel(currentLevel+1);
        cooldown += Time.deltaTime;
        if(cooldown>=initialCooldown)
        {
            cooldown = 0f;
            _tempColor = new Color(UnityEngine.Random.Range(0,1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f),1f);
            collectable = poolManager.FetchFromPool();
            activeCollectables.Add(collectable);
            collectable.Spawn(new Vector3(0f, 3f, 0f), _tempColor, new Vector3(UnityEngine.Random.Range(-30f,30f),0f, UnityEngine.Random.Range(-10f, 10f)));
        }

    }

    // Create vertical layers of level
    private void CreateLayer(int _layerNo, Texture2D _layerTexture, ICollectable _collectable)
    {
        
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
                collectable.Spawn(new Vector3(j + xOffset, _layerNo + 0.5f, i + zOffset), _tempColor, Vector3.zero);
            }
        }
    }

    #endregion
}
