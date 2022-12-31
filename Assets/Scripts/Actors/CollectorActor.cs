// CollectorActor by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorActor : Actor<LevelManager>
{
	
    [Header("Level Design")]
    private int temp1;

    [Space(15)]
    [Header("General Variables")]
    public float temp_speed = 10f;

    [Space(15)]
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    #region UNITY_EVENTS
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            AddForce(Vector3.back);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AddForce(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AddForce(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AddForce(Vector3.left);
        }
    }
    #endregion

    #region EVENTS

    #endregion

    #region PUBLIC_METHODS
    public void AddForce(Vector3 _force)
    {
        //rb.AddForce(_force * temp_speed);

        rb.velocity = _force * temp_speed;
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion
}
