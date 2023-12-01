using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character{

    public Building house;
    public Building work;
    public int speed;
    
    public Character(Building house){
        this.house = house;
    }

    public void SetWork(Building work){
        this.work = work;
    }

}
