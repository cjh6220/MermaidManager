using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remove_Client : MonoBehaviour
{
    public Button OK;
    public Button No;
    Data_Client client;

    private void Start() 
    {
        OK.onClick.AddListener(OnClickOK);    
        No.onClick.AddListener(OnClickNo);
    }

    public void SetPopup(Data_Client data)
    {
        client = data;
    }

    void OnClickOK()
    {
        DBManager.Instance.RemoveClient(client);
        var popup = PopupController.Instance.GetPopup("Popup_Search_Client");
        popup.GetComponent<Client_NickName>().SetClientList(client.Client_Name);
        OnClickNo();
    }

    void OnClickNo()
    {
        PopupController.Instance.ClosePopup("Remove_Client");
    }
}
