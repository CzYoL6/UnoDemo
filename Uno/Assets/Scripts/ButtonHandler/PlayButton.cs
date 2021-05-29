using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void PlayCard(bool uno) {
        Card _selectedCard = GameManager.instance.localPlayer.GetComponent<PlayerManager>().selectedCard;
        Card _lastCard = GameManager.instance.theLastCard;
        if (_lastCard.color == _selectedCard.color || _lastCard.value == _selectedCard.value) {
            //ºÏ·¨
            GameManager.instance.mePlayCard(uno);
            
        }
        else {
            GameManager.instance.localPlayer.GetComponent<PlayerManager>().unPickAllCards();
        }
        gameObject.SetActive(false);
    }

}
