using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject menu;

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

    public GameObject roomPanelUserNameInputField;
    public GameObject roomPanel;

    public GameObject backButton;

    public GameObject RulePanel;

    public GameObject ruleButton;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this);
        }
    }
}
