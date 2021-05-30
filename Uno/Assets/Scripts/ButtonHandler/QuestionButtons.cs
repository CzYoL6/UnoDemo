using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionButtons : MonoBehaviour
{
    public void question() {
        int nextid1 = (Client.instance.myId) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid2 = (nextid1) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid3 = (nextid2) % ServerInfoKeeper.instance.maxPlayer + 1;

        if (ServerInfoKeeper.instance.maxPlayer >= 2 && GameManager_game.instance.idOfLastPlayer == nextid1) {
            GameManager_game.instance.otherPlayers[0].GetComponent<PlayerManager>().showAllCards(true);
            GameManager_game.instance.otherPlayers[0].GetComponent<PlayerManager>().showCheckDoneButton();
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 3 && GameManager_game.instance.idOfLastPlayer == nextid2) {
            GameManager_game.instance.otherPlayers[1].GetComponent<PlayerManager>().showAllCards(true);
            GameManager_game.instance.otherPlayers[1].GetComponent<PlayerManager>().showCheckDoneButton();
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 4 && GameManager_game.instance.idOfLastPlayer == nextid3) {
            GameManager_game.instance.otherPlayers[2].GetComponent<PlayerManager>().showAllCards(true);
            GameManager_game.instance.otherPlayers[2].GetComponent<PlayerManager>().showCheckDoneButton();
        }
        gameObject.SetActive(false);
    }

    public void cancel() {
        AudioManager.instance.Play("skip");
        GameManager_game.instance.ItsMyTurn(false);
        ClientSend.QuestionCancel();
        gameObject.SetActive(false);
    }
}
