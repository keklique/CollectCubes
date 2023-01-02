// RivalActor by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalActor : Actor<LevelManager>
{
	
    [Header("Level Design")]
    [SerializeField] private float speed = 10f;

    [Space(15)]
    [Header("General Variables")]
    private bool isActive = false;
    private Vector3 velocity;

    [Space(15)]
    [Header("References")]
    [SerializeField] private CollectorActor collectorActor;
    [SerializeField] private StoreActor storeActor;


    #region UNITY_EVENTS

    #endregion
    protected override void Start()
    {
        base.Start();
        InitVariables();
        manager.OnLevelStateChange += OnLevelStateChange;
        storeActor.SetColor(Color.red);
    }

    private void Update()
    {
        if (!isActive) return;
        collectorActor.SetVelocity(velocity);
    }

    #region EVENTS
    private void OnLevelStateChange(object sender, LevelManager.OnLevelStateChangeArgs e)
    {
        if (e.levelState == LevelState.Play)
        {
            isActive = true;
            StartCoroutine(RivalCoroutine());
        }
        else if (e.levelState == LevelState.Init)
        {
            isActive = false;
            collectorActor.SetPosition(new Vector3(0f, 0f, -25f));
            velocity = Vector3.zero;
        }
        else
        {
            isActive = false;
        }
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS
    private void InitVariables()
    {

    }

    private IEnumerator RivalCoroutine()
    {

        while(isActive)
        {
            velocity = ((new Vector3(Random.Range(-20f,20f),0f,0f) - collectorActor.transform.position).normalized * speed);
            yield return new WaitForSeconds(3f);
            velocity = ((new Vector3(0f, 0f, 25f) - collectorActor.GetCollectorPosition()).normalized * speed);
            yield return new WaitForSeconds(3f);
        }
        yield return null;
    }
    #endregion
}
