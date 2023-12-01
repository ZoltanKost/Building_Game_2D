using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CellGrid{

    public static CellGrid singleton{get;private set;}
    // public Pathfinding pathfinding;
    public static EventHandler<TileCell> OnCellChanged;
    TileCell[,] tileCellMap;
    Resource[] resources;

    int currentDirection;
    int width, height;

    public CellGrid(int width, int height, Tile[] ground, Resource[] resources = null, int currentDirection = 0){
        if(singleton == null) singleton = this;
        this.width = width;
        this.height = height;
        this.currentDirection = currentDirection;
        this.resources = resources != null ? resources : new Resource[1];
        GenerateFloor(ground);
        // pathfinding = new Pathfinding(this);
    }

    public void GenerateFloor(Tile[] grounds){
        tileCellMap = new TileCell[width, height];
        for(int y =0; y<width; y++){
            for (int x = 0; x < height; x++){
                Tile ground = grounds[UnityEngine.Random.Range(0, grounds.Length - 1)];
                
                if(UnityEngine.Random.Range(0,11) < 3){
                    Resource resource = resources[UnityEngine.Random.Range(0,resources.Length)];
                    tileCellMap[x,y] = new TileCell(x, y, ground);
                    if(resource) tileCellMap[x,y].TryBuild(resource);
                }else{
                    tileCellMap[x,y] = new TileCell(x, y, ground);
                }
                
                OnCellChanged?.Invoke(this,tileCellMap[x,y]);
            }
        }
    }

    public void RevertMapLeft(){
        for (int x = 0; x < width / 2; x++){
            for (int y = 0; y < height / 2; y++){
                TileCell temp = tileCellMap[x,y];
                int x1 = y;
                int y1 = height - x - 1;
                int x2 = y1;
                int y2 = height - x1 - 1;
                int x3 = y2;
                int y3 = height - x2 - 1;

                tileCellMap[x,y] = tileCellMap[x3,y3];
                tileCellMap[x,y].SetCoordinates(x3,y3);
                tileCellMap[x3, y3] = tileCellMap[x2,y2];
                tileCellMap[x3,y3].SetCoordinates(x2,y2);
                tileCellMap[x2,y2] = tileCellMap[x1,y1];
                tileCellMap[x2,y2].SetCoordinates(x1,y1);
                tileCellMap[x1,y1] = temp;
                tileCellMap[x1,y1].SetCoordinates(x,y);

                OnCellChanged?.Invoke(this, tileCellMap[x,y]);
                OnCellChanged?.Invoke(this, tileCellMap[x1,y1]);
                OnCellChanged?.Invoke(this, tileCellMap[x2,y2]);
                OnCellChanged?.Invoke(this, tileCellMap[x3,y3]);
            }
        }
    }

    public void RevertMapRight(){
        for (int x = 0; x < width / 2; x++){
            for (int y = 0; y < height / 2; y++){

                int x1 = width - y - 1;
                int y1 = x;
                int x2 = width - y1 - 1;
                int y2 = x1;
                int x3 = width - y2 - 1;
                int y3 = x2;

                TileCell temp = tileCellMap[x3,y3];
                tileCellMap[x3,y3] = tileCellMap[x2,y2];
                tileCellMap[x3,y3].SetCoordinates(x2,y2);
                tileCellMap[x2, y2] = tileCellMap[x1,y1];
                tileCellMap[x2,y2].SetCoordinates(x1,y1);
                tileCellMap[x1,y1] = tileCellMap[x,y];
                tileCellMap[x1,y1].SetCoordinates(x,y);
                tileCellMap[x,y] = temp;
                tileCellMap[x,y].SetCoordinates(x3,y3);

                OnCellChanged?.Invoke(this, tileCellMap[x,y]);
                OnCellChanged?.Invoke(this, tileCellMap[x1,y1]);
                OnCellChanged?.Invoke(this, tileCellMap[x2,y2]);
                OnCellChanged?.Invoke(this, tileCellMap[x3,y3]);
            }
        }
    }

    public void FillWithHouses(IBuildableSO currentBuildingSO){
        if(currentBuildingSO == null) return;
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                tileCellMap[x,y].TryBuild(currentBuildingSO);
                OnCellChanged?.Invoke(this, tileCellMap[x,y]);
            }
        }
    }

    public void PlaceRoad(int x, int y, RuleTile roadTiles){
        TileCell chosenCell = GetCell(x,y);
        if(chosenCell == null) return;
        if(!chosenCell.PlaceRoad(roadTiles)) return;
        OnCellChanged?.Invoke(this, tileCellMap[x,y]);
    }

    public void BuildTile(int x, int y, IBuildableSO building){
        TileCell chosenCell = GetCell(x,y);
        if(chosenCell == null) return;
        if(!chosenCell.TryBuild(building)) return;
        OnCellChanged?.Invoke(this, tileCellMap[x,y]);
    }

    public void DestroyTile(int x, int y){
        TileCell chosenCell = GetCell(x,y);
        if(chosenCell == null) return;
        if(chosenCell.DestroyBuildingOrRoad()){
            OnCellChanged?.Invoke(this, tileCellMap[x,y]);
        }
    }

    public TileCell GetCell(int x, int y){
        if(!CheckBoundaries(x, y)) return null;
        return tileCellMap[x, y];
    }

    public bool CheckBoundaries(int x, int y){
        if(x < 0 || x >= width) return false;
        if(y < 0 || y >= height) return false;
        return true;
    }

    public int SwitchDirection(bool left){
        if(left){ 
            currentDirection -= 1;
            if(currentDirection < 0) currentDirection = 3;
        }
        else{ 
            currentDirection += 1;
            if(currentDirection > 3) currentDirection = 0;
        }
        return currentDirection;
    }

    public List<TileCell> GetCellNeighbours(int x, int y){
        if(tileCellMap[x,y].neighbours != null) return tileCellMap[x,y].neighbours;
        List<TileCell> neighbours = new List<TileCell>();
        if(x - 1 >= 0) neighbours.Add(tileCellMap[x - 1, y]);
        if(x + 1 < width) neighbours.Add(tileCellMap[x + 1, y]);
        if(y - 1 >= 0) neighbours.Add(tileCellMap[x, y - 1]);
        if(y + 1 < height) neighbours.Add(tileCellMap[x, y + 1]);
        tileCellMap[x,y].neighbours = neighbours; 
        return neighbours;
    }
    
}
