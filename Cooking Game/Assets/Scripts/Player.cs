using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask layerMask;

    private ClearCounter selectedCounter;
    private Vector3 lastMoveDir;
    private bool isWalking;

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if(selectedCounter != null) {
            selectedCounter.Interact();
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastMoveDir = moveDir;
        }

        //CHECK IF THE PLAYER COLLIDES WITH CLEAR COUNTER OBJECT AND IF IT HAS THE CLEAR COUNTER SCRIPT
        if (Physics.Raycast(transform.position, lastMoveDir, out RaycastHit raycastHit, interactDistance, layerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                //HAS CLEARCOUNTER
                //ACCESS ITS FUNCTION
                if (clearCounter != selectedCounter) {
                    selectedCounter = clearCounter;
                }
            }
            else {
                selectedCounter = null;
            }
        }
        else {
            selectedCounter = null;
        }
        Debug.Log(selectedCounter);
    }

    private void HandleMovement() {
        //GETTING INPUT
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        //CHECK IF PLAYER CAN MOVE IN MOVEDIR OR NOT
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //CAN NOT MOVE TO MOVEDIR
        if (!canMove) {
            //CHECK IF CAN MOVE IN ONLY ONE AXIS (X)
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            //IF CAN MOVE TO X THEN SET MOVEDIR TO X
            if (canMove) {
                moveDir = moveDirX;
            }

            //IF CAN'T MOVE THEN CHECK THE OTHER AXIS (Z)
            else {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                //IF CAN MOVE TO Z THEN SET MOVEDIR TO Z
                if (canMove) {
                    moveDir = moveDirZ;
                }
                else {
                    //CANNOT MOVE IN BOTH SO DO NOTHING
                }
            }
        }

        //CAN MOVE TO MOVEDIR SO MOVING THE OBJECT
        if (canMove) {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        //SETTING THE BOOL TO PASS ON TO ANIMATOR SCRIPT
        isWalking = (moveDir != Vector3.zero);

        //ROTATING THE OBJECT TO FACE THE DIRECTION IT IS MOVING IN
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }
}
