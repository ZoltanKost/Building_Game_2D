using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(menuName = "Building/Gathering Building")]
public class GatheringBuildingSO : ScriptableObject, IBuildableSO{

    public Resource consumingResource;
    public Resource producingResource;
    public Tile[] tiles;
    public int producingNumber = 1;
    public int gatheringCellRange;
    public float productionTime;
    public int capacity;

    public Tile GetBuildingTile(int currentDirection)
    {
        currentDirection = Mathf.Clamp(currentDirection,0,tiles.Length - 1);
        return tiles[currentDirection];
    }

    public ScriptableObject GetScriptableObject()
    {
        return this;
    }
}
