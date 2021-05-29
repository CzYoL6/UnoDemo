using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject startMenu;
    public InputField usernameField;
    public InputField ipField;
    public InputField portField;
    public void ConnectToServer() {
        if (UIManager.instance.menu.GetComponent<Menu>().usernameField.text == "")
            return;
        /*startMenu.SetActive(false);
        usernameField.enabled = false;*/
        Client.instance.ConnectToServer(UIManager.instance.menu.GetComponent<Menu>().ipField.text, 
            int.Parse(UIManager.instance.menu.GetComponent<Menu>().portField.text));
        //gameObject.SetActive(false);
    }
}
