using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringBuilding : IProducing
{
    //Change State To Match this building without an Array!!!
    float producedTime = 0f;
    TileCell tileCell;
    // readonly IState ProductiveState;
    // readonly IState NonProductiveState;
    // IState currentState;
    public Storage storage;
    List<ResourceBuilding> resourceBuildings;
    GatheringBuildingSO GatheringBuildingSO;

    public GatheringBuilding(){
        // NonProductiveState = new NonProductiveState();
        // ProductiveState = new ProductiveState();
        // currentState = NonProductiveState;
    }
    public bool CanBeDestoyedByBuilding()
    {
        return false;
    }

    public void Demontage()
    {
        ProductionManager.singleton.StopProducing(this);
    }

    public IBuildableSO GetBuildableSO()
    {
        return GatheringBuildingSO;
    }

    public TileCell GetTileCell()
    {
        return tileCell;
    }

    public void Initialize(TileCell tileCell, IBuildableSO buildableSO)
    {
        this.tileCell = tileCell;
        GatheringBuildingSO = buildableSO as GatheringBuildingSO;
        resourceBuildings = new List<ResourceBuilding>();
        storage = new Storage(GatheringBuildingSO.consumingResource, GatheringBuildingSO.producingResource, GatheringBuildingSO.capacity);
        IBuildableSO buildableResource = GatheringBuildingSO.consumingResource;
        foreach(TileCell cell in CellGrid.singleton.GetCellNeighbours(tileCell.x,tileCell.y)){
            if(cell.TryBuild(buildableResource)){
                resourceBuildings.Add(cell.building as ResourceBuilding);
                storage.AddToStorage(buildableResource as Resource, 1);
            }
        }
        Debug.Log(storage.storage[GatheringBuildingSO.consumingResource]);
        ProductionManager.singleton.StartProducing(this);
    }

    public void Produce(float delta)
    {
        if(!CheckIfCanProduce()){ 
                Debug.Log("Not Enougth Resources!");
                return;
            }
        producedTime += delta;
        if(producedTime >= GatheringBuildingSO.productionTime){
            Debug.Log("Produced!");
            producedTime = 0f;
            UpdateStorage();
        }
    }

    // public IState GetProductiveState()
    // {
    //     return ProductiveState;
    // }

    // public IState GetNonProductiveState()
    // {
    //     return NonProductiveState;
    // }

    public Storage GetStorage()
    {
        return storage;
    }

    public void UpdateStorage()
    {
        //Doesn't need to remove consuming resources;
        int inStorage = storage.GetNumberInStorage(GatheringBuildingSO.consumingResource);
        //Add producing Resources to storage;
        storage.AddToStorage(GatheringBuildingSO.producingResource, inStorage * GatheringBuildingSO.producingNumber);
    }

    public float GetProductionTime()
    {
        return GatheringBuildingSO.productionTime;
    }

    public bool CheckIfCanProduce()
    {
        if (storage.CheckIfEnoughtResource(GatheringBuildingSO.consumingResource,1)) return true;
        return false;
    }
}
