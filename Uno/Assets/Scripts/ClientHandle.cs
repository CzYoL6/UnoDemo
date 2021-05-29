using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet) {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        //send welcome received packet

        ClientSend.WelcomeReceived();
    }

    public static void SpawnPlayer(Packet _packet) {
        int _id = _packet.ReadInt();
        string _userName = _packet.ReadString();

        GameManager.instance.SpawnPlayer(_id, _userName);
    }

    public static void DealCardKnown(Packet _packet) {
        VALUE _value = (VALUE)_packet.ReadInt();
        COLOR _color = (COLOR)_packet.ReadInt();


        Card _newCard = Card.getNewCard(_value, _color);
        

        GameManager.instance.DealCardToMe(_newCard);

    }

    public static void DealCardUnknown(Packet _packet) {
        int _playerToDeal = _packet.ReadInt();
        VALUE _value = (VALUE)_packet.ReadInt();
        COLOR _color = (COLOR)_packet.ReadInt();

        Card _newCard = Card.getNewCard(_value, _color);
        GameManager.instance.DealCardToOtherPlayer(_playerToDeal, _newCard);

    }

    public static void OtherPlayCard(Packet _packet) {
        int _player = _packet.ReadInt();
        Card _card = _packet.ReadCard();

        bool shoutUno = _packet.ReadBool();
        int leftCount = _packet.ReadInt();

        if (shoutUno) {
            GameManager.instance.SomeoneUno(_player);
        }
        if(!shoutUno && leftCount == 1) {
            GameManager.instance.CanReport(_player);
        }

        if(_player == 0) {
            //开局的那张牌
            GameManager.instance.updateLastAndLastButOne(_card, null);
            return;
        }

        Card lastButOneCard = GameManager.instance.theLastCard;
        GameManager.instance.updateLastAndLastButOne(_card, lastButOneCard);

        GameManager.instance.otherPlayCard(_player, _card, shoutUno, leftCount);

    }

    public static void ItsMyTurn(Packet _packet) {

        bool ableToQuestion = _packet.ReadBool();
        bool hasToSkip = _packet.ReadBool();
        Card lastCard = _packet.ReadCard();
        bool lastButOneCardNull = _packet.ReadBool();
        Card lastButOneCard = null;
        if (!lastButOneCardNull)
            lastButOneCard = _packet.ReadCard();
        int lastPlayerId = _packet.ReadInt();

        GameManager.instance.updateLastAndLastButOne(lastCard, lastButOneCard);
        GameManager.instance.idOfLastPlayer = lastPlayerId;


        GameManager.instance.ItsMyTurn(true);

        if((lastCard.value == VALUE.SKIP || lastCard.value == VALUE.PLUS2) && hasToSkip) {
            GameManager.instance.HasToSkip();
            return;
        }

        //如果是+4就选择是否质疑
        if (lastCard.value == VALUE.PLUS4 && ableToQuestion) {
            GameManager.instance.ProcessQuestion();
            return;
        }

        

        
        GameManager.instance.CheckIfCanContinue(lastCard);
    }

    public static void Win(Packet _packet) {
        int winnerId = _packet.ReadInt();
        GameManager.instance.winAndRestart(winnerId);
    }

    public static void WhosTurn(Packet _packet) {
        int whosTurn = _packet.ReadInt();
        GameManager.instance.WhosTurn(whosTurn);
    }

    public static void ChangeDir(Packet _packet) {
        int dir = _packet.ReadInt();
        GameManager.instance.ChangeDir(dir);
    }

    public static void SomeoneReportSucceed(Packet _packet) {
        GameManager.instance.SomeoneReportSucceed();
    }

    public static void Restart(Packet _packet) {
        GameManager.instance.Restart();
        
    }
}
