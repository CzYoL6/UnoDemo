using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public ServerInfo serverInfo;
    public Text text;
    public void enterServer() {
        /*if (UIManager.instance.roomPanelUserNameInputField.GetComponent<InputField>().text == "")
            return;*/
        /*startMenu.SetActive(false);
        usernameField.enabled = false;*/
        GameManager_room.instance.EnterRoom(serverInfo.ip, serverInfo.port, serverInfo.maxPlayer);
    }
    public void setInfo(ServerInfo _serverInfo) {
        serverInfo = _serverInfo;
        text.text = serverInfo.name + " ("+ serverInfo.onlinePlayer.ToString() + " / " + serverInfo.maxPlayer.ToString() +")";
        if (serverInfo.onlinePlayer == serverInfo.maxPlayer) GetComponent<Button>().enabled = false;
        else GetComponent<Button>().enabled = true;
    }
}
