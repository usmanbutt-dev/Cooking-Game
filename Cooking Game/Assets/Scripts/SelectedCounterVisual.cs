using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    //ACCESSING THE EVENT THROUGH THE PLAYER'S SINGLETON PATTERN
    //NOT DOING IT ON AWAKE SO THAT THIS CODE DOESN'T RUN BEFORE THE PLAYER CODE'S AWAKE
    private void Start() {
        Player.Instance.OnSelectCounterChange += player_OnSelectCounterChange;        
    }

    private void player_OnSelectCounterChange(object sender, Player.OnSelectCounterChangeEventArgs e) {
        if(e.selectedCounter == clearCounter) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        visualGameObject.SetActive(true);
    }

    private void Hide() {
        visualGameObject.SetActive(false);
    }
}