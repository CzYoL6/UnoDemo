using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerInfoKeeper : MonoBehaviour
{
    public static ServerInfoKeeper instance;
    public string ip;
    public int port;
    public int maxPlayer;
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
        DontDestroyOnLoad(gameObject);
    }
}
