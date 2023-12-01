using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Building/Building")]
public class BuildingSO : ScriptableObject, IBuildableSO
{
    [SerializeField] public Resource[] consumingResources;
    [SerializeField] public int[] consumingNumbers;
    public int consumingResourcesNumber;

    public Tile[] tiles;

    public int productionTime = 10;
    public Resource producing;
    public int producingNumber;
    public int capacity = 1;

    private void Awake(){
        if(consumingResources.Length != consumingNumbers.Length) Debug.LogError("Two arrays have different Length");
        consumingResourcesNumber = consumingResources.Length;
    }

    public Tile GetBuildingTile(int currentDirection){
        return tiles[currentDirection];
    }

    public ScriptableObject GetScriptableObject(){
        return this;
    }

    public Resource[] GetConsumingRosurces()
    {
        return consumingResources;
    }

    public Resource GetProducingResource()
    {
        return producing;
    }

    public int GetProducingNumber()
    {
        return producingNumber;
    }

    public int[] GetConsumingNumbers()
    {
        return consumingNumbers;
    }

    public int GetConsumingResourceNumber()
    {
        return consumingResourcesNumber;
    }

    public int GetProductionTime()
    {
        return productionTime;
    }

}
