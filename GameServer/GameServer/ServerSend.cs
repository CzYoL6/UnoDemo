using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GameServer {
    class ServerSend {
        #region tcp
        private static void SendTCPData(int _toClient, Packet _packet) {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet) {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                if (i != _exceptClient)
                    Server.clients[i].tcp.SendData(_packet);
            }
        }


        public static void Welcome(int _toClient, string _msg) {
            using (Packet _packet = new Packet((int)ServerPackets.welcome)) {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }


        //add the player into the game
        public static void SpawnPlayer(int _toClient, Player _playerToSpawn) {
            using (Packet _packet = new Packet((int)ServerPackets.SpawnPlayer)) {
                _packet.Write(_playerToSpawn.id);
                _packet.Write(_playerToSpawn.playerName);

                SendTCPData(_toClient, _packet);
            }
        }

       

        public static void PlayCard(int _toClient, int _playerId, Card _card, bool shoutUno, int leftCount) {
            using (Packet _packet = new Packet((int)ServerPackets.OtherPlayCard)) {
                _packet.Write(_playerId);
                _packet.Write(_card);
                _packet.Write(shoutUno);
                _packet.Write(leftCount);

                SendTCPData(_toClient, _packet);
            }
        }


        //broadcast dealing cards
        public static void DealCardKnown(int _toClient, int _playerToDeal, Card _card) {
            using (Packet _packet = new Packet((int)ServerPackets.DealCardKnown)) {
                //_packet.Write(_playerToDeal);  //发给谁，此人可以知道是什么牌
                _packet.Write((int)_card.value);
                _packet.Write((int)_card.color);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void DealCardUnknown(int _toClient, int _playerToDeal, Card _card) {
            using (Packet _packet = new Packet((int)ServerPackets.DealCardUnknown)) {
                _packet.Write(_playerToDeal);  //发给谁，别人不知道是什么牌
                _packet.Write((int)_card.value);
                _packet.Write((int)_card.color);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void ItsYourTurn(int _toClient, bool ableToQuestion, bool hasToSkip) {
            using (Packet _packet = new Packet((int)ServerPackets.YourTurn)) {
                _packet.Write(ableToQuestion);
                _packet.Write(hasToSkip);
                _packet.Write(GameLogic.lastCard);
                _packet.Write(GameLogic.lastButOneCard == null);
                if (GameLogic.lastButOneCard != null) _packet.Write(GameLogic.lastButOneCard);
                _packet.Write(GameLogic.lastPlayerId);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void Win(int _toClient, int _winnerId) {
            using (Packet _packet = new Packet((int)ServerPackets.Win)) {
                _packet.Write(_winnerId);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void WhosTurn(int _toClient, int _whosTurn) {
            //Console.WriteLine($"tipping player {_toClient} to show his turn sign.");
            using (Packet _packet = new Packet((int)ServerPackets.WhosTurn)) {
                _packet.Write(_whosTurn);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void ChangeDir(int _toClient, DIRECTION _dir) {
            //Console.WriteLine($"tipping player {_toClient} to changeTurn.");
            using (Packet _packet = new Packet((int)ServerPackets.ChangeDir)) {
                _packet.Write((int)_dir);
                SendTCPData(_toClient, _packet);
            }

        }

        public static void ReportSuccess(int _toClient) {
            using (Packet _packet = new Packet((int)ServerPackets.OtherReportSuccess)) {
                SendTCPData(_toClient, _packet);
            }
        }

        public  static void Restart(int _toClient) {
            using (Packet _packet = new Packet((int)ServerPackets.Restart)) {
                SendTCPData(_toClient, _packet);
            }
        }
        #endregion

        #region restfulapi


        private static string jsonStringify(string ip, int port, int maxPlayer, int onlinePlayer, string name) {
            string ret =  "{" +
                "\"ip\":" +
                "\"" +
                ip + 
                "\"" +
                "," +
                "\"port\":" +
                port;
            if(maxPlayer >= 0) {
                ret += ", " +
                    "\"maxPlayer\":" +
                    maxPlayer;

            }

            if (onlinePlayer >= 0) {
                ret += ", " +
                    "\"onlinePlayer\":" +
                    onlinePlayer;

            }

            if (name != "") {
                ret += ", " +
                    "\"name\":" +
                    "\"" +
                    name +
                    "\"";

            }

            ret += "}";
            return ret;

        }

        public static void restfulapi_RegisterInfo() {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Server.MainServerIP + ":" + Server.MainServerPort + "/RegisterGameServerInfo");

            request.Method = "POST";
            request.ContentType = "application/json";

            /* string strContent = JsonSerializer.Serialize(new {
                    ip = Server.MainServerIP,
                    port = Server.Port,
                    maxPlayer = Server.MaxPlayers,
                    name = Server.ServerName
                }) ;*/
            //string strContent = JsonSerializer.Serialize("{\"serverIP\":\"127.0.0.1\", \"serverPORT\":26950,\"maxPlayer\":3}");
            //Console.WriteLine(strContent);



            /*new DataContractJsonSerializer(typeof(object)).WriteObject(request.GetRequestStream(), new {
                ip = Server.MainServerIP,
                port = Server.Port,
                maxPlayer = Server.MaxPlayers,
                name = Server.ServerName
            });*/

            /*StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new JsonTextWriter(stringWriter);
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("ip");
            jsonWriter.WriteValue(Server.MainServerIP);
            jsonWriter.WritePropertyName("port");
            jsonWriter.WriteValue(Server.Port);
            jsonWriter.WritePropertyName("maxPlayer");
            jsonWriter.WriteValue(Server.MaxPlayers);
            jsonWriter.WritePropertyName("name");
            jsonWriter.WriteValue(Server.ServerName);
            jsonWriter.WriteEndObject();
            jsonWriter.Flush();
            string content = stringWriter.GetStringBuilder().ToString();*/

            string content = jsonStringify(Server.MainServerIP, Server.Port, Server.MaxPlayers, -1, Server.ServerName);
            Console.WriteLine(content);

            using (Stream resStream = request.GetRequestStream()) {
                using (StreamWriter dataStream = new StreamWriter(resStream)) {
                    dataStream.Write(content);
                    dataStream.Close();
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1) {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                
                string retString = reader.ReadToEnd();
                reader.Close();
                Console.WriteLine(retString);
            }
        }

        public static void restfulapi_UpdateInfo() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Server.MainServerIP + ":" + Server.MainServerPort + "/UpdateGameServerInfo");
            

            request.Method = "POST";
            request.ContentType = "application/json";
            /*string strContent = JsonSerializer.Serialize(new {
                ip = Server.MainServerIP,
                port = Server.Port,
                onlinePlayer = Server.getOnlinePlayerCount()
            });*/
            //string strContent = JsonSerializer.Serialize("{\"serverIP\":\"127.0.0.1\", \"serverPORT\":26950,\"maxPlayer\":3}");
            /*Console.WriteLine(strContent);
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream())) {
                dataStream.Write(strContent);
                dataStream.Close();
            }*/
            /*new DataContractJsonSerializer(typeof(object)).WriteObject(request.GetRequestStream(), new {
                ip = Server.MainServerIP,
                port = Server.Port,
                onlinePlayer = Server.getOnlinePlayerCount()
            });*/
            /*StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new JsonTextWriter(stringWriter);
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("ip");
            jsonWriter.WriteValue(Server.MainServerIP);
            jsonWriter.WritePropertyName("port");
            jsonWriter.WriteValue(Server.Port);
            jsonWriter.WritePropertyName("onlinePlayer");
            jsonWriter.WriteValue(Server.getOnlinePlayerCount());
            jsonWriter.WriteEndObject();
            jsonWriter.Flush();
            string content = stringWriter.GetStringBuilder().ToString();*/

            string content = jsonStringify(Server.MainServerIP, Server.Port, -1, Server.getOnlinePlayerCount(), Server.ServerName);

            using (Stream resStream = request.GetRequestStream()) {
                using (StreamWriter dataStream = new StreamWriter(resStream)) {
                    dataStream.Write(content);
                    dataStream.Close();
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1) {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                reader.Close();
                Console.WriteLine(retString);
            }
        }

        public static void restfulapi_UnregisterInfo() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + Server.MainServerIP + ":" + Server.MainServerPort +"/UnregisterGameServerInfo");

            request.Method = "POST";
            request.ContentType = "application/json";
            /*string strContent = JsonSerializer.Serialize(new {
                ip = Server.MainServerIP,
                port = Server.Port
            });*/
            //string strContent = JsonSerializer.Serialize("{\"serverIP\":\"127.0.0.1\", \"serverPORT\":26950,\"maxPlayer\":3}");
            /*Console.WriteLine(strContent);
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream())) {
                dataStream.Write(strContent);
                dataStream.Close();
            }*/
            /*new DataContractJsonSerializer(typeof(object)).WriteObject(request.GetRequestStream(), new {
                ip = Server.MainServerIP,
                port = Server.Port
            });*/

            /*StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new JsonTextWriter(stringWriter);
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("ip");
            jsonWriter.WriteValue(Server.MainServerIP);
            jsonWriter.WritePropertyName("port");
            jsonWriter.WriteValue(Server.Port);
            jsonWriter.WriteEndObject();
            jsonWriter.Flush();
            string content = stringWriter.GetStringBuilder().ToString();*/

            string content = jsonStringify(Server.MainServerIP, Server.Port, -1, -1, "");

            using (Stream resStream = request.GetRequestStream()) {
                using (StreamWriter dataStream = new StreamWriter(resStream)) {
                    dataStream.Write(content);
                    dataStream.Close();
                }
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1) {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                reader.Close();
                Console.WriteLine(retString);
            }
        }
        #endregion
    }
}
