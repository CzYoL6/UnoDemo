using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDoneButton : MonoBehaviour
{
    private void Start() {
        
    }
    public void checkDone(int playerId) {
        GameManager_game.instance.otherPlayers[playerId].GetComponent<PlayerManager>().showAllCards(false);
        GameManager_game.instance.checkIfQuestionStands(playerId);
        
        gameObject.SetActive(false);
        //GameManager.instance.Restart();
    }
}
