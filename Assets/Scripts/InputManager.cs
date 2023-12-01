using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputActions InputActions;
    public void Awake(){
        InputActions = new InputActions();
        // InputActions.GameInput.Escape.performed += OnEscapePerformed;
    }

    // private void OnEscapePerformed(CallbackContext i){

    // }
}
