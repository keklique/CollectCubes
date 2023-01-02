// PlayerControllerActor by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerActor : Actor<LevelManager>
{

    [Header("Level Design")]
    [SerializeField] private float speed = 10f;

    [Space(15)]
    [Header("General Variables")]
    private Plane basePlane;
    private Vector3 hit;
    private Ray ray;
    private Camera mainCamera;
    private bool isActive = false;
    private Vector3 previousInput = Vector3.zero;
    private Vector3 currentInput;
    

    [Space(15)]
    [Header("References")]
    [SerializeField] private CollectorActor collectorActor;


    #region UNITY_EVENTS
    protected override void Start()
    {
        base.Start();
        InitVariables();
        manager.OnLevelStateChange += OnLevelStateChange;
    }
    private void Update()
    {
        Move();
    }
    #endregion

    #region EVENTS
    private void OnLevelStateChange(object sender, LevelManager.OnLevelStateChangeArgs e)
    {
        if(e.levelState == LevelState.Play)
        {
            isActive = true;
        }else if(e.levelState == LevelState.Init)
        {
            isActive = false;
            collectorActor.SetPosition(new Vector3(0f,0f,-25f));
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
    private void Move()
    {
        if (!isActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            previousInput = MousetoWorldPosition(Input.mousePosition);
            
        }else if(Input.GetMouseButton(0))
        {
            currentInput = MousetoWorldPosition(Input.mousePosition);
            
            if((currentInput - previousInput).magnitude > 0.3f)
            {
                collectorActor.SetVelocity((currentInput - previousInput).normalized * speed * Time.deltaTime);
                previousInput += (currentInput - previousInput)/50f;
            }
            else
            {
                collectorActor.SetVelocity(Vector3.zero);
                previousInput = currentInput;
            }
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            collectorActor.SetVelocity(Vector3.zero);

        }

    }
    private void InitVariables()
    {
        mainCamera = Camera.main;
        Vector3 tempPlane = Vector3.zero;
        tempPlane.y = 0f;
        basePlane = new Plane(Vector3.up, tempPlane);
    }
    private Vector3 MousetoWorldPosition(Vector3 mousePosition)
    {
        Vector3 screenCoordinates = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
        ray = mainCamera.ScreenPointToRay(screenCoordinates);
        float ent = 100.0f;
        basePlane.Raycast(ray, out ent);
        hit = ray.GetPoint(ent);
        return hit;
    }
    #endregion
}
