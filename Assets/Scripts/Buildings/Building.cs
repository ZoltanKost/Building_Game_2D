using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : IBuildableObject, IProducing{

    //Insert delegate to instantiate visual if can't poduce anything;
    public TileCell tileCell;
    private BuildingSO BuildingSO; 
    public Storage storageOfResources;

    // readonly IState productiveState;
    // readonly IState nonProductiveState;
    // IState currentState;
    float producedTime = 0f;

    public Building(){
        // productiveState = new ProductiveState();
        // nonProductiveState = new NonProductiveState();
        // currentState = new ProductiveState();
    }

    public void Produce(float delta){
        producedTime += Time.deltaTime;
        if(producedTime > BuildingSO.productionTime){
            UpdateStorage();
            producedTime = 0;
        }
    }

    public Tile GetBuildingTile(int currentDirection){
        return BuildingSO.GetBuildingTile(currentDirection);
    }

    public IBuildableSO GetBuildableSO()
    {
        return BuildingSO;
    }

    public TileCell GetTileCell(){
        return tileCell;
    }

    public bool CanBeDestoyedByBuilding(){
        return false;
    }

    public void Demontage(){
        ProductionManager.singleton.StopProducing(this);
    }

    public void Initialize(TileCell tileCell, IBuildableSO buildableSO)
    {
        this.BuildingSO = buildableSO as BuildingSO;
        this.tileCell = tileCell;
        // currentState = new NonProductiveState();
        storageOfResources = new Storage(BuildingSO.consumingResources, BuildingSO.producing, BuildingSO.capacity);
        ProductionManager.singleton.StartProducing(this);
    }

    // public IState GetProductiveState() -- temporarily removed in oreder to test other implementations
    // {
    //     return productiveState;
    // }

    // public IState GetNonProductiveState()
    // {
    //     return nonProductiveState;
    // }

    public Storage GetStorage()
    {
        return storageOfResources;
    }
    public void UpdateStorage()
    {
        for (int i = 0; i < BuildingSO.consumingResourcesNumber; i++){
            storageOfResources.RemoveFromStorage(BuildingSO.consumingResources[i], BuildingSO.consumingNumbers[i]);
        }
        
        storageOfResources.AddToStorage(BuildingSO.producing,BuildingSO.producingNumber);
    }

    public bool CheckIfCanProduce()
    {
        bool canProduce = true;
        for (int i = 0; i < BuildingSO.consumingResourcesNumber; i++){
            Resource resource = BuildingSO.consumingResources[i];
            if(storageOfResources.CheckIfEnoughtResource(resource, BuildingSO.GetConsumingNumbers()[i])) continue;
            canProduce = false;
            break;
        }
        return canProduce;
    }

    public float GetProductionTime()
    {
        return BuildingSO.GetProductionTime();
    }
}
