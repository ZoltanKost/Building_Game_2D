using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : IBuildableObject
{
    public Resource resource;
    public TileCell tileCell;
    public ResourceBuilding(){}

    public bool CanBeDestoyedByBuilding()
    {
        return false;
    }

    public void Demontage()
    {
        Debug.Log("Destroyed");
    }

    public IBuildableSO GetBuildableSO()
    {
        return resource;
    }

    public TileCell GetTileCell()
    {
        return tileCell;
    }

    public void Initialize(TileCell tileCell, IBuildableSO buildableSO)
    {
        resource = buildableSO as Resource;
        this.tileCell = tileCell;
    }
}
