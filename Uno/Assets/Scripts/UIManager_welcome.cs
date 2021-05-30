using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_welcome : MonoBehaviour
{
    public static UIManager_welcome instance;



    public InputField userNameFiled;

    //public GameObject backButton;

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
