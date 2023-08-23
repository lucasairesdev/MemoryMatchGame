using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour {

    bool isShown = false;
    bool isMatch = false;
    [SerializeField] UnityEngine.UI.Image image;

    public void OnButtonClick() {
        if(isShown == true) {
            HideCard();
        } else {
            ShowCard();
        }
    }

    void ShowCard() {
        image.enabled = true;
        isShown = true;
    }

    public void HideCard() {
        image.enabled = false;
        isShown = false;
    }

}