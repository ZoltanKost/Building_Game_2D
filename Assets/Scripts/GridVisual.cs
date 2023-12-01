using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridVisual
{
    private Tilemap ground;
    private Tilemap building;
    Vector3Int offset;
    int currentDirection = 0;
    public GridVisual(Tilemap[] tilemaps, Vector3Int offset){
        ground = tilemaps[0];
        building = tilemaps[1];
        this.offset = offset;
        CellGrid.OnCellChanged += UpdateVisual;
    }
    public void UpdateVisual(object sender, TileCell tileCell){
        Vector3Int tilePosition = new Vector3Int(tileCell.x, tileCell.y) - offset;
        ground.SetTile(tilePosition, tileCell.GetGroundTile());
        building.SetTile(tilePosition, tileCell.GetBuildingTile(currentDirection));
        Debug.Log("Visual Updated!");
    }

    public Vector3Int GetLogicPosition(Vector3 position){
        return ground.WorldToCell(position) + offset;
    }

    public void SetDirection(int direction){
        currentDirection = direction;
    }
}
