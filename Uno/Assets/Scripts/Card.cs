using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum COLOR {
    RED = 1,
    YELLOW,
    BLUE,
    GREEN,
    ANY
}

public enum VALUE {
    ZERO = 0,
    ONE,
    TWO,
    THREE,
    FOUR,
    FIVE,
    SIX,
    SEVEN,
    EIGHT,
    NINE,
    PLUS2,
    REVERSE,
    SKIP,

    CHANGE,
    PLUS4
}


[CreateAssetMenu(menuName ="Uno")]
public class Card : ScriptableObject{
    public VALUE value;
    public COLOR color;
    public Sprite image;

    public override bool Equals(object obj) {
        return obj is Card card &&
               value == card.value &&
               color == card.color;
    }

    public static Card getNewCard(VALUE _value, COLOR _color) {
        Card _newCard;
        if (_value == VALUE.CHANGE && _color == COLOR.ANY) _newCard = Cards.instance.changeAny;
        else if (_value == VALUE.PLUS4 && _color == COLOR.ANY) _newCard = Cards.instance.plus4Any;
        else _newCard = Cards.instance.cardInfo[(int)_value * 4 + (int)_color];
        return _newCard;
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    public override string ToString() {
        return base.ToString();
    }
}