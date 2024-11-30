using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        //FIRST WE REMOVE THE KITCHENOBJECT FROM THE CURRENT CLEARCOUNTER 
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        //CHANGE THE CURRENT CLEARCOUNTER TO NEXT ONE
        this.kitchenObjectParent = kitchenObjectParent;

        if(kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("KitchenObjectParent ALREADY HAS AN OBJECT!");
        }
        //CHANGE THIS OBJECT IN THE NEW CLEARCOUNTER'S KITCHEN OBJECT
        kitchenObjectParent.SetKitchenObject(this);

        //CHANGING ITS PARENT TO SET IT AS THE NEW COUNTERS CHILD AND SETTING ITS POSITION TO ITS COUNTERTOP POSITION
        transform.parent = kitchenObjectParent.GetCounterTopPoint();
        transform.localPosition = Vector3.zero;
    }

    public KitchenObjectSO KitchenObjectSO () {
        return kitchenObjectSO;
    }

}