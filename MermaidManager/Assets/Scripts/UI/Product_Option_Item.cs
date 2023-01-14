using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product_Option_Item : MonoBehaviour
{
    public Text ProductName;
    public Button Btn;
    int OptionID;

    private void Start() 
    {
        Btn.onClick.AddListener(OnClickOption);
    }

    void ScrollCellIndex(int idx)
    {
        string name = "Cell " + idx.ToString();
        gameObject.name = name;
        SetItem(idx);
    }

    void SetItem(int idx)
    {
        var target = DBManager.Instance.SelectedProduct.Products[idx];

        OptionID = target.Option_Id;
        ProductName.text = target.Option_Name;
    }

    void OnClickOption()
    {
        var popup = PopupController.Instance.GetPopup("Popup_Start");
        popup.GetComponent<UIPopup_Start>().SetSelectOption(OptionID);
    }
}
