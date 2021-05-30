using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer {
    class ServerHandle {
        public static void WelcomeReceived(int _fromClientId, Packet _packet) {
            int _clientIdCheck = _packet.ReadInt();
            string _userName = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClientId].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClientId}.");
            if(_fromClientId != _clientIdCheck) {
                Console.WriteLine($"Player \"{_userName}\" (ID: {_fromClientId}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            //send player into the game
            Server.clients[_fromClientId].SendIntoGame(_userName);
            ServerSend.restfulapi_UpdateInfo();

            int count = 0;
            foreach(Client _client in Server.clients.Values) {
                if (_client.player != null) count++;
            }
            if(count == Server.MaxPlayers) {
                //为简单起见，准备好直接开始游戏
                GameLogic.startGame();
            }
        }

        public static void playerPlayCard(int _fromClientId, Packet _packet) {

            


            // int _playerId = _packet.ReadInt();
            Card _card = _packet.ReadCard();
            bool ShoutUno = _packet.ReadBool();
            int leftCount = _packet.ReadInt();
            Console.WriteLine($"player {_fromClientId} played a {_card.color} card valued {_card.value}");
            if(ShoutUno) Console.WriteLine($"player {_fromClientId} shoutedUno.");
            Console.WriteLine($"player {_fromClientId} has {leftCount} card(s) left.");

            

            GameLogic.discordCard(_card);

            GameLogic.printDeckAndDeadWood();

            GameLogic.lastButOneCard = GameLogic.lastCard;
            GameLogic.lastCard = _card;
            GameLogic.lastPlayerId = _fromClientId;

            foreach (Client _client in Server.clients.Values) {
                if (_client.player != null) {
                    ServerSend.ReportSuccess(_client.id);
                }
            }
            Server.clients[_fromClientId].PlayCard (_card, ShoutUno, leftCount);


            if(_card.value == VALUE.REVERSE) {
                GameLogic.reverseDirection();
                Console.WriteLine($"direction changed to {GameLogic.curDir}.");
            }

            if (_card.value == VALUE.SKIP) {
                Console.WriteLine($"player{GameLogic.getNextIdInDir(GameLogic.currentPlayerId, GameLogic.curDir)} has to skip.");
                GameLogic.nextRound(false, true);
                return;
            }

            if(_card.value == VALUE.CHANGE) {
                GameLogic.lastCard.color = _card.color;
            }

            if(_card.value == VALUE.PLUS2) {
                Console.WriteLine($"player{GameLogic.getNextIdInDir(GameLogic.currentPlayerId, GameLogic.curDir)} has to draw 2 cards and skip.");
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.getNextIdInDir(GameLogic.currentPlayerId, GameLogic.curDir));
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.getNextIdInDir(GameLogic.currentPlayerId, GameLogic.curDir));
                GameLogic.nextRound(false, true);
                return;
            }

            if (_card.value == VALUE.PLUS4) {
                //可以质疑
                GameLogic.nextRound(true,false);
                return;
            }

            GameLogic.nextRound(false, false);
        }

        public static void cannotContinue(int _fromClientId, Packet _packet) {
           // int tmp = _packet.ReadInt();
            Console.WriteLine($"player {_fromClientId} cannot continue, preparing to draw a card from the deck.");
            Thread.Sleep(50);
            GameLogic.DealCardToClient(_fromClientId);

            GameLogic.nextRound(false, false);
        }

        public static void QuestionCancel(int _fromClientId, Packet _packet) {
            //质疑取消，该玩家摸4张，跳过
            Thread.Sleep(50);
            GameLogic.DealCardToClient(GameLogic.currentPlayerId);
            Thread.Sleep(50);
            GameLogic.DealCardToClient(GameLogic.currentPlayerId);
            Thread.Sleep(50);
            GameLogic.DealCardToClient(GameLogic.currentPlayerId);
            Thread.Sleep(50);
            GameLogic.DealCardToClient(GameLogic.currentPlayerId);
            GameLogic.nextRound(false, false);
        }

        public static void QuestionStands(int _fromClientId, Packet _packet) {
            bool success = _packet.ReadBool();

            //质疑成功，上家摸4张，该玩家不跳过
            if (success) {
                Console.WriteLine($"player {_fromClientId}'s question stands. last player draws 4 cards.");
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.lastPlayerId);
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.lastPlayerId); 
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.lastPlayerId);
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.lastPlayerId);

                //Server.clients[GameLogic.currentPlayerId].ItsYourTurn(false, false);
            }
            //失败，该玩家摸6张 跳过
            else {
                Console.WriteLine($"player {_fromClientId}'s question doesn't stand. the player draws 6 cards.");
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.currentPlayerId);
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.currentPlayerId);
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.currentPlayerId);
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.currentPlayerId);
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.currentPlayerId);
                Thread.Sleep(50);
                GameLogic.DealCardToClient(GameLogic.currentPlayerId);
                GameLogic.nextRound(false, false);
            }
        }

        public static void IWin(int _fromClientId, Packet _packet) {
            GameLogic.WinnerAppear(_fromClientId);
        }

        public static void Skipped(int _fromClientId, Packet _packet) {
            GameLogic.Skipped(_fromClientId);
        }


        public static void ReportSuccess(int _fromClientId, Packet _packet) {
            GameLogic.ReportSuccess(_fromClientId);
        }
    }
}
