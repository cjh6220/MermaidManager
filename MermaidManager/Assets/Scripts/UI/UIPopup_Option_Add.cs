using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Option_Add : MonoBehaviour
{
    public Button AddBtn;

    private void Awake()
    {
        AddBtn.onClick.AddListener(OnClickAddBtn);
    }

    void OnClickAddBtn()
    {
        var popup = PopupController.Instance.AddPopup("Popup_AddItem");
    }
}
