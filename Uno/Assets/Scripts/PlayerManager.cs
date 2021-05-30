using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int id;
    private string userName;
    [SerializeField]
    private Text playerName;

    public GameObject unoPrefab;

    public GameObject myTurnSign;

    public GameObject UnoSign;

    public GameObject reportButton;

    public GameObject checkDoneButton;

    public GameObject winnerSign;

    public Card selectedCard = null;

    [SerializeField]
    private HorizontalLayoutGroup cardField;

    [SerializeField]
    private Image avatar;

    public List<UnoDisplay> unoDisplays = new List<UnoDisplay>();

    

    public void setPlayerName(string _userName) {
        userName = _userName;
        playerName.text = userName;
    }

    public void addCard(Card _newCard) {
        GameObject _newUno = Instantiate(unoPrefab, cardField.transform);
        _newUno.GetComponent<UnoDisplay>().setCard(_newCard);
        
        unoDisplays.Add(_newUno.GetComponent<UnoDisplay>());
        sortCards();
       // logOutCards();
    }


    
    public void addCardUnknown(Card _newCard) {
        GameObject _newUno = Instantiate(unoPrefab, cardField.transform);
        _newUno.GetComponent<UnoDisplay>().setCard(_newCard);
        _newUno.GetComponent<UnoDisplay>().turnToBack();
        
        Destroy(_newUno.GetComponent<PickUnoCard>());
        unoDisplays.Add(_newUno.GetComponent<UnoDisplay>());
        sortCards();


    }

    public void setAvatar(Sprite _avatar) {
        avatar.sprite = _avatar;
    }

    private void logOutCards() {
        foreach(var x in unoDisplays) {
            Debug.Log(x.card.value);
        }
        Debug.Log("---------------");
    }

    public void sortCards() {
        unoDisplays.Sort((p1, p2) => {
            if(p1.card.value != p2.card.value) {
                return p1.card.value - p2.card.value;
            }
            return p1.card.color - p2.card.color;
        });

        updateDisplayOrder();
        
    }

    private void updateDisplayOrder() {
        for (int i = 0; i < unoDisplays.Count; i++) {
            unoDisplays[i].transform.SetSiblingIndex(i);
        }
    }

    public void PlayCard(Card _card) {
        //TODO
        foreach(var x in unoDisplays) {
            if (x.card.Equals(_card) || (x.card.value == VALUE.PLUS4 && _card.value == VALUE.PLUS4)|| (x.card.value == VALUE.CHANGE && _card.value == VALUE.CHANGE)) {
                unoDisplays.Remove(x);
                Debug.Log($"{x.card.value} is played.");
                Destroy(x.gameObject);
                break;
            }
        }
        updateDisplayOrder();
    }

    public void unPickAllCards() {
        foreach(var x in unoDisplays) {
            x.GetComponent<PickUnoCard>().setPick(false);
        }
    }

    public void showAllCards(bool f) {
        foreach (var x in unoDisplays) {
            if (f)
                x.turnToFront();
            else x.turnToBack();
        }
    }

    public void showCheckDoneButton() {
        checkDoneButton.SetActive(true);   
    }

    public void showReportButton(bool f) {
        reportButton.SetActive(f);
    }

    public void showMyTurnSign(bool f) {
        myTurnSign.SetActive(f);
    }

    public void win() {
        winnerSign.SetActive(true);
        UIManager_game.instance.restartButton.SetActive(true);
    }

    public void Uno() {
        AudioManager.instance.Play("uno");
        UnoSign.SetActive(true);
    }
}
