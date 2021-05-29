using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUnoCard : MonoBehaviour
{

    private bool picked = false;
    //private bool mouseDown = false;

    public void MouseDown() {
        if (!GameManager.instance.myTurn) return;
        if (!picked && GameManager.instance.localPlayer.GetComponent<PlayerManager>().selectedCard != null) return;
        //mouseDown = true;
        if (picked) {
           // picking = false;
            setPick(false);
        }
        else {
            //picking = true;
            setPick(true);
        }
    }

    public void setPick(bool _picked) {
        picked = _picked;
        if (picked) {
            GetComponent<RectTransform>().pivot = new Vector2(0.5f, -5f);
            if (GetComponent<UnoDisplay>().card.color != COLOR.ANY) {
                UIManager.instance.playButton.SetActive(true);
                if(GameManager.instance.localPlayer.GetComponent<PlayerManager>().unoDisplays.Count == 2)
                    UIManager.instance.unoButton.SetActive(true);
            }
            else UIManager.instance.changeColor.SetActive(true);

            GameManager.instance.localPlayer.GetComponent<PlayerManager>().selectedCard = GetComponent<UnoDisplay>().card;
        }
        else {
            GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            UIManager.instance.playButton.SetActive(false);
            UIManager.instance.changeColor.SetActive(false);
            UIManager.instance.unoButton.SetActive(false);
            GameManager.instance.localPlayer.GetComponent<PlayerManager>().selectedCard = null;
        }
        
    }
}
