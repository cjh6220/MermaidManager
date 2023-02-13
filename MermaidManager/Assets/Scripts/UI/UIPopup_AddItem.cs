using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_AddItem : MonoBehaviour
{
    public GameObject Obj;
    public Transform Contents;
    public Button Exit;
    public InputField ProductName;
    public InputField CountPerBox;
    public InputField CountPerOnce;
    public InputField Price;
    public Button AddBtn;
    public Button SaveBtn;

    private void Awake()
    {
        Exit.onClick.AddListener(OnClickExit);
        AddBtn.onClick.AddListener(OnClickAddBtn);
        SaveBtn.onClick.AddListener(OnClickSaveBtn);
    }

    private void OnEnable()
    {
        ClearPopup();
    }

    void OnClickExit()
    {
        PopupController.Instance.ClosePopup("Popup_AddItem");
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
        var options = Contents.GetComponentsInChildren<UIPopup_AddItem_Item>();
        for (int i = 0; i < options.Length; i++)
        {
            var option = options[i].GetOptionData();
            option.Name = ProductName.text;
            option.OneBox_Total_Count = int.Parse(CountPerBox.text);
            option.Person_Per_Count = int.Parse(CountPerOnce.text);
            Item.Products.Add(option);
        }
        DBManager.Instance.AddProduct(Item);
        var optionPopup = PopupController.Instance.GetPopup("Popup_Option");
        if (optionPopup != null)
        {
            optionPopup.GetComponent<UIPopup_Option>().ResetItems();
        }
        OnClickExit();
    }

    void ClearPopup()
    {
        ProductName.text = "";
        CountPerBox.text = "0";
        CountPerOnce.text = "0";
        Price.text = "0";

        while (Contents.childCount > 0)
        {
            DestroyImmediate(Contents.GetChild(0).gameObject);
        }
    }
}
