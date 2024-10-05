using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized() {
        //TAKING INPUT
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();


        //PROCESSING THE INPUT (IF NEEDED)
        inputVector = inputVector.normalized;
        return inputVector;
    } 
}
