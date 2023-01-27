using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client_NickName : MonoBehaviour
{
    public Text ClientName;
    public InputField ClientNickName;
    public GameObject Obj;
    public Transform Content;
    public Button AddBtn;
    public Button ExitBtn;

    private void Start()
    {
        AddBtn.onClick.AddListener(OnClickAddNickName);
        ExitBtn.onClick.AddListener(OnClickExit);
    }

    public void SetClient(string clientName)
    {
        ClientName.text = clientName;
        ClientNickName.text = "닉네임 입력";
        SetClientList(clientName);
    }

    public void SetClientList(string name)
    {
        ClearAllItem();
        var targets = DBManager.Instance.GetClientBtName(name);
        for (int i = 0; i < targets.Count; i++)
        {
            var item = Instantiate(Obj, Content);
            item.GetComponent<Client_NickName_Item>().SetItem(targets[i]);
        }
    }

    void ClearAllItem()
    {
        while (Content.childCount > 0)
        {
            DestroyImmediate(Content.GetChild(0).gameObject);
        }
    }

    void OnClickAddNickName()
    {
        if(ClientNickName.text == "닉네임 입력") return;

        var newClient = new Data_Client();
        newClient.Client_Name = ClientName.text;
        newClient.NickName = ClientNickName.text;

        DBManager.Instance.AddNewClient(newClient);

        SetClientList(ClientName.text);
    }

    void OnClickExit()
    {
        PopupController.Instance.ClosePopup("Popup_Search_Client");
    }
}
