using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleButton : MonoBehaviour
{
    private bool showing = false;
    public Sprite ruleIcon, ShutIcon;
    public void showRule() {
        if (!showing) {
            UIManager_welcome.instance.RulePanel.SetActive(true);
            //GetComponentInChildren<Text>().text = "πÿ±’";
            GetComponent<Image>().sprite = ShutIcon;
            showing = true;
        }
        else {
            UIManager_welcome.instance.RulePanel.SetActive(false);
            //GetComponentInChildren<Text>().text = "πÊ‘Ú";
            GetComponent<Image>().sprite = ruleIcon;
            showing = false;
        }
    }
}
