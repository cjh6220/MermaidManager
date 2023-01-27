using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calender_Button : MonoBehaviour
{
    public Button CalenderBtn;

    private void Start()
    {
        CalenderBtn.onClick.AddListener(OnClickCalender);
    }

    void OnClickCalender()
    {
        PopupController.Instance.AddPopup("Calender_Popup");
    }
}
