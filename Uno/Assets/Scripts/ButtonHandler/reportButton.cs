using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reportButton : MonoBehaviour
{
    public void report() {
        GameManager_game.instance.reportSuccess();
        gameObject.SetActive(false);
    }
}
