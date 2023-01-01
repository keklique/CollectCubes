// ScoreBoard by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : Actor<UIManager>
{
    [Header("Level Design")]
    private int temp1;

    [Space(15)]
    [Header("General Variables")]
    private int score;

    [Space(15)]
    [Header("References")]
    [SerializeField] private TMPro.TMP_Text scoreboardText;
    [SerializeField] private GameObject scoreboardObject;


    #region UNITY_EVENTS
    protected override void Start()
    {
        base.Start();
        manager.OnOnTimerInit += OnOnTimerInit;
        LevelManager.Instance.OnScoreChange += OnScoreChange;
    }
    #endregion

    #region EVENTS
    private void OnOnTimerInit(object sender, UIManager.OnTimerInitArgs e)
    {
        if (e.level.LevelType == LevelType.Timer || e.level.LevelType == LevelType.Rival)
        {
            scoreboardObject.SetActive(true);
            score = 0;
        }
        else
        {
            scoreboardObject.SetActive(false);
            score = 0;
        }
        scoreboardText.text = score.ToString();
    }

    private void OnScoreChange(object sender, LevelManager.OnScoreChangeArgs e)
    {
        score = e.score;
        scoreboardText.text = score.ToString();
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
