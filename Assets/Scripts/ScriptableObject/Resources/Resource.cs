using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Resources/newResource")]
public class Resource : ScriptableObject, IBuildableSO{

    public string Name;
    public Sprite UISprite;
    public Tile BuildingTile;

    public Tile GetBuildingTile(int currentDirection)
    {
        return BuildingTile;
    }

    public ScriptableObject GetScriptableObject()
    {
        return this;
    }
}
