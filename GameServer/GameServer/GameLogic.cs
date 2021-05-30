using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer {
    public enum GAMESTATE {
        WAITING_FOR_PLAYERS = 1,
        DEALING
    }
    public enum DIRECTION {
        ANTICLOCKWISE = 1,
        CLOCKWISE
    }

    class GameLogic {
        public GAMESTATE gameState = GAMESTATE.WAITING_FOR_PLAYERS;

        public static DIRECTION curDir;

        public static List<Card> deck, deadWood;

        public static Card lastCard, lastButOneCard;
        public static int lastPlayerId;

        public static int currentPlayerId;

        public static void Update() {
            ThreadManager.UpdateMain();
        }

        public static void printDeckAndDeadWood() {
            /*Console.WriteLine("deck:");
            foreach(var x in deck) {
                Console.Write($"{x.color} {x.value} , ");
            }
            Console.WriteLine();
            Console.WriteLine("deadwood:");
            foreach (var x in deadWood) {
                Console.Write($"{x.color} {x.value} , ");
            }
            Console.WriteLine();*/
        }

        public static DIRECTION antiDirection(DIRECTION _dir) {
            if (_dir == DIRECTION.ANTICLOCKWISE) return DIRECTION.CLOCKWISE;
            else return DIRECTION.ANTICLOCKWISE;
        }

        public static void reverseDirection() {
            curDir = antiDirection(curDir);
            foreach (var c in Server.clients.Values) {
                if(c.player != null) {
                    ServerSend.ChangeDir(c.id, antiDirection(curDir));
                }
            }
            
            
        }

        public static void startGame() {
            deck = new List<Card>();
            deadWood = new List<Card>();

            deck.Add(Card.getCard(VALUE.ZERO, COLOR.RED));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.YELLOW));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.BLUE));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.GREEN));
            for (int i = (int)VALUE.ONE; i <= (int)VALUE.SKIP; i++) {
                for (int j = (int)COLOR.RED; j <= (int)COLOR.GREEN; j++) {
                    deck.Add(Card.getCard((VALUE)i, (COLOR)j));
                    deck.Add(Card.getCard((VALUE)i, (COLOR)j));
                }
            }
            deck.Add(Constants.changeAny);
            deck.Add(Constants.changeAny);
            deck.Add(Constants.changeAny);
            deck.Add(Constants.changeAny);
            deck.Add(Constants.plus4Any);
            deck.Add(Constants.plus4Any);
            deck.Add(Constants.plus4Any);
            deck.Add(Constants.plus4Any);

            /*deck.Add(Card.getCard(VALUE.ZERO, COLOR.RED));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.YELLOW));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.BLUE));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.GREEN));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.RED));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.YELLOW));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.BLUE));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.GREEN));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.RED));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.YELLOW));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.BLUE));
            deck.Add(Card.getCard(VALUE.ZERO, COLOR.GREEN));*/


            Random r = new Random();
            currentPlayerId = r.Next(Server.MaxPlayers) + 1;
           // currentPlayerId = 4;


            curDir = DIRECTION.CLOCKWISE;
            foreach (var c in Server.clients.Values) {
                if (c.player != null) {
                    ServerSend.ChangeDir(c.id, curDir);
                }
            }

            DealCard(currentPlayerId);

            
            //Server.clients[1].DealACardToTheClient(Card.getCard(VALUE.PLUS4,COLOR.ANY));


            //找到第一张数字牌
            lastCard = drawCardFromDeckIgnoreEmptiness();
            while((int)lastCard.value > 9) {
                discordCard(lastCard);
                lastCard = drawCardFromDeckIgnoreEmptiness();
            }
            lastPlayerId = currentPlayerId;
            lastButOneCard = null;

            foreach (var _client in Server.clients.Values) {
                if (_client != null) {
                    ServerSend.PlayCard(_client.id, 0, lastCard,false, 0);
                }
            }
            discordCard(lastCard);
            Thread.Sleep(2000);
            Server.clients[currentPlayerId].ItsYourTurn(false, false);

            printDeckAndDeadWood();
        }

        public static Card drawCardFromDeckIgnoreEmptiness() {
            Card card = deck.First();
            deck.Remove(card);
            return card;
        }

        public static void discordCard(Card _card) {
            deadWood.Add(_card);
        }

        public static void DealCard(int _firstPlayerId) {
            
            shuffleCards(deck);

            //依次发牌, 7张

            for(int i = 1; i <= 7; i++) {
                int _secondId = getNextIdInDir(_firstPlayerId, DIRECTION.CLOCKWISE);
                int _thirdId = getNextIdInDir(_secondId, DIRECTION.CLOCKWISE);
                int _fourthId = getNextIdInDir(_thirdId, DIRECTION.CLOCKWISE);

                DealCardToClient(_firstPlayerId);
                Thread.Sleep(50);
                if (Server.MaxPlayers >= 2) {
                    DealCardToClient(_secondId);
                    Thread.Sleep(50);
                }
                if (Server.MaxPlayers >= 3) {
                    DealCardToClient(_thirdId);
                    Thread.Sleep(50);
                }
                if (Server.MaxPlayers >= 4) {
                    DealCardToClient(_fourthId);
                    Thread.Sleep(50);
                }
            }
        }

        public static int getNextIdInDir(int _now, DIRECTION _dir) {
            if(_dir == DIRECTION.CLOCKWISE) {
                _now--;
                if (_now <= 0) _now += Server.MaxPlayers;
                return _now;
            }
            else {
                return (_now % Server.MaxPlayers) + 1;
            }
        }

        public static void shuffleCards(List<Card> _cards) {
            //洗牌
            for (int i = 0; i < _cards.Count; i++) {
                Random r = new Random();
                int index = r.Next(_cards.Count);
                var temp = _cards[i];
                _cards[i] = _cards[index];
                _cards[index] = temp;
            }
        }

        public static bool DealCardToClient(int _clientId) {
            
            if (deck.Count == 0) {
                //TODO
                //弃牌堆洗牌
                deck = new List<Card>(deadWood);
                shuffleCards(deck);
                deadWood = new List<Card>();
                //return false;
            }
            Card _first = deck.First();
            
            Server.clients[_clientId].DealACardToTheClient(_first);
            deck.Remove(_first);

            printDeckAndDeadWood();
            return true;
        }

        internal static void Skipped(int fromClientId) {
            nextRound(false, false);
        }

        public static void nextRound(bool ableToQuestion, bool hasToSkip) {
            currentPlayerId = getNextIdInDir(currentPlayerId, curDir);
            
            Server.clients[currentPlayerId].ItsYourTurn(ableToQuestion, hasToSkip);
        }

        public static void WinnerAppear(int winId) {
            Console.WriteLine($"player {winId} winned. restarting the game.");
            Server.clients[winId].Win();
            
        }


        public static void nextRoundSkip(bool ableToQuestion, bool hasToSkip) {
            currentPlayerId = getNextIdInDir( getNextIdInDir(currentPlayerId, curDir), curDir);

            Server.clients[currentPlayerId].ItsYourTurn(ableToQuestion, hasToSkip);
        }

        public static void ReportSuccess(int exceptId) {
            Server.clients[exceptId].ReportSuccess();
            Thread.Sleep(50);
            DealCardToClient(lastPlayerId);
            Thread.Sleep(50);
            DealCardToClient(lastPlayerId);
        }
    }
}
