using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBuilding : IBuildableObject
{
    // reference to GlobalStorage
    private TileCell tileCell;
    private Storage storage;
    private StorageBuildingSO storageBuildingSO;
    public StorageBuilding(){}
    public bool CanBeDestoyedByBuilding()
    {
        return false;
    }

    public void Demontage()
    {
        throw new System.NotImplementedException();
    }

    public IBuildableSO GetBuildableSO()
    {
        return storageBuildingSO;
    }

    public TileCell GetTileCell()
    {
        return tileCell;
    }

    public void Initialize(TileCell tileCell, IBuildableSO buildableSO)
    {
        this.storageBuildingSO = buildableSO as StorageBuildingSO;
        storage = new Storage(this.storageBuildingSO.resources, storageBuildingSO.capacity); 
        this.tileCell = tileCell;
    }
}
