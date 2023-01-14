using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected_Item : MonoBehaviour
{
    public Button ReduceBtn;
    public Button AddBtn;
    public Button RemoveBtn;
    public Text ProductName;
    public Text OptionName;
    public Text BoxInCount;
    public Text CountPerOnce;
    public Text Count;
    public Text Price;
    Product_Option Option;
    int intCount = 1;

    private void Start()
    {
        ReduceBtn.onClick.AddListener(OnClickReduceBtn);
        AddBtn.onClick.AddListener(OnClickAddBtn);
        RemoveBtn.onClick.AddListener(OnClickRemove);
    }

    public void SetItem(Product_Option option)
    {
        Option = option;
        ProductName.text = option.Name;
        OptionName.text = option.Option_Name;
        BoxInCount.text = option.OneBox_Total_Count.ToString();
        CountPerOnce.text = option.Person_Per_Count.ToString();
        Count.text = 1.ToString();
        Price.text = JH_Util.TransIntToWon(option.Person_Per_Price);
    }

    public void UpdatePrice()
    {
        Count.text = intCount.ToString();
        Price.text = JH_Util.TransIntToWon(Option.Person_Per_Price * intCount);
        GetParent().UpdateItemCount(Option, intCount);
    }

    void OnClickReduceBtn()
    {
        if (intCount <= 1) return;

        intCount--;
        UpdatePrice();
    }

    void OnClickAddBtn()
    {
        intCount++;
        UpdatePrice();
    }

    void OnClickRemove()
    {
        GetParent().RemoveItem(Option);
        Destroy(this.gameObject);
    }

    UIPopup_Start GetParent()
    {
        var popup = PopupController.Instance.GetPopup("Popup_Start");
        var component = popup.GetComponent<UIPopup_Start>();
        return component;
    }
}
