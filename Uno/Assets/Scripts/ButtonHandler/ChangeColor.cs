using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public void ChangeTheColorTo(int color) {
        GameManager.instance.localPlayer.GetComponent<PlayerManager>().selectedCard = Card.getNewCard((VALUE)GameManager.instance.localPlayer.GetComponent<PlayerManager>().selectedCard.value
            ,(COLOR)color);
        GameManager.instance.mePlayCard(false);
        gameObject.SetActive(false);
    }
}
