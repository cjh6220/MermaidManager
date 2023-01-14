using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product_List_Item : MonoBehaviour
{
    public Text ProductName;
    public Button Btn;
    int itemID;

    private void Start()
    {
        Btn.onClick.AddListener(OnClickProduct);
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
            ProductName.text = item.Name;
        }
    }

    void OnClickProduct()
    {
        var popup = PopupController.Instance.GetPopup("Popup_Start");
        popup.GetComponent<UIPopup_Start>().SetOption(itemID);
    }
}
