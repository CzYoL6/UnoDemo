using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_room : MonoBehaviour
{
    public static UIManager_room instance;

   
    public GameObject roomPanelUserName;
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

    private void Start() {
        roomPanelUserName.GetComponent<Text>().text = "My Name: " + Client.instance.userName;
    }
}
