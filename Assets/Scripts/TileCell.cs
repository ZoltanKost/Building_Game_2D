using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCell
{
    public Tile groundTile;
    public RuleTile roadTile;
    public IBuildableObject building;
    // public Resource[] gathering;
    public int x,y;
    public int MoveScore = 10;
    public Sprite sprite;
    public bool walkable = true;
    public bool buildable = true;
    public List<TileCell> neighbours;
    public TileCell(int x, int y, Tile tile){
        this.groundTile = tile;
        this.x = x;
        this.y = y;
        
    }

    public IBuildableSO GetBuildingSO(){
        return building.GetBuildableSO();
    }
    public bool TryBuild(IBuildableSO buildingSO){
        if(!buildable) return false;
        this.building = CommonBuildableFactory.singleton.Build(this, buildingSO);
        buildable = building.CanBeDestoyedByBuilding();
        walkable = buildable;
        CellGrid.OnCellChanged?.Invoke(this, this);
        return true;
    }

    public bool PlaceRoad(RuleTile tile){
        if(!buildable) return false;
        roadTile = tile;
        buildable = roadTile == null;
        CellGrid.OnCellChanged?.Invoke(this, this);
        return true;
    }

    public bool DestroyBuildingOrRoad(){
        if(buildable) return false;
        if(building != null){
            building.Demontage();
            building = null;
        }
        roadTile = null;
        buildable = building == null && !roadTile;
        walkable = building == null;
        CellGrid.OnCellChanged?.Invoke(this, this);
        return true;
    }

    public TileBase GetGroundTile(){
        if(roadTile && building == null){
            return roadTile;
        }
        return groundTile;
    }

    public Tile GetBuildingTile(int currentDirection){
        if(building != null){
            return building.GetBuildableSO().GetBuildingTile(currentDirection);
        }
        return null;
    }
    public void SetCoordinates(int x, int y){
        this.x = x;
        this.y = y;
    }
}
