using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFieldSpaceAdjust : MonoBehaviour
{
    public HorizontalLayoutGroup horizontalLayoutGroup;
    public bool horizontal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal) {
            if (transform.childCount >= 14) horizontalLayoutGroup.spacing = -50;
            else horizontalLayoutGroup.spacing = -30;
        }
        else {
            if (transform.childCount >= 8) horizontalLayoutGroup.spacing = -50;
            else horizontalLayoutGroup.spacing = -30;
        }
    }
}
