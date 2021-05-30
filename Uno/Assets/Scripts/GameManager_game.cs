using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_game : MonoBehaviour
{
    

    public static GameManager_game instance;

    

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
        //AudioManager.instance.Play("bgm");
        Client.instance.ConnectToServer(ServerInfoKeeper.instance.ip, ServerInfoKeeper.instance.port);
        //DontDestroyOnLoad(gameObject);
        //ClientSend.restfulapi_GetServerInfos();

    }

    public void reportSuccess() {
        ClientSend.ReportSuccess();

    }
    public void SpawnPlayer(int _id, string _userName) {
        int nextid1 = (Client.instance.myId) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid2 = (nextid1) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid3 = (nextid2) % ServerInfoKeeper.instance.maxPlayer + 1;

        if (_id == Client.instance.myId) {
            localPlayer.SetActive(true);
            localPlayer.GetComponent<PlayerManager>().setPlayerName(_userName);
            localPlayer.GetComponent<PlayerManager>().setAvatar(Avatars.instance.sprites[_id - 1]);
        }
        else if(ServerInfoKeeper.instance.maxPlayer >= 2 && _id == nextid1) {
            otherPlayers[0].SetActive(true);
            otherPlayers[0].GetComponent<PlayerManager>().setPlayerName(_userName);
            otherPlayers[0].GetComponent<PlayerManager>().setAvatar(Avatars.instance.sprites[_id - 1]);
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 3 && _id == nextid2) {
            otherPlayers[1].SetActive(true);
            otherPlayers[1].GetComponent<PlayerManager>().setPlayerName(_userName);
            otherPlayers[1].GetComponent<PlayerManager>().setAvatar(Avatars.instance.sprites[_id - 1]);
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 4 && _id == nextid3) {
            otherPlayers[2].SetActive(true);
            otherPlayers[2].GetComponent<PlayerManager>().setPlayerName(_userName);
            otherPlayers[2].GetComponent<PlayerManager>().setAvatar(Avatars.instance.sprites[_id - 1]);
        }

    }

    public void CanReport(int _id) {
        int nextid1 = (Client.instance.myId) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid2 = (nextid1) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid3 = (nextid2) % ServerInfoKeeper.instance.maxPlayer + 1;
        if (ServerInfoKeeper.instance.maxPlayer >= 2 && _id == nextid1) {
            
            otherPlayers[0].GetComponent<PlayerManager>().showReportButton(true);
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 3 && _id == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().showReportButton(true);
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 4 && _id == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().showReportButton(true);
        }
    }

    public void HasToSkip() {
        myTurn = false;
        UIManager_game.instance.hasToSkipButton.SetActive(true);
    }

    public void updateLastAndLastButOne(Card _lastCard, Card _lastButOneCard) {
        theLastCard = _lastCard;
        theLastButOneCard = _lastButOneCard;
        for (int i = 0; i < UIManager_game.instance.lastCard.transform.childCount; i++)
            Destroy(UIManager_game.instance.lastCard.transform.GetChild(i).gameObject);
        for (int i = 0; i < UIManager_game.instance.lastButOneCard.transform.childCount; i++)
            Destroy(UIManager_game.instance.lastButOneCard.transform.GetChild(i).gameObject);
        if (theLastCard != null) {
            GameObject lastUno = Instantiate(unoPrefab, UIManager_game.instance.lastCard.transform);
            Destroy(lastUno.GetComponent<PickUnoCard>());
            lastUno.GetComponent<UnoDisplay>().setCard(_lastCard);
        }
        if (theLastButOneCard != null) {
            GameObject lastButOneUno = Instantiate(unoPrefab, UIManager_game.instance.lastButOneCard.transform);
            Destroy(lastButOneUno.GetComponent<PickUnoCard>());
            lastButOneUno.GetComponent<UnoDisplay>().setCard(_lastButOneCard);
        }
    }

    public void Restart() {
        SceneManager.LoadScene(1);
    }

    public void SomeoneUno(int id) {
        
        int nextid1 = (Client.instance.myId) % ServerInfoKeeper.instance.maxPlayer  + 1;
        int nextid2 = (nextid1) % ServerInfoKeeper.instance.maxPlayer  + 1;
        int nextid3 = (nextid2) % ServerInfoKeeper.instance.maxPlayer + 1;

        if (id == Client.instance.myId) {
            localPlayer.GetComponent<PlayerManager>().Uno();
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 2 && id == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().Uno();
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 3 && id == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().Uno();
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 4 && id == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().Uno();
        }
    }

    public void ChangeDir(int dir) {
        UIManager_game.instance.dirSign.SetActive(true);
        UIManager_game.instance.dirSign.GetComponent<ArrowSpinning>().changeDir();
    }

    public void WhosTurn(int whosTurn) {
        //testSign.SetActive(true);
        localPlayer.GetComponent<PlayerManager>().showMyTurnSign(false);
        foreach(var x in otherPlayers) x.GetComponent<PlayerManager>().showMyTurnSign(false);

        int nextid1 = (Client.instance.myId) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid2 = (nextid1) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid3 = (nextid2) % ServerInfoKeeper.instance.maxPlayer + 1;

        if (whosTurn == Client.instance.myId) {
            localPlayer.GetComponent<PlayerManager>().showMyTurnSign(true);
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 2&& whosTurn == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().showMyTurnSign(true);
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 3 && whosTurn == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().showMyTurnSign(true);
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 4 && whosTurn == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().showMyTurnSign(true);
        }
    }

    public void winAndRestart(int winnerId) {
        AudioManager.instance.Play("win");
        int nextid1 = (Client.instance.myId) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid2 = (nextid1) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid3 = (nextid2) % ServerInfoKeeper.instance.maxPlayer + 1;

        if (winnerId == Client.instance.myId) {
            localPlayer.GetComponent<PlayerManager>().win();
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 2 && winnerId == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().win();
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 3 && winnerId == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().win();
        }
        else if (ServerInfoKeeper.instance.maxPlayer >= 4 && winnerId == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().win();
        }
    }

    public void ItsMyTurn(bool f) {
        UIManager_game.instance.myTurnSign.SetActive(f);
        myTurn = f;
    }

    public void DealCardToMe(Card _card) {
        //TODO: º”∂Øª≠
        AudioManager.instance.Play("fapai");
        localPlayer.GetComponent<PlayerManager>().addCard(_card);
    }

    public void DealCardToOtherPlayer(int _playerId, Card _card) {
        AudioManager.instance.Play("fapai");

        int nextid1 = (Client.instance.myId) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid2 = (nextid1) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid3 = (nextid2) % ServerInfoKeeper.instance.maxPlayer + 1;

        //TODO: º”∂Øª≠

        if (ServerInfoKeeper.instance.maxPlayer >= 2 && _playerId == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().addCardUnknown(_card);
        }
        else if(ServerInfoKeeper.instance.maxPlayer >= 3 && _playerId == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().addCardUnknown(_card);
        }
        else if(ServerInfoKeeper.instance.maxPlayer >= 4 && _playerId == nextid3) {
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
        UIManager_game.instance.cancelButton.SetActive(true);
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
        int nextid1 = (Client.instance.myId) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid2 = (nextid1) % ServerInfoKeeper.instance.maxPlayer + 1;
        int nextid3 = (nextid2) % ServerInfoKeeper.instance.maxPlayer + 1;

        if(ServerInfoKeeper.instance.maxPlayer >= 2 && _player == nextid1) {
            otherPlayers[0].GetComponent<PlayerManager>().PlayCard(_card);
        }else if(ServerInfoKeeper.instance.maxPlayer >= 3 && _player == nextid2) {
            otherPlayers[1].GetComponent<PlayerManager>().PlayCard(_card);
        }
        else if(ServerInfoKeeper.instance.maxPlayer >= 4 && _player == nextid3) {
            otherPlayers[2].GetComponent<PlayerManager>().PlayCard(_card);
        }

        
        Debug.Log($"Player {_player} played {_card.value} , {_card.color}");
    }

    public void ProcessQuestion() {
        myTurn = false;
        UIManager_game.instance.questionButton.SetActive(true);
    }

    public void SomeoneReportSucceed() {
        foreach(var x in otherPlayers) x.GetComponent<PlayerManager>().showReportButton(false);
    }

    public void loadScene(int index) {
        SceneManager.LoadScene(index);
    }
}
