using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void PlayCard(bool uno) {
        Card _selectedCard = GameManager_game.instance.localPlayer.GetComponent<PlayerManager>().selectedCard;
        Card _lastCard = GameManager_game.instance.theLastCard;
        if (_lastCard.color == _selectedCard.color || _lastCard.value == _selectedCard.value) {
            //ºÏ·¨
            GameManager_game.instance.mePlayCard(uno);
            
        }
        else {
            GameManager_game.instance.localPlayer.GetComponent<PlayerManager>().unPickAllCards();
        }
        gameObject.SetActive(false);
    }

}
