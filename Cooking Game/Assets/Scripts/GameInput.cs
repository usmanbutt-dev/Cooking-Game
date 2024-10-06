using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
        Debug.Log(obj);
    }

    public Vector2 GetMovementVectorNormalized() {
        //TAKING INPUT
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();


        //PROCESSING THE INPUT (IF NEEDED)
        inputVector = inputVector.normalized;
        return inputVector;
    } 
}
