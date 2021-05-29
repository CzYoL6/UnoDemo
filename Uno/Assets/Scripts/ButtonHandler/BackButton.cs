using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void back() {
        Client.instance.Disconnect();
        GameManager.instance.Restart();
    }
}
