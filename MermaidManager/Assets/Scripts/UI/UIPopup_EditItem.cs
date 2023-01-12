using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_EditItem : MonoBehaviour
{
    public GameObject Obj;
    public Transform Contents;
    public Button ExitBtn;
    public InputField ProductName;
    public InputField CountPerBox;
    public InputField CountPerOnce;
    public InputField Price;
    public Button AddBtn;
    public Button SaveBtn;
    int ItemId;

    private void Start()
    {
        ExitBtn.onClick.AddListener(ClosePopup);
        AddBtn.onClick.AddListener(OnClickAddBtn);
        SaveBtn.onClick.AddListener(OnClickSaveBtn);
    }

    public void SetItem(int itemId)
    {
        ClearPopup();
        ItemId = itemId;
        Debug.LogError("UIPopup_EditItem SetItem = " + itemId);
        var itemInfo = DBManager.Instance.GetProductByID(itemId);
        if (itemInfo != null)
        {
            ProductName.text = itemInfo.Name;
            CountPerBox.text = itemInfo.Products[0].OneBox_Total_Count.ToString();
            CountPerOnce.text = itemInfo.Products[0].Person_Per_Count.ToString();
            Price.text = (itemInfo.Products[0].Person_Per_Price * (itemInfo.Products[0].OneBox_Total_Count / itemInfo.Products[0].Person_Per_Count)).ToString();

            for (int i = 0; i < itemInfo.Products.Count; i++)
            {
                var item = Instantiate(Obj, Contents);
                item.GetComponent<UIPopup_AddItem_Item>().SetOption(itemInfo.Products[i]);
            }
        }
    }

    void OnClickAddBtn()
    {
        if (Contents.childCount == 0)
        {
            var newItem = Instantiate(Obj, Contents);
            var sample = new Product_Option();
            sample.Person_Per_Price = int.Parse(Price.text) / (int.Parse(CountPerBox.text) / int.Parse(CountPerOnce.text));
            newItem.GetComponent<UIPopup_AddItem_Item>().SetOption(sample);
        }
        else
        {
            var newItem = Instantiate(Obj, Contents);
            var sample = Contents.GetChild(0).GetComponent<UIPopup_AddItem_Item>();
            newItem.GetComponent<UIPopup_AddItem_Item>().SetOption(sample.GetOptionData());
        }
    }

    void OnClickSaveBtn()
    {
        var Item = new Product();
        Item.Id = ItemId;
        var options = Contents.GetComponentsInChildren<UIPopup_AddItem_Item>();
        for (int i = 0; i < options.Length; i++)
        {
            var option = options[i].GetOptionData();
            option.Name = ProductName.text;
            option.OneBox_Total_Count = int.Parse(CountPerBox.text);
            option.Person_Per_Count = int.Parse(CountPerOnce.text);
            option.Id = ItemId;
            option.Option_Id = i;
            Item.Products.Add(option);
        }

        DBManager.Instance.EditProduct(Item);

        var optionPopup = PopupController.Instance.GetPopup("Popup_Option");
        if (optionPopup != null)
        {
            optionPopup.GetComponent<UIPopup_Option>().ResetItems();
        }

        ClosePopup();
    }

    void ClearPopup()
    {
        while (Contents.childCount > 0)
        {
            DestroyImmediate(Contents.GetChild(0).gameObject);
        }
    }

    void ClosePopup()
    {
        PopupController.Instance.ClosePopup("Popup_EditItem");
    }
}
