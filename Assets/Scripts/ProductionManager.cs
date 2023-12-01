using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionManager : MonoBehaviour
{

    public static ProductionManager singleton{get; private set;}
    
    private Dictionary<IProducing, Coroutine> productions;
    private HashSet<IProducing> buildings;

    private WaitForSeconds waitForCheckTime;
    [SerializeField] private float checkTime = .5f;

    void Awake(){
        if(!singleton){
            singleton = this;
        }
        waitForCheckTime = new WaitForSeconds(checkTime);
        buildings = new HashSet<IProducing>(10000);
        productions = new Dictionary<IProducing, Coroutine>();
    }

    void Update(){
        foreach(IProducing producing in buildings){
            producing.Produce(Time.deltaTime);
        }
    }

    public void StartProducing(IProducing building){
        buildings.Add(building);
    }
    public void StopProducing(IProducing building){
        buildings.Remove(building);
    }
}
