using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet) {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived() {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived)) {
            _packet.Write(Client.instance.myId);
            _packet.Write(Client.instance.userName);

            SendTCPData(_packet);
        }
    }

    public static void PlayCard(bool uno, int leftCount) {
        using (Packet _packet = new Packet((int)ClientPackets.PlayCard)) {
            

            Card _card = GameManager_game.instance.localPlayer.GetComponent<PlayerManager>().selectedCard;
            _packet.Write(_card);
            _packet.Write(uno);
            _packet.Write(leftCount);

            SendTCPData(_packet);
        }
    }

    public static void CannotContinnue() {
        using (Packet _packet = new Packet((int)ClientPackets.CannotContinue)) {
           // _packet.Write((int)1);
            SendTCPData(_packet);
        }
    }

    public static void QuestionCancel() {
        using (Packet _packet = new Packet((int)ClientPackets.QuestionCancel)) {
            // _packet.Write((int)1);
            SendTCPData(_packet);
        }
    }

    public static void QuestionStands(bool f) {
        using (Packet _packet = new Packet((int)ClientPackets.QuestionStands)) {
            _packet.Write(f);
            SendTCPData(_packet);
        }
    }

    public static void ReportSuccess() {
        using (Packet _packet = new Packet((int)ClientPackets.ReportSuccess)) {
            SendTCPData(_packet);
        }
    }

    public static void Win() {
        using (Packet _packet = new Packet((int)ClientPackets.IWin)) {
            //_packet.Write(Client.instance.myId);
            SendTCPData(_packet);
        }
    }

    public static void Skipped() {
        using (Packet _packet = new Packet((int)ClientPackets.Skipped)) {
            //_packet.Write(Client.instance.myId);
            SendTCPData(_packet);
        }
    }

    #endregion

    #region restfulapi
    public static List<ServerInfo> restfulapi_GetServerInfos() {

        List<ServerInfo> serverInfos = new List<ServerInfo>();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://120.79.166.136:5000/GetServerInfos");
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/GetServerInfos");

        request.Method = "POST";
        request.ContentType = "application/json";
        string content = "{\"version\":" + Application.version + "}";

        using (Stream resStream = request.GetRequestStream()) {
            using (StreamWriter dataStream = new StreamWriter(resStream)) {
                dataStream.Write(content);
                dataStream.Close();
            }
        }
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1) {
                encoding = "UTF-8";
            }


            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            // Debug.Log(retString);
            JavaScriptObject jo = (JavaScriptObject)JavaScriptConvert.DeserializeObject(retString);
            JavaScriptArray serverArray = (JavaScriptArray)jo["serverInfo"];
            //Debug.Log(serversInString[0]);
            for (int i = 0; i < serverArray.Count; i++) {
                ServerInfo serverInfo = new ServerInfo() {
                    ip = ((JavaScriptObject)serverArray[i])["ip"].ToString(),
                    port = int.Parse(((JavaScriptObject)serverArray[i])["port"].ToString()),
                    maxPlayer = int.Parse(((JavaScriptObject)serverArray[i])["maxPlayer"].ToString()),
                    onlinePlayer = int.Parse(((JavaScriptObject)serverArray[i])["onlinePlayer"].ToString()),
                    name = ((JavaScriptObject)serverArray[i])["name"].ToString()
                };
                serverInfos.Add(serverInfo);
                // Debug.Log(serverInfo.ip+" "+serverInfo.port+" "+serverInfo.maxPlayer+" "+serverInfo.onlinePlayer);

            }
            //string zone_en = jo["zone_en"].ToString();
            //JavaScriptArray a = (JavaScriptArray)JavaScriptConvert.DeserializeObject(serversInString);

            //Debug.Log(a);
        }
        return serverInfos;
    }
    #endregion

}
