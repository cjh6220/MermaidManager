using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_AddItem_Item : MonoBehaviour
{
    public InputField OptionName;
    public InputField TotalCount;
    public InputField Sell_Count;
    public InputField Error_Count;
    public InputField Price;
    public Button DeleteBtn;

    private void Awake()
    {
        DeleteBtn.onClick.AddListener(DeleteOption);
    }

    public void SetOption(Product_Option option)
    {
        OptionName.text = option.Option_Name;
        //TotalCount.text = option.Total_Box.ToString();
        //Sell_Count.text = option.Sell_Count.ToString();
        //Error_Count.text = option.Error_Count.ToString();
        Price.text = option.Person_Per_Price.ToString();
    }

    public Product_Option GetOptionData()
    {
        var data = new Product_Option();
        data.Option_Name = OptionName.text;
        //data.Total_Box = int.Parse(TotalCount.text);
        //data.Sell_Count = int.Parse(Sell_Count.text);
        //data.Error_Count = int.Parse(Error_Count.text);
        data.Person_Per_Price = int.Parse(Price.text);

        return data;
    }

    void DeleteOption()
    {
        DestroyImmediate(this.gameObject);
    }
}
