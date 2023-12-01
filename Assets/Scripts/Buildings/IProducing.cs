using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProducing : IBuildableObject
{
    public void Produce(float delta);
    public Storage GetStorage();
    public float GetProductionTime();
    public void UpdateStorage();
    public bool CheckIfCanProduce();
    
}
