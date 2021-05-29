using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnoDisplay : MonoBehaviour
{
    public Card card;
    public Image image;

    public Sprite backImage;
    private void Start() {
        
    }

    public void turnToBack() {
        image.sprite = backImage;
    }

    public void turnToFront() {
        image.sprite = card.image;
    }

    public void setCard(Card _card) {
        card = _card;
        image.sprite = card.image;
    }
}
