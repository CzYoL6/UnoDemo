using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_welcome : MonoBehaviour
{
    

    public static GameManager_welcome instance;

    

  
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
        AudioManager.instance.Play("bgm");
        //DontDestroyOnLoad(gameObject);
        //ClientSend.restfulapi_GetServerInfos();

    }

  
    public void loadScene(int index) {
        SceneManager.LoadScene(index);
    }

}
