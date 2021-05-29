using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnoSignDisapprear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("disappear");
    }

    private IEnumerator disappear() {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        StartCoroutine("disappear");
    }
}
