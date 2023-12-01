using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IBuildableSO
{
    public Tile GetBuildingTile(int currentDirection);
    public ScriptableObject GetScriptableObject();
}
