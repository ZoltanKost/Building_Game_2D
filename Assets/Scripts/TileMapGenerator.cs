using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    private Actions currentAction = Actions.Move;

    private Camera cameraMain;
    public Resource[] GlobalResources;
    public static Storage g_Storage;

    public Resource[] resources;

    [Header("Tiles")]
    public Tile[] ground;
    private IBuildableSO currentBuildingSO;
    public RuleTile roadTiles;

    private CellGrid grid;
    private GridVisual gridVisual;

    // [Header("TileMaps")]
    private Tilemap[] tilemaps;

    [Header("Properties")]
    public int width = 10;
    public int height = 10;
    public int currentDirection = 0;

    public void Awake(){
        cameraMain = Camera.main;
        tilemaps = GetComponentsInChildren<Tilemap>();
        gridVisual = new GridVisual(tilemaps,new Vector3Int(width / 2, height / 2));
        grid = new CellGrid(width, height, ground);
        g_Storage = new Storage(GlobalResources, 100);
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            gridVisual.SetDirection(grid.SwitchDirection(false));
            grid.RevertMapRight();
        }else if(Input.GetKeyDown(KeyCode.Q)){
            gridVisual.SetDirection(grid.SwitchDirection(true));
            grid.RevertMapLeft();
        }else if(Input.GetKeyDown(KeyCode.P)){
            grid.FillWithHouses(currentBuildingSO);
        }

        if(currentAction == Actions.Move) {
            return;
        }else if(currentAction != Actions.Move && Input.GetKeyDown(KeyCode.Escape)){
            currentAction = Actions.Move;
            return;
        }

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)){
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit)){
                switch(currentAction){
                    case Actions.PlaceRoad:
                        Vector3Int lp = gridVisual.GetLogicPosition(hit.point);
                        grid.PlaceRoad(lp.x,lp.y, roadTiles);
                    break;
                    case Actions.Build:
                        lp = gridVisual.GetLogicPosition(hit.point);
                        grid.BuildTile(lp.x, lp.y, currentBuildingSO);
                    break;
                    case Actions.DestroyBuildingOrRoad:
                    break;
                }
            }
        }
        else if(Input.GetMouseButtonDown(1) || Input.GetMouseButton(1)){
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit)){
                Vector3Int lp = gridVisual.GetLogicPosition(hit.point);
                grid.DestroyTile(lp.x, lp.y);
            }
        }
    }

    public void SetCurrentBuildingTile(IBuildableSO building, Actions action){
        currentBuildingSO = building;
        currentAction = action;
    }
    
}

public enum Actions{
    PlaceRoad,
    Build,
    DestroyBuildingOrRoad,
    Move
}
