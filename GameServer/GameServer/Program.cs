using System;
using System.Threading;

namespace GameServer {
    class Program {
        public static bool isRunning = false;
        static void Main(string[] args) {
            Console.Title = "Game Server"; 
            
            isRunning = true;
            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Server.Start(int.Parse( args[0]),int.Parse( args[1]), args[2], int.Parse(args[3]), args[4]);

            while (true) {
                string command = Console.ReadLine();
                if(command == "quit") {
                    ServerSend.restfulapi_UnregisterInfo();
                    Environment.Exit(0);
                }
            }
        }

        private static void MainThread() {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning) {
                while(_nextLoop < DateTime.Now) {
                    GameLogic.Update();
                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if(_nextLoop > DateTime.Now) {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
