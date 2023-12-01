using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    TileMapGenerator tileMapGenerator;
    Actions currentAction;
    Image image;
    public void Start(){
        tileMapGenerator = FindObjectOfType<TileMapGenerator>();
    }
    public void SetCurrentBuildingAction(IBuildableSO building, Actions action){
        currentAction = action;
        tileMapGenerator.SetCurrentBuildingTile(building, currentAction);
    }

    public void SetUpResourceUI(Resource resource){

    }
}
