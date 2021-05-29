using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTurnSignFloating : MonoBehaviour
{

    public float floatFactor;
    public float speedFactor;
    public bool horizontal;
    private void Start() {
        
    }
    void Update()
    {
        if(!horizontal)
            GetComponent<RectTransform>().position = new Vector2(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y + floatFactor * Mathf.Sin(Time.time * speedFactor) * Time.deltaTime);
        else GetComponent<RectTransform>().position = new Vector2(GetComponent<RectTransform>().position.x + floatFactor * Mathf.Sin(Time.time * speedFactor) * Time.deltaTime, GetComponent<RectTransform>().position.y);
    }
}
