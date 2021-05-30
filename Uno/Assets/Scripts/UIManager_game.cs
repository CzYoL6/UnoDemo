using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_game : MonoBehaviour
{
    public static UIManager_game instance;

    public GameObject deck;

    public GameObject deadWood;

    public GameObject myTurnSign;

    public GameObject changeColor;

    public GameObject cancelButton;

    public GameObject playButton;

    public GameObject questionButton;

    public GameObject unoButton;

    public GameObject lastButOneCard;

    public GameObject lastCard;

    public GameObject dirSign;

    public GameObject restartButton;

    public  GameObject hasToSkipButton;

    public GameObject backButton;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this);
        }
    }
}
