using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

    public static GameManager instance;

    

    [SerializeField]


    public GameObject localPlayer;


    public GameObject[] otherPlayers;

    public GameObject unoPrefab;

    public bool myTurn;

    //public GameObject testSign;

    [HideInInspector]
    public List<Card> deck;     //√˛≈∆∂—
    [HideInInspector] 
    public List<Card> deadWood; //∆˙≈∆∂—


    public Card theLastCard = null;

    public void checkIfQuestionStands(int otherPlayerIdinClient) {
        bool f = false ;
        foreach(var x in otherPlayers[otherPlayerIdinClient].GetComponent<PlayerManager>().unoDisplays) {
            if(x.card.color == theLastButOneCard.color) {
                f = true;
                break;
            }
        }

        //÷ “…≥…π¶
        if (f) {
            AudioManager.instance.Play("questionSucceed");
            ClientSend.QuestionStands(true);
            myTurn = true;
            //ItsMyTurn(true);
        }
        // ß∞‹
        else {
            AudioManager.instance.Play("questionFail");
            ClientSend.QuestionStands(false);
            ItsMyTurn(false);
        }
        otherPlayers[otherPlayerIdinClient].GetComponent<PlayerManager>().checkDoneButton.SetActive(false);
        //ItsMyTurn(false);
    }

    public Card theLastButOneCard = null;
    public int idOfLastPlayer;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this);
        }

        //testRule();
    }
    private void Start() {
        //lastCard = Card.getNewCard(VALUE.FIVE, COLOR.YELLOW);
        //localPlayer.GetComponent<PlayerManager>().showMyTurnSign(true);
        AudioManager.instance.Play("bgm");

        //ClientSend.restfulapi_GetServerInfos();

    }

    public void reportSuccess() {
        ClientSend.ReportSuccess();

    }
    public void SpawnPlayer(int _id, string _userName) {
        int nextid1 = (Client.instance.myId) % 4 + 1;
        int nextid2 = (nextid1) % 4 + 1;
        int nextid3 = (nextid2) % 4 + 1;
        if (_id == Client.instance.myId) {
            localPlayer.SetActive(true);
            localPlayer.GetComponent<PlayerManager>().setPlayerName(_userName);
            localPlayer.GetComponent<PlayerManager>().setAvatar(Avatars.instance.sprites[_id - 1]);
        }
        else if(_id == nextid1) {
            otherPlayers[0].SetActive(true);
            otherPlayers[0].GetComponent<PlayerManager>().setPlayerName(_userName);
            otherPlayers[0].GetComponent<PlayerManager>().setAvatar(Avatars.instance.sprites[_id - 1]);
        }
        else if (_id == nextid2) {
            otherPlayers[1].SetActive(true);
            otherPlayers[1].GetComponent<PlayerManager>().setPlayerName(_userName);
            otherPlayers[1].GetComponent<PlayerManager>().setAvatar(Avatars.instance.sprites[_id - 1]);
        }
        else if (_id == nextid3) {
            otherPlayers[2].SetActive(true);
            otherPlayers[2].GetComponent<PlayerManager>().setPlayerName(_userName);
            otherPlayers[2].GetComponent<PlayerManager>().setAvatar(Avatars.instance.sprites[_id - 1]);
        }

    }

    public void CanReport(int _id) {
        int nextid1 = (Client.instance.myId) % 4 + 1;
        int nextid2 = (nextid1) % 4 + 1;
        int nextid3 = (nextid2) % 4 + 1;
        if (_id == nextid1) {
            
            otherPlayers[0].GetComponent<PlayerManager>().showReportButton(true);
        }
        else if (_id == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().showReportButton(true);
        }
        else if (_id == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().showReportButton(true);
        }
    }

    public void HasToSkip() {
        myTurn = false;
        UIManager.instance.hasToSkipButton.SetActive(true);
    }

    public void updateLastAndLastButOne(Card _lastCard, Card _lastButOneCard) {
        theLastCard = _lastCard;
        theLastButOneCard = _lastButOneCard;
        for (int i = 0; i < UIManager.instance.lastCard.transform.childCount; i++)
            Destroy(UIManager.instance.lastCard.transform.GetChild(i).gameObject);
        for (int i = 0; i < UIManager.instance.lastButOneCard.transform.childCount; i++)
            Destroy(UIManager.instance.lastButOneCard.transform.GetChild(i).gameObject);
        if (theLastCard != null) {
            GameObject lastUno = Instantiate(unoPrefab, UIManager.instance.lastCard.transform);
            Destroy(lastUno.GetComponent<PickUnoCard>());
            lastUno.GetComponent<UnoDisplay>().setCard(_lastCard);
        }
        if (theLastButOneCard != null) {
            GameObject lastButOneUno = Instantiate(unoPrefab, UIManager.instance.lastButOneCard.transform);
            Destroy(lastButOneUno.GetComponent<PickUnoCard>());
            lastButOneUno.GetComponent<UnoDisplay>().setCard(_lastButOneCard);
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SomeoneUno(int id) {
        
        int nextid1 = (Client.instance.myId) % 4 + 1;
        int nextid2 = (nextid1) % 4 + 1;
        int nextid3 = (nextid2) % 4 + 1;

        if (id == Client.instance.myId) {
            localPlayer.GetComponent<PlayerManager>().Uno();
        }
        else if (id == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().Uno();
        }
        else if (id == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().Uno();
        }
        else if (id == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().Uno();
        }
    }

    public void ChangeDir(int dir) {
        UIManager.instance.dirSign.SetActive(true);
        UIManager.instance.dirSign.GetComponent<ArrowSpinning>().changeDir();
    }

    public void WhosTurn(int whosTurn) {
        //testSign.SetActive(true);
        localPlayer.GetComponent<PlayerManager>().showMyTurnSign(false);
        otherPlayers[0].GetComponent<PlayerManager>().showMyTurnSign(false);
        otherPlayers[1].GetComponent<PlayerManager>().showMyTurnSign(false);
        otherPlayers[2].GetComponent<PlayerManager>().showMyTurnSign(false);

        int nextid1 = (Client.instance.myId) % 4 + 1;
        int nextid2 = (nextid1) % 4 + 1;
        int nextid3 = (nextid2) % 4 + 1;

        if (whosTurn == Client.instance.myId) {
            localPlayer.GetComponent<PlayerManager>().showMyTurnSign(true);
        }
        else if (whosTurn == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().showMyTurnSign(true);
        }
        else if (whosTurn == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().showMyTurnSign(true);
        }
        else if (whosTurn == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().showMyTurnSign(true);
        }
    }

    public void winAndRestart(int winnerId) {
        AudioManager.instance.Play("win");
        int nextid1 = (Client.instance.myId) % 4 + 1;
        int nextid2 = (nextid1) % 4 + 1;
        int nextid3 = (nextid2) % 4 + 1;

        if (winnerId == Client.instance.myId) {
            localPlayer.GetComponent<PlayerManager>().win();
        }
        else if (winnerId == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().win();
        }
        else if (winnerId == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().win();
        }
        else if (winnerId == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().win();
        }
    }

    public void ItsMyTurn(bool f) {
        UIManager.instance.myTurnSign.SetActive(f);
        myTurn = f;
    }

    public void DealCardToMe(Card _card) {
        //TODO: º”∂Øª≠
        AudioManager.instance.Play("fapai");
        localPlayer.GetComponent<PlayerManager>().addCard(_card);
    }

    public void DealCardToOtherPlayer(int _playerId, Card _card) {
        AudioManager.instance.Play("fapai");

        int nextid1 = (Client.instance.myId) % 4 + 1;
        int nextid2 = (nextid1) % 4 + 1;
        int nextid3 = (nextid2) % 4 + 1;

        //TODO: º”∂Øª≠

        if (_playerId == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().addCardUnknown(_card);
        }
        else if(_playerId == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().addCardUnknown(_card);
        }
        else if(_playerId == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().addCardUnknown(_card);
        }
    }

    public void CheckIfCanContinue(Card lastCard) {
        foreach (var x in localPlayer.GetComponent<PlayerManager>().unoDisplays) {
            if (x.card.color == lastCard.color || x.card.value == lastCard.value || x.card.color == COLOR.ANY) {
                return;
            }
        }
        myTurn = false;
        UIManager.instance.cancelButton.SetActive(true);
    }

    public void mePlayCard(bool uno) {
        if (!myTurn) return;
        AudioManager.instance.Play("dapai");
        if (uno) {
            localPlayer.GetComponent<PlayerManager>().Uno();
        }

        localPlayer.GetComponent<PlayerManager>().PlayCard(localPlayer.GetComponent<PlayerManager>().selectedCard);
        ClientSend.PlayCard(uno, localPlayer.GetComponent<PlayerManager>().unoDisplays.Count);

        updateLastAndLastButOne(localPlayer.GetComponent<PlayerManager>().selectedCard, theLastCard);

        if (localPlayer.GetComponent<PlayerManager>().unoDisplays.Count == 0) ClientSend.Win();

        //lastCard = localPlayer.GetComponent<PlayerManager>().selectedCard;
        localPlayer.GetComponent<PlayerManager>().selectedCard = null;
        localPlayer.GetComponent<PlayerManager>().unPickAllCards();
        ItsMyTurn(false);

    }

    public void otherPlayCard(int _player, Card _card, bool shoutUno, int leftCount) {
        AudioManager.instance.Play("dapai");
        int nextid1 = (Client.instance.myId) % 4 + 1;
        int nextid2 = (nextid1) % 4 + 1;
        int nextid3 = (nextid2) % 4 + 1;

        if(_player == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().PlayCard(_card);
        }else if(_player == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().PlayCard(_card);
        }
        else if(_player == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().PlayCard(_card);
        }

        
        Debug.Log($"Player {_player} played {_card.value} , {_card.color}");
    }

    public void ProcessQuestion() {
        myTurn = false;
        UIManager.instance.questionButton.SetActive(true);
    }

    public void SomeoneReportSucceed() {

        otherPlayers[0].GetComponent<PlayerManager>().showReportButton(false);
        
        otherPlayers[1].GetComponent<PlayerManager>().showReportButton(false);
        
        otherPlayers[2].GetComponent<PlayerManager>().showReportButton(false);
    }
}
