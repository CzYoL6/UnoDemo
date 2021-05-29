using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasToSkipButton : MonoBehaviour
{
    public void hasToSkip() {
        AudioManager.instance.Play("skip");
        GameManager.instance.ItsMyTurn(false);

        ClientSend.Skipped();
        
        gameObject.SetActive(false);
    }
}
