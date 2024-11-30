using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;


    //FOR TESTING SWITCHING OBJECT FROM ONE COUNTER TO ANOTHER
    [SerializeField] ClearCounter secondClearCounter;

    public void Interact(Player player) {
        //THIS CONDITION IS USED SO THAT IF THE COUNTERTOP IS EMPTY A NEW PREFAB WILL SPAWN, OTHERWISE WE CAN DO SOMETHING ELSE
        if(kitchenObject == null) {
            //SPAWN A NEW KITCHENOBJECT AND STORE IT IN KITCHENOBJECTTRANSFORM
            Transform KitchenObjectTransform= Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            //ACCESS THAT OBJECTS COMPONENT AND STORE IT IN KITCHENOBJECT & SET ITS CLEARCOUNTER VARIABLE TO THIS CLEARCOUNTER
            KitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else {
            //GIVE IT TO THE PLAYER
            if (player.HasKitchenObject()) {
                Debug.LogError("Player already has an object");
            }
            else {
                kitchenObject.SetKitchenObjectParent(player);
            }
        }
    }

    public Transform GetCounterTopPoint() {
        return counterTopPoint;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

}