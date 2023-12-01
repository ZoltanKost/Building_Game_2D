using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildableObject
{
    public void Initialize(TileCell tileCell, IBuildableSO buildableSO);
    // IBuildable
    public IBuildableSO GetBuildableSO();
    //IBuildable
    public TileCell GetTileCell();
    //IBuildable
    public bool CanBeDestoyedByBuilding();
    //IBuildable 
    public void Demontage();
}
