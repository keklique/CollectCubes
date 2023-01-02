// ICollectable by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void SetState(CollectableState state);
    CollectableState GetState();
    void MoveToStore(Transform transform, Color color);
    void ToPool(Transform parent, PoolManager poolManager);
    void SetColor(Color32 color);
    void Spawn(Vector3 position, Color32 color, Vector3 initialForce);
    void SetLayer(string layer);
}
