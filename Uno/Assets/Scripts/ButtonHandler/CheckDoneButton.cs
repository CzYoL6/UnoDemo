using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDoneButton : MonoBehaviour
{
    private void Start() {
        
    }
    public void checkDone(int playerId) {
        GameManager.instance.otherPlayers[playerId].GetComponent<PlayerManager>().showAllCards(false);
        GameManager.instance.checkIfQuestionStands(playerId);
        
        gameObject.SetActive(false);
        //GameManager.instance.Restart();
    }
}
