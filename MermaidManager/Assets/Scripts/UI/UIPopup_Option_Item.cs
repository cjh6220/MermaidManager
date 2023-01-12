using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Option_Item : MonoBehaviour
{
    public Text Name;
    public Text OneBox_Total_Count;
    public Text Person_Per_Count;
    public Text Person_Per_Price;
    public Button ItemClickBtn;
    public Button ItemRemoveBtn;
    int itemID;

    private void Start()
    {
        ItemClickBtn.onClick.AddListener(OnClickItemBtn);
        ItemRemoveBtn.onClick.AddListener(OnClickRemoveBtn);
    }

    void ScrollCellIndex(int idx)
    {
        string name = "Cell " + idx.ToString();
        gameObject.name = name;
        SetItem(DBManager.Instance.GetProductByOrder(idx));
    }

    void SetItem(Product item)
    {
        if (item != null)
        {
            itemID = item.Id;
            Name.text = item.Name;
            OneBox_Total_Count.text = item.Products[0].OneBox_Total_Count.ToString();
            Person_Per_Count.text = item.Products[0].Person_Per_Count.ToString();
            Person_Per_Price.text = item.Products[0].Person_Per_Price.ToString();
        }
        else
        {
            PopupController.Instance.GetPopup("Popup_Option").GetComponent<UIPopup_Option>().ResetItems();
        }
    }

    void OnClickItemBtn()
    {
        var popup = PopupController.Instance.AddPopup("Popup_EditItem");
        popup.GetComponent<UIPopup_EditItem>().SetItem(itemID);
    }

    void OnClickRemoveBtn()
    {
        var popup = PopupController.Instance.AddPopup("Popup_Remove_Product");
        popup.GetComponent<Popup_Remove_Product>().SetPopup(itemID);
    }
}
