using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_AddItem : MonoBehaviour
{
    public GameObject Obj;
    public Transform Contents;
    public Button Exit;
    public Button AddBtn;
    public Button SaveBtn;

    private void Awake()
    {
        Exit.onClick.AddListener(OnClickExit);
        AddBtn.onClick.AddListener(OnClickAddBtn);
        SaveBtn.onClick.AddListener(OnClickSaveBtn);
    }

    void OnClickExit()
    {
        PopupController.Instance.ClosePopup("Popup_AddItem");
    }

    void OnClickAddBtn()
    {
        var newItem = Instantiate(Obj, Contents);
    }

    void OnClickSaveBtn()
    {

    }
}
