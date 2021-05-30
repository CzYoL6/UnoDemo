using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterButton : MonoBehaviour
{
    public void EnterGame() {
        if (UIManager_welcome.instance.userNameFiled.text == "") return;
        Client.instance.userName = UIManager_welcome.instance.userNameFiled.text;
        GameManager_welcome.instance.loadScene(1);
    }
}
