using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Killer Rabbit/Map Rotation")]
public class MapRotationSO : ScriptableObject
{
    public List<GameManager.GameScenes> scenes;
}
