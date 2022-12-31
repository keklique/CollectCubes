// Level by Ahmet Keklik
// e-mail: ahmetkeklik@outlook.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
	public List<Texture2D> Layers;
	public LevelType LevelType = LevelType.Standard;
}
