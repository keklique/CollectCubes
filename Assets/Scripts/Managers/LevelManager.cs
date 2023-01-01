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
    private LevelState levelState = LevelState.Play;

    [Header("References")]
    [SerializeField] private CubeActor cubePrefab;

    #region UNITY_EVENTS

    #endregion

    #region EVENTS
    public event EventHandler<OnLevelStateChangeArgs> OnLevelStateChange;
    public class OnLevelStateChangeArgs : EventArgs
    {
        public int levelID;
        public LevelState levelState;
    }
    #endregion

    #region PUBLIC_METHODS
    public void StartLevel()
    {
        InitLevel();
    }
    #endregion

    #region PRIVATE_METHODS
    private void InitLevel()
    {
        OnLevelStateChange?.Invoke(this, new OnLevelStateChangeArgs { levelID = currentLevel, levelState= levelState } ) ;
        LoadLevel(0);
    }

    private void LoadLevel(int _levelID)
    {
        currentLevel = _levelID;
        Level _level = levels[currentLevel % levels.Length];
        
        for(int i = 0; i<_level.Layers.Count;i++)
        {
            CreateLayer(i, _level.Layers[i], cubePrefab);

        }
        

    }

    // Create vertical layers of level
    private void CreateLayer(int _layerNo, Texture2D _layerTexture, ICollectable _collectable)
    {
        Color32 _tempColor;
        CubeActor collectable;
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
                collectable = Instantiate(cubePrefab, new Vector3(j + xOffset,_layerNo + 0.5f,i + zOffset), Quaternion.identity);
                collectable.SetColor(_tempColor);
            }
        }
        

    }

    public static Texture2D RenderTextureToTexture2D(RenderTexture _renderTexture)
    {
        RenderTexture.active = _renderTexture;
        Texture2D _tex = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGB24, false);
        _tex.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);
        RenderTexture.active = null;

        return _tex;
    }
    #endregion
}
