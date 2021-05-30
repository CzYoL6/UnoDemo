using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_room : MonoBehaviour
{
    

    public static GameManager_room instance;

    public int[] SCENE_BY_PLAYER_NUMBER = new int[] { 0, 0, 4, 3, 2 };

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this);
        }

        //testRule();
    }
    private void Start() {
        //lastCard = Card.getNewCard(VALUE.FIVE, COLOR.YELLOW);
        //localPlayer.GetComponent<PlayerManager>().showMyTurnSign(true);
        //AudioManager.instance.Play("bgm");
        //DontDestroyOnLoad(gameObject);
        //ClientSend.restfulapi_GetServerInfos();

    }


    public void loadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void EnterRoom(string ip, int port, int maxPlayer) {
        ServerInfoKeeper.instance.ip = ip;
        ServerInfoKeeper.instance.port = port;
        ServerInfoKeeper.instance.maxPlayer = maxPlayer;
        loadScene(SCENE_BY_PLAYER_NUMBER[maxPlayer]);
    }
}
