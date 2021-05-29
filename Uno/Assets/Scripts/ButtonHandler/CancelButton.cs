using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public void cancel() {
        AudioManager.instance.Play("skip");
        GameManager.instance.ItsMyTurn(false);
        ClientSend.CannotContinnue();
        gameObject.SetActive(false);
    }
}
