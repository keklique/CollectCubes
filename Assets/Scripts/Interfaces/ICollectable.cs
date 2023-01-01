// ICollectable by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void SetState(CollectableState state);
    CollectableState GetState();
    void MoveToStore(Transform transform);
    void ToPool(Transform parent);
    void SetColor(Color32 color);
    void Spawn(Vector3 position, Color32 color);
}
