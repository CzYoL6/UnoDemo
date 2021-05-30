using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public void ChangeTheColorTo(int color) {
        GameManager_game.instance.localPlayer.GetComponent<PlayerManager>().selectedCard = Card.getNewCard((VALUE)GameManager_game.instance.localPlayer.GetComponent<PlayerManager>().selectedCard.value
            ,(COLOR)color);
        GameManager_game.instance.mePlayCard(false);
        gameObject.SetActive(false);
    }
}
