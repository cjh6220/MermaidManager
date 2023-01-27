using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Start : MonoBehaviour
{
    public Product_List Product_List;
    public Product_Option_List Option_List;
    public UIPopup_Start_History History;
    public GameObject SelectedObj;
    public Button Exit;
    public Button RefreshBtn;
    public Button EditProductBtn;
    public Transform Content;
    public Button ConfirmBtn;
    public Button ResetBtn;
    public Text TotalPrice;
    public InputField ClientName;
    public Button ClientSearch;
    public Text ClientNickName;
    List<Data_Selected_Option> SelectedOption = new List<Data_Selected_Option>();
    Data_Client currentClient;

    private void Start()
    {
        RefreshBtn.onClick.AddListener(OnClickRefresh);
        Exit.onClick.AddListener(OnClickClose);
        EditProductBtn.onClick.AddListener(OnClickEditProduct);
        ClientSearch.onClick.AddListener(SearchClient);
        ConfirmBtn.onClick.AddListener(OnClickConfirm);
        ResetBtn.onClick.AddListener(OnClickReset);
    }

    public void SetOption(int idx)
    {
        Option_List.SetOption(idx);
    }

    public void SetSelectOption(int OptionID)
    {
        var selectedItem = DBManager.Instance.SelectedProduct;
        if (selectedItem != null)
        {
            var target = SelectedOption.Find(t => t.Option.Id == selectedItem.Id && t.Option.Option_Id == OptionID);
            if (target == null)
            {
                var newSelect = new Data_Selected_Option();
                newSelect.Count = 1;
                newSelect.Option = selectedItem.Products[OptionID];
                SelectedOption.Add(newSelect);
                AddSelectedItem(selectedItem.Products[OptionID]);
            }
        }
    }

    public void UpdateItemCount(Product_Option option, int count)
    {
        var target = SelectedOption.Find(t => t.Option.Id == option.Id && t.Option.Option_Id == option.Option_Id);
        if (target != null)
        {
            target.Count = count;
            CalTotalPrice();
        }
    }

    void AddSelectedItem(Product_Option option)
    {
        var item = Instantiate(SelectedObj, Content);
        item.GetComponent<Selected_Item>().SetItem(option);
        CalTotalPrice();
    }

    public void RemoveItem(Product_Option option)
    {
        var target = SelectedOption.Find(t => t.Option.Id == option.Id && t.Option.Option_Id == option.Option_Id);
        if (target != null)
        {
            SelectedOption.Remove(target);
            CalTotalPrice();
        }
    }

    void CalTotalPrice()
    {
        int price = 0;
        for (int i = 0; i < SelectedOption.Count; i++)
        {
            price += SelectedOption[i].Option.Person_Per_Price * SelectedOption[i].Count;
        }
        TotalPrice.text = JH_Util.TransIntToWon(price);
    }

    void SearchClient()
    {
        if (ClientName.text == "고객 명") return;
        var popup = PopupController.Instance.AddPopup("Popup_Search_Client");
        popup.GetComponent<Client_NickName>().SetClient(ClientName.text);
    }

    public void SetClient(Data_Client data)
    {
        currentClient = data;
        ClientNickName.text = data.NickName;
        var historyData = DBManager.Instance.LoadClientHistory(data);
        History.SetHistory();
    }

    void ClearSelectedItem()
    {
        while (Content.childCount > 0)
        {
            DestroyImmediate(Content.GetChild(0).gameObject);
        }
    }

    void OnClickRefresh()
    {
        Product_List.UpdateItems();
        DBManager.Instance.SelectedProduct = null;
        Option_List.ClearAllOption();
    }

    void OnClickEditProduct()
    {
        PopupController.Instance.AddPopup("Popup_Option");
    }

    void OnClickConfirm()
    {
        if (SelectedOption.Count <= 0) return;
        
        DBManager.Instance.SaveClientProduct(currentClient, SelectedOption);
        OnClickReset();
        Option_List.ClearAllOption();
        DBManager.Instance.SelectedProduct = null;
        currentClient = null;
        CalTotalPrice();
        ClientNickName.text = "";
        ClientName.text = "";
    }

    void OnClickReset()
    {
        ClearSelectedItem();
        SelectedOption.Clear();
    }

    void OnClickClose()
    {
        PopupController.Instance.ClosePopup("Popup_Start");
    }
}
