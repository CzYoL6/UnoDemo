using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GameServer {
    class Server {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static string MainServerIP { get; private set; } //假设在同一IP不同端口
        public static int MainServerPort { get; private set; }

        public static string ServerName { get; private set; }

        public static Dictionary<int, Client> clients;

        public static TcpListener tcpListener;

        public delegate void PacketHandler(int _fromClientId, Packet _packet);
        public static Dictionary<int, PacketHandler> packetHandlers;

        public static void Start(int _maxPlayer, int _port, string _mainServerIp, int _mainServerPort, string _serverName) {
            clients = new Dictionary<int, Client>();
            MaxPlayers = _maxPlayer;
            Port = _port;
            MainServerIP = _mainServerIp;
            MainServerPort = _mainServerPort;
            ServerName = _serverName;

            Console.WriteLine("Starting server...");

            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Server started on {Port}.");

            ServerSend.restfulapi_RegisterInfo();

        }

        private static void TCPConnectCallback(IAsyncResult _result) {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);


            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");

            for(int i = 1; i <= MaxPlayers; i++) {
                if(clients[i].tcp.socket == null) {
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }

            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect. Server full!");
        }


        public static int getOnlinePlayerCount() {
            int count = 0;
            foreach(var x in Server.clients.Values) {
                if (x.player != null) count++;
            }
            return count;
        }

        private static void InitializeServerData() {
            for(int i = 1; i <= MaxPlayers; i++) {
                clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>() {
                {(int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
                {(int)ClientPackets.PlayCard, ServerHandle.playerPlayCard },
                {(int)ClientPackets.CannotContinue, ServerHandle.cannotContinue},
                {(int)ClientPackets.QuestionCancel, ServerHandle.QuestionCancel },
                {(int)ClientPackets.QuestionStands, ServerHandle.QuestionStands },
                {(int)ClientPackets.IWin, ServerHandle.IWin },
                {(int)ClientPackets.Skipped, ServerHandle.Skipped },
                 {(int)ClientPackets.ReportSuccess, ServerHandle.ReportSuccess }

            };
            Console.WriteLine("Initializing Data..");
        }
    }
}
