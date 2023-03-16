using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup_SlotMachine_PickUp : MonoBehaviour
{
    public Button CloseBtn;
    public TextMeshProUGUI Name;

    private void Awake()
    {
        CloseBtn.onClick.AddListener(OnClickCloseBtn);
    }

    public void SetPopup(string name)
    {
        Name.SetText(name);
    }

    void OnClickCloseBtn()
    {
        UIManager.Instance.CloseUiPanelType(e_UIType.Popup_SlotMachine_PickUp);
    }
}
