using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Building/ StorageBuilding")]
public class StorageBuildingSO : ScriptableObject, IBuildableSO
{
    public Tile tile;
    public Resource[] resources;
    public readonly int capacity;
    public Tile GetBuildingTile(int currentDirection)
    {
        return tile;
    }

    public ScriptableObject GetScriptableObject()
    {
        return this;
    }
}
