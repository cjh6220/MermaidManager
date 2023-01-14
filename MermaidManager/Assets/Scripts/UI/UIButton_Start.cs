using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton_Start : MonoBehaviour
{
    public Button StartButton;
    void Start()
    {
        StartButton.onClick.AddListener(OnClickStartBtn);
    }

    void OnClickStartBtn()
    {
        PopupController.Instance.AddPopup("Popup_Start");
    }
}
