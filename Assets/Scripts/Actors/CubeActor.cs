// BoxActor by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeActor : Actor<PoolManager>, ICollectable
{
	
    [Header("Level Design")]
    private int temp1;

    [Space(15)]
    [Header("General Variables")]
    private CollectableState state = CollectableState.Active;

    [Space(15)]
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Collider mainCollider;


    #region UNITY_EVENTS

    #endregion

    #region EVENTS

    #endregion

    #region PUBLIC_METHODS
    public void SetState(CollectableState _state)
    {
        if (state == _state) return;
        state = _state;
        manager.CollectableStateChanged(this);
        switch (state)
        {
            case CollectableState.Active:
                gameObject.layer = LayerMask.NameToLayer("ActiveCube");
                break;
            case CollectableState.Collected:
                
                break;
            case CollectableState.Passive:
                gameObject.layer = LayerMask.NameToLayer("PassiveCube");
                break;
            case CollectableState.AtPool:
                
                break;
            default:
                // code block
                break;
        }

    }

    public void MoveToStore(Transform _target, Color _color)
    {
        StartCoroutine(MoveToStoreCoroutine(_target, _color));
    }

    public CollectableState GetState()
    {
        return state;
    }

    public void SetColor(Color32 _color)
    {
        meshRenderer.material.color = _color;
    }

    public void ToPool(Transform _parent, PoolManager _poolManager)
    {
        StopAllCoroutines();
        mainCollider.isTrigger = true;
        manager = _poolManager;
        transform.SetParent(_parent);

        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;

        rb.velocity = Vector3.zero;
        SetState(CollectableState.AtPool);
        gameObject.SetActive(false);
    }

    public void Spawn(Vector3 _position, Color32 _color, Vector3 _initialForce)
    {
        transform.position = _position;
        SetColor(_color);
        gameObject.SetActive(true);
        mainCollider.isTrigger = false;
        SetState(CollectableState.Active);
        rb.AddForce(_initialForce * rb.mass * 50f);
    }
    #endregion

    #region PRIVATE_METHODS
    private IEnumerator MoveToStoreCoroutine(Transform _transform, Color _color)
    {
        SetState(CollectableState.Passive);
        SetColor(_color);
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        for(int i = 0; i<100;i++)
        {
            rb.MovePosition(Vector3.Lerp(rb.position,_transform.position,Time.deltaTime *10f));
            yield return null;
        }
        yield return null;
    }

    private void SetColor(Color _color)
    {
        meshRenderer.material.color = _color;
    }
    #endregion
}
