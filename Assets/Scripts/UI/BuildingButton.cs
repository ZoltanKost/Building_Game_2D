using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour{
    
    private UIManager UIManager;
    public ScriptableObject buildingSO;
    [SerializeField] private Actions action;
    public void Start(){
        UIManager = GetComponentInParent<UIManager>();
    }
    
    public void Click(){
        UIManager.SetCurrentBuildingAction(buildingSO as IBuildableSO, action);
    }

}
