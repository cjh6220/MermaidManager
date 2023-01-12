using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Remove_Product : MonoBehaviour
{
    public Button Yes;
    public Button No;
    int Id;

    private void Start() 
    {
        Yes.onClick.AddListener(OnClickYes);
        No.onClick.AddListener(OnClickNo);
    }

    public void SetPopup(int ItemId)
    {
        Id = ItemId;
    }

    void OnClickYes()
    {
        DBManager.Instance.RemoveProductByID(Id);
        var popup = PopupController.Instance.GetPopup("Popup_Option");
        popup.GetComponent<UIPopup_Option>().RefreshCells();
        OnClickNo();
    }

    void OnClickNo()
    {
        PopupController.Instance.ClosePopup("Popup_Remove_Product");
    }
}
