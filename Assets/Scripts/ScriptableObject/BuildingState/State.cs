using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public interface IState{
//     public IState Tick(IProducing building, float delta);
// }

// public class ProductiveState : IState{
//     float time = 0f;
//     float producingTime = 0;
//     public IState Tick(IProducing building, float delta){

//         if(producingTime == 0){
//             producingTime = building.GetProductionTime();
//         }

//         time += delta;

//         if(time > producingTime){
            
//             building.UpdateStorage();
            
//             time = 0;
//             return building.GetNonProductiveState();
//         }
//         return this;
//     }
// }

// public class NonProductiveState : IState{
//     float time = 0;
//     public IState Tick(IProducing building, float delta){
//         time += delta;

//         if(!building.CheckIfCanProduce()) return this;
//         time = 0;
//         return building.GetProductiveState();
//     }
// } 
