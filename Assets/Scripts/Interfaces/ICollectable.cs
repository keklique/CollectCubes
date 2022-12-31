// ICollectable by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void SetState(CollectableState _state);
    CollectableState GetState();
    void MoveToStore(Transform _transform);
}
