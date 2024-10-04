using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] float rotateSpeed = 10f;
    private bool isWalking;

    private void Update() {

        //TAKING INPUT
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }

        //PROCESSING THE INPUT (IF NEEDED)
        inputVector = inputVector.normalized;

        //MOVING THE OBJECT
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = (moveDir != Vector3.zero);

        //ROTATING THE OBJECT TO FACE THE DIRECTION IT IS MOVING IN
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
