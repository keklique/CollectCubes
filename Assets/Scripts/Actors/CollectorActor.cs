// CollectorActor by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorActor : Actor<LevelManager>
{
	

    [Space(15)]
    [Header("General Variables")]
    public float temp_speed = 10f;

    [Space(15)]
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    #region UNITY_EVENTS

    #endregion

    #region EVENTS

    #endregion

    #region PUBLIC_METHODS
    public void AddForce(Vector3 _force)
    {
        rb.velocity = _force;
        if(_force != Vector3.zero) rb.MoveRotation(Quaternion.LookRotation(_force,Vector3.up));
    }

    public void SetPosition(Vector3 _position)
    {
        rb.MovePosition(_position);
        rb.MoveRotation(Quaternion.LookRotation((transform.position * -1f), Vector3.up));
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion
}
