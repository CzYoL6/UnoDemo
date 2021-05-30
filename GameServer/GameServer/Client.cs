using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace GameServer {
    class Client {
        public static int dataBufferSize = 4096 * 2;

        public int id;
        public TCP tcp;

        public Player player;

        public Client(int _clientId) {
            id = _clientId;
            tcp = new TCP(id);
        }

        public class TCP {

            public TcpClient socket;

            private readonly int id;

            private NetworkStream stream;

            private Packet receivedData;

            private byte[] receiveBuffer;

            public TCP(int _id) {
                id = _id;
            }


            public void Connect(TcpClient _socket) {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receivedData = new Packet();

                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

                

                ServerSend.Welcome(id, $"Welcome to the server! player {id}.");
            }

            private void ReceiveCallback(IAsyncResult _result) {
                try {
                    int _byteLength = stream.EndRead(_result);
                    if(_byteLength <= 0) {
                        //disconnect
                        //Server.clients[id].Disconnect();
                        Console.WriteLine($"client {id} error.");
                        foreach (var x in Server.clients.Values) {
                            if (x.player != null) x.Disconnect();
                        }
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    receivedData.Reset(HandleData(_data));

                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch (Exception _ex) {
                    Console.WriteLine($"client {id} error.");
                    Console.WriteLine($"Error receiving TCP data: {_ex}");
                    //disconnect
                    //Server.clients[id].Disconnect();
                    foreach(var x in Server.clients.Values) {
                        if (x.player != null) x.Disconnect();
                    }
                }
            }

            private bool HandleData(byte[] _data) {
                int _packetLength = 0;
                receivedData.SetBytes(_data);

                if (receivedData.UnreadLength() >= 4) {
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0) {
                        return true;
                    }
                }

                while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength()) {
                    byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                    using (Packet _packet = new Packet(_packetBytes)) {
                        int _packetId = _packet.ReadInt();
                        Server.packetHandlers[_packetId](id,_packet);
                    }

                    _packetLength = 0;
                    if (receivedData.UnreadLength() >= 4) {
                        _packetLength = receivedData.ReadInt();
                        if (_packetLength <= 0) {
                            return true;
                        }
                    }

                }

                if (_packetLength <= 1) {
                    return true;
                }

                return false;
            }

            public void SendData(Packet _packet) {
                try {
                    if (socket != null) {
                        stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                    }
                }
                catch (Exception _ex) {

                    Console.WriteLine($"Error sending data to player {id} via TCP: {_ex}");
                }
            }

            public void Disconnect() {
                socket.Close();
                receiveBuffer = null;
                receivedData = null;
                stream = null;
                socket = null;
            }
        }

        public void Disconnect() {
            int y = 1;
            foreach(var x in Server.clients.Values) {
                if (x != this) y++;
                else {
                    break;
                }
            }
            Console.WriteLine($"client{y} - {tcp.socket.Client.RemoteEndPoint} has disconnected.");

            player = null;
            tcp.Disconnect();

            ServerSend.restfulapi_UpdateInfo();
            
        }


        public void SendIntoGame(string _username) {
            player = new Player(id, _username);
            ServerSend.SpawnPlayer(id, player);

            //spawn other players in the new one
            foreach(Client _client in Server.clients.Values) {
                if(_client.player != null && _client.id != id) {
                    ServerSend.SpawnPlayer(id, _client.player);
                }
            }

            //spawn the new player in other players
            foreach (Client _client in Server.clients.Values) {
                if (_client.player != null && _client.id != id) {
                    ServerSend.SpawnPlayer(_client.id, player);
                }
            }
        }

        public void DealACardToTheClient(Card _card) {
            //得牌的人知道是什么牌
            ServerSend.DealCardKnown(id, id, _card);

            //其他人不知道是什么牌
            foreach (Client _client in Server.clients.Values) {
                if (_client.player != null && id != _client.id) {
                    ServerSend.DealCardUnknown(_client.id, id, _card);
                }
            }
        }

        public void Win() {
            foreach (Client _client in Server.clients.Values) {
                if (_client.player != null) {
                    ServerSend.Win(_client.id, id);
                }
            }
        }

        public void PlayCard(Card _card, bool shoutUno, int leftCount) {
            foreach (Client _client in Server.clients.Values) {
                if (_client.player != null && id != _client.id) {
                    ServerSend.PlayCard(_client.id, id, _card, shoutUno, leftCount);
                }
            }
        }

        public void ItsYourTurn(bool ableToQuestion, bool hasToSkip) {
            foreach (Client _client in Server.clients.Values) {
                if (_client.player != null) {
                    ServerSend.WhosTurn(_client.id, id);
                }
            }
            ServerSend.ItsYourTurn(id, ableToQuestion, hasToSkip);
            Console.WriteLine($"now it's player {id}'s turn.");
        }


        public void ReportSuccess() {
            foreach (Client _client in Server.clients.Values) {
                if (_client.player != null && id != _client.id) {
                    ServerSend.ReportSuccess(_client.id);
                }
            }
            Console.WriteLine($"player {id} report succeed.");
        }
    }
}
