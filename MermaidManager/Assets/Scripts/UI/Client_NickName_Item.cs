using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client_NickName_Item : MonoBehaviour
{
    public Button Btn;
    public Text NickName;
    Data_Client client;

    private void Start() 
    {
        Btn.onClick.AddListener(OnClickBtn);
    }

    public void SetItem(Data_Client data)
    {
        client = data;
        NickName.text = data.NickName;
    }

    void OnClickBtn()
    {
        var popup = PopupController.Instance.GetPopup("Popup_Start");
        popup.GetComponent<UIPopup_Start>().SetClient(client);
        PopupController.Instance.ClosePopup("Popup_Search_Client");
    }
}
