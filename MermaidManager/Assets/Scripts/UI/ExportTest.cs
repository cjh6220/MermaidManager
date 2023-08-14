using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;
using Newtonsoft.Json;

public class ExportTest : MonoBehaviour
{
    public void OnClickProductExport()
    {
        string mailto = "cjh6220@gmail.com";

        string subject = EscapeURL("상품 정보 / " + DateTime.Now.ToString("yyyy/MM/dd"));

        string body = EscapeURL
        (
            //GetTodayGift()
#if UNITY_EDITOR
            File.ReadAllText(Application.dataPath + "/ProductDB.json")
#else
            File.ReadAllText(Application.persistentDataPath + "/ProductDB.json")
#endif
        );

        Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);
    }

    public void OnClickClientExport()
    {
        string mailto = "cjh6220@gmail.com";

        string subject = EscapeURL("고객 정보 / " + DateTime.Now.ToString("yyyy/MM/dd"));

        string body = EscapeURL
        (
            //GetTodayGift()
#if UNITY_EDITOR
            File.ReadAllText(Application.dataPath + "/ClientDB.json")
#else
            File.ReadAllText(Application.persistentDataPath + "/ClientDB.json")
#endif
        );

        Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);
    }

    public void OnClickClientHistoryExport()
    {
#if UNITY_EDITOR
        GetHistory();
#endif
        string mailto = "cjh6220@gmail.com";

        string subject = EscapeURL("고객 히스토리 정보 / " + DateTime.Now.ToString("yyyy/MM/dd"));

        string body = EscapeURL
        (
            GetHistory()
        );

        Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);
    }


    string EscapeURL(string url)
    {
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }

    string GetHistory()
    {
        string str = "";
        var historyList = new List<Data_Client_History>();
#if UNITY_EDITOR
        var test = new DirectoryInfo(Application.dataPath + "/ClientData");
        var targets = test.GetFiles("*json");
        for (int i = 0; i < targets.Length; i++)
        {
            var jsonStrRead = File.ReadAllText(Application.dataPath + "/ClientData/" + targets[i].Name);
            var deserializedBarList = JsonConvert.DeserializeObject<Data_Client_History>(jsonStrRead);
            historyList.Add(deserializedBarList);
        }
#else
        var test = new DirectoryInfo(Application.persistentDataPath+"/ClientData");
        var targets = test.GetFiles("*json");
        for (int i = 0; i < targets.Length; i++)
        {
            var jsonStrRead = File.ReadAllText(Application.persistentDataPath + "/ClientData/" + targets[i].Name);
            var deserializedBarList = JsonConvert.DeserializeObject<Data_Client_History>(jsonStrRead);
            historyList.Add(deserializedBarList);
        }
#endif
        str = JsonConvert.SerializeObject(historyList);
        return str;
    }
}