using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Pathfinding{

    private readonly CellGrid cellGrid;
    public Pathfinding(CellGrid cellGrid){
        this.cellGrid = cellGrid;
    }

    public List<TileCell> GeneratePath(TileCell startCell, TileCell finishCell){
        List<TileCell> path = new List<TileCell>();
        TileCell currentCell = startCell;
        List<TileCell> check = new List<TileCell>();
        
        

        return path;
    }

}
