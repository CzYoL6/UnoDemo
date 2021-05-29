using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer {
    class Constants {

        public const int TICKS_PER_SEC = 30;
        public const int MS_PER_TICK = 1000 / TICKS_PER_SEC;

        public static Card[] coloredCards = {
            null,
            new Card(){value = VALUE.ZERO, color = COLOR.RED},
            new Card(){value = VALUE.ZERO, color = COLOR.YELLOW},
            new Card(){value = VALUE.ZERO, color = COLOR.BLUE},
            new Card(){value = VALUE.ZERO, color = COLOR.GREEN},

            new Card(){value = VALUE.ONE, color = COLOR.RED},
            new Card(){value = VALUE.ONE, color = COLOR.YELLOW},
            new Card(){value = VALUE.ONE, color = COLOR.BLUE},
            new Card(){value = VALUE.ONE, color = COLOR.GREEN},

            new Card(){value = VALUE.TWO, color = COLOR.RED},
            new Card(){value = VALUE.TWO, color = COLOR.YELLOW},
            new Card(){value = VALUE.TWO, color = COLOR.BLUE},
            new Card(){value = VALUE.TWO, color = COLOR.GREEN},

            new Card(){value = VALUE.THREE, color = COLOR.RED},
            new Card(){value = VALUE.THREE, color = COLOR.YELLOW},
            new Card(){value = VALUE.THREE, color = COLOR.BLUE},
            new Card(){value = VALUE.THREE, color = COLOR.GREEN},

            new Card(){value = VALUE.FOUR, color = COLOR.RED},
            new Card(){value = VALUE.FOUR, color = COLOR.YELLOW},
            new Card(){value = VALUE.FOUR, color = COLOR.BLUE},
            new Card(){value = VALUE.FOUR, color = COLOR.GREEN},

            new Card(){value = VALUE.FIVE, color = COLOR.RED},
            new Card(){value = VALUE.FIVE, color = COLOR.YELLOW},
            new Card(){value = VALUE.FIVE, color = COLOR.BLUE},
            new Card(){value = VALUE.FIVE, color = COLOR.GREEN},

            new Card(){value = VALUE.SIX, color = COLOR.RED},
            new Card(){value = VALUE.SIX, color = COLOR.YELLOW},
            new Card(){value = VALUE.SIX, color = COLOR.BLUE},
            new Card(){value = VALUE.SIX, color = COLOR.GREEN},

            new Card(){value = VALUE.SEVEN, color = COLOR.RED},
            new Card(){value = VALUE.SEVEN, color = COLOR.YELLOW},
            new Card(){value = VALUE.SEVEN, color = COLOR.BLUE},
            new Card(){value = VALUE.SEVEN, color = COLOR.GREEN},

            new Card(){value = VALUE.EIGHT, color = COLOR.RED},
            new Card(){value = VALUE.EIGHT, color = COLOR.YELLOW},
            new Card(){value = VALUE.EIGHT, color = COLOR.BLUE},
            new Card(){value = VALUE.EIGHT, color = COLOR.GREEN},

            new Card(){value = VALUE.NINE, color = COLOR.RED},
            new Card(){value = VALUE.NINE, color = COLOR.YELLOW},
            new Card(){value = VALUE.NINE, color = COLOR.BLUE},
            new Card(){value = VALUE.NINE, color = COLOR.GREEN},

            new Card(){value = VALUE.PLUS2, color = COLOR.RED},
            new Card(){value = VALUE.PLUS2, color = COLOR.YELLOW},
            new Card(){value = VALUE.PLUS2, color = COLOR.BLUE},
            new Card(){value = VALUE.PLUS2, color = COLOR.GREEN},

            new Card(){value = VALUE.REVERSE, color = COLOR.RED},
            new Card(){value = VALUE.REVERSE, color = COLOR.YELLOW},
            new Card(){value = VALUE.REVERSE, color = COLOR.BLUE},
            new Card(){value = VALUE.REVERSE, color = COLOR.GREEN},

            new Card(){value = VALUE.SKIP, color = COLOR.RED},
            new Card(){value = VALUE.SKIP, color = COLOR.YELLOW},
            new Card(){value = VALUE.SKIP, color = COLOR.BLUE},
            new Card(){value = VALUE.SKIP, color = COLOR.GREEN},

            new Card(){value = VALUE.CHANGE, color = COLOR.RED},
            new Card(){value = VALUE.CHANGE, color = COLOR.YELLOW},
            new Card(){value = VALUE.CHANGE, color = COLOR.BLUE},
            new Card(){value = VALUE.CHANGE, color = COLOR.GREEN},

            new Card(){value = VALUE.PLUS4, color = COLOR.RED},
            new Card(){value = VALUE.PLUS4, color = COLOR.YELLOW},
            new Card(){value = VALUE.PLUS4, color = COLOR.BLUE},
            new Card(){value = VALUE.PLUS4, color = COLOR.GREEN},
        };

        public static Card changeAny = new Card() { value = VALUE.CHANGE, color = COLOR.ANY };
        public static Card plus4Any = new Card() { value = VALUE.PLUS4, color = COLOR.ANY };
    }
}
