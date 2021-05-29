using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionButtons : MonoBehaviour
{
    public void question() {
        int nextid1 = (Client.instance.myId) % 4 + 1;
        int nextid2 = (nextid1) % 4 + 1;
        int nextid3 = (nextid2) % 4 + 1;

        if (GameManager.instance.idOfLastPlayer == nextid1) {
            GameManager.instance.otherPlayers[0].GetComponent<PlayerManager>().showAllCards(true);
            GameManager.instance.otherPlayers[0].GetComponent<PlayerManager>().showCheckDoneButton();
        }
        else if (GameManager.instance.idOfLastPlayer == nextid2) {
            GameManager.instance.otherPlayers[1].GetComponent<PlayerManager>().showAllCards(true);
            GameManager.instance.otherPlayers[1].GetComponent<PlayerManager>().showCheckDoneButton();
        }
        else if (GameManager.instance.idOfLastPlayer == nextid3) {
            GameManager.instance.otherPlayers[2].GetComponent<PlayerManager>().showAllCards(true);
            GameManager.instance.otherPlayers[2].GetComponent<PlayerManager>().showCheckDoneButton();
        }
        gameObject.SetActive(false);
    }

    public void cancel() {
        AudioManager.instance.Play("skip");
        GameManager.instance.ItsMyTurn(false);
        ClientSend.QuestionCancel();
        gameObject.SetActive(false);
    }
}
