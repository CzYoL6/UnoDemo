using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    public float timeInterval = 1f;
    private float curTime;
    public Transform roomButtonParent;
    public bool inRoomSelectingPhase ;
    //public Dictionary<string, GameObject> roomButtons;
    public GameObject roomButtonPrefab;
    // Start is called before the first frame update

    public GameObject[] Children;
    void Start()
    {
        curTime = Time.time;
        inRoomSelectingPhase = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(inRoomSelectingPhase && Time.time - curTime >= timeInterval) {
            curTime = Time.time;
            RequestRoomUpdate();
        }
        //Debug.Log("!!!");
    }

    public void Show(bool f) {
        foreach(var x in Children) {
            x.SetActive(f);
        }
        inRoomSelectingPhase = false;
    }

    private void RequestRoomUpdate() {
        var serverInfos = ClientSend.restfulapi_GetServerInfos();

        for (int i = 0; i < roomButtonParent.childCount; i++) {
            Destroy(roomButtonParent.GetChild(i).gameObject);
        }
        

        foreach (var si in serverInfos) {
            //string key = si.ip + "-" + si.port;
            
            GameObject roomButton = Instantiate(roomButtonPrefab, roomButtonParent);
            roomButton.GetComponent<RoomButton>().setInfo(si);
        }

    }
}
