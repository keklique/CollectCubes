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


    #region UNITY_EVENTS

    #endregion

    #region EVENTS

    #endregion

    #region PUBLIC_METHODS
    public void SetState(CollectableState _state)
    {
        if (state == _state) return;
        state = _state;
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
            default:
                // code block
                break;
        }

    }

    public void MoveToStore(Transform _target)
    {
        StartCoroutine(MoveToStoreCoroutine(_target));
    }

    public CollectableState GetState()
    {
        return state;
    }

    public void SetColor(Color32 _color)
    {
        meshRenderer.material.color = _color;
    }
    #endregion

    #region PRIVATE_METHODS
    private IEnumerator MoveToStoreCoroutine(Transform _transform)
    {
        SetState(CollectableState.Passive);
        SetColor(Color.yellow);
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
