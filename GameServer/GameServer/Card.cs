using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer {

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

    public class Card {
        public VALUE value;
        public COLOR color;

        public static Card getCard(VALUE _value, COLOR _color) {
            Card _newCard;
            if (_value == VALUE.CHANGE && _color == COLOR.ANY) _newCard = Constants.changeAny;
            else if (_value == VALUE.PLUS4 && _color == COLOR.ANY) _newCard = Constants.plus4Any;
            else _newCard = Constants.coloredCards[(int)_value * 4 + (int)_color];
            return _newCard;
        }
    }
}
