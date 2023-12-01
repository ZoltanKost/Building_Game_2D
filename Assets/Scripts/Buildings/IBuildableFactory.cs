using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBuildableFactory{
    public abstract IBuildableObject Build(TileCell tileCell, IBuildableSO buildableSO);
}

// public class BuildingFactory : IBuildableFactory
// {
//     public static BuildingFactory singleton{get; private set;} = new BuildingFactory();
//     public override IBuildableObject Build(TileCell tileCell, IBuildableSO buildableSO)
//     {
//         Building building = new Building();
//         building.Initialize(tileCell,buildableSO);
//         return building;
//     }
// }

// public class ResourceBuildingFactory : IBuildableFactory
// {
//     public static ResourceBuildingFactory singleton{get; private set;} = new ResourceBuildingFactory();
//     public override IBuildableObject Build(TileCell tileCell, IBuildableSO buildableSO)
//     {
//         ResourceBuilding resourceBuilding = new ResourceBuilding();
//         resourceBuilding.Initialize(tileCell, buildableSO);
//         return resourceBuilding;
//     }
// }

// public class GatheringBuildingFactory : IBuildableFactory
// {
//     public static GatheringBuildingFactory singleton{get; private set;} = new GatheringBuildingFactory();
//     public override IBuildableObject Build(TileCell tileCell, IBuildableSO buildableSO)
//     {
//         GatheringBuilding gatheringBuilding = new GatheringBuilding();
//         gatheringBuilding.Initialize(tileCell, buildableSO);
//         return gatheringBuilding;
//     }
// }

// public class BuildableStorageFactory : IBuildableFactory
// {
//     public static BuildableStorageFactory singleton{get; private set;} = new BuildableStorageFactory();
//     public override IBuildableObject Build(TileCell tileCell, IBuildableSO buildableSO)
//     {
//         StorageBuilding storage = new StorageBuilding();
//         storage.Initialize(tileCell, buildableSO);
//         return storage;
//     }
// }

public class CommonBuildableFactory : IBuildableFactory
{
    public static CommonBuildableFactory singleton{get;private set;} = new CommonBuildableFactory();
    public override IBuildableObject Build(TileCell tileCell, IBuildableSO buildableSO){
        IBuildableObject buildableObject = new Building();
        
        switch(buildableSO){
            case GatheringBuildingSO:
                buildableObject = new GatheringBuilding();
            break;
            case StorageBuildingSO:
                buildableObject = new StorageBuilding();
            break;
            case Resource:
                buildableObject = new ResourceBuilding();
            break;
        }
        buildableObject.Initialize(tileCell, buildableSO);
        return buildableObject;

    }
}
