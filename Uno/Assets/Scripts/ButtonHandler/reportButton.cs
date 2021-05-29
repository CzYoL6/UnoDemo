using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reportButton : MonoBehaviour
{
    public void report() {
        GameManager.instance.reportSuccess();
        gameObject.SetActive(false);
    }
}
