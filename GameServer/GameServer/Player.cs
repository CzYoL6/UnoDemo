using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer {
    class Player {
        public int id;
        public string playerName;
        public List<Card> cards;

        public Player(int _id, string _name) {
            id = _id;
            playerName = _name;
            cards = new List<Card>();
        }
    }
}
