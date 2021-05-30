using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void restart() {
        Client.instance.Disconnect();
        GameManager_game.instance.Restart();
    }
}
