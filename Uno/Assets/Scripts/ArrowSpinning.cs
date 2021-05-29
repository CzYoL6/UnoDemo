using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpinning : MonoBehaviour
{

    public float spinningSpeed = 5f;
    public int spinningDir = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().eulerAngles = new Vector3(GetComponent<RectTransform>().eulerAngles.x,
            GetComponent<RectTransform>().eulerAngles.y,
            GetComponent<RectTransform>().eulerAngles.z + spinningSpeed * spinningDir * Time.deltaTime);
    }

    public void changeDir() {
        spinningDir *= -1;
        GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x * -1,
            GetComponent<RectTransform>().localScale.y,
            GetComponent<RectTransform>().localScale.z);
    }

}
