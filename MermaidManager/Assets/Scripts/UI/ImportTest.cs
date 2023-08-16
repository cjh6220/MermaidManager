using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class ImportTest : MonoBehaviour
{
    public TextAsset ProductDB;
    public TextAsset ClientDB;
    public List<TextAsset> ClientHistoryList;

    public void LoadNSave()
    {
#if UNITY_EDITOR
        File.WriteAllText(Application.dataPath + "/ProductDB.json", ProductDB.ToString());

        File.WriteAllText(Application.dataPath + "/ClientDB.json", ClientDB.ToString());

        if (!File.Exists(Application.dataPath + "/ClientData"))
        {
            Directory.CreateDirectory(Application.dataPath + "/ClientData");
        }

        for (int i = 0; i < ClientHistoryList.Count; i++)
        {
            File.WriteAllText(Application.dataPath + "/ClientData/" + ClientHistoryList[i].name + ".json", ClientHistoryList[i].ToString());
        }
        PlayerPrefs.SetInt("LastProductIdx", 162);
#else
        File.WriteAllText(Application.persistentDataPath + "/ProductDB.json", ProductDB.ToString());

        File.WriteAllText(Application.persistentDataPath + "/ClientDB.json", ClientDB.ToString());

        if (!File.Exists(Application.persistentDataPath + "/ClientData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/ClientData");
        }

        for (int i = 0; i < ClientHistoryList.Count; i++)
        {
            File.WriteAllText(Application.persistentDataPath + "/ClientData/" + ClientHistoryList[i].name+".json", ClientHistoryList[i].ToString());
        }
        PlayerPrefs.SetInt("LastProductIdx", 162);
#endif
    }
}
