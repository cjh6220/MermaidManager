using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton_Option : MonoBehaviour
{
    public Button Btn;

    private void Awake()
    {
        Btn.onClick.AddListener(OnClick_Option);
    }

    void OnClick_Option()
    {
        PopupController.Instance.AddPopup("Popup_Option");
    }
}
