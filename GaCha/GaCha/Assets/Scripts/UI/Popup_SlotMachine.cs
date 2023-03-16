using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup_SlotMachine : MonoBehaviour
{
    public GameObject Item;
    public Transform Content;
    public TMP_InputField Input;
    public TextMeshProUGUI TotalCount;
    public Button AddButton;
    public Button ConfirmButton;
    public Button ResetButton;
    public Button SelectedButton;
    List<string> ItemList = new List<string>();
    List<string> WinnerList = new List<string>();

    private void Awake()
    {
        AddButton.onClick.AddListener(OnClickAddBtn);
        ConfirmButton.onClick.AddListener(OnClickConfirmBtn);
        ResetButton.onClick.AddListener(OnClickReset);
        SelectedButton.onClick.AddListener(OnClickWinner);
    }

    private void OnEnable() 
    {
        OnClickReset();
    }

    void AddItem()
    {
        if (string.IsNullOrEmpty(Input.text)) return;

        if (ItemList.Contains(Input.text)) return; //같은 이름 있음 경고메세지

        var newItem = Instantiate(Item, Content);
        newItem.GetComponent<SlotMachine_Item>().SetItem(Input.text);

        ItemList.Add(Input.text);

        UpdateTotalCount();
    }

    void OnClickAddBtn()
    {
        AddItem();
    }

    void OnClickConfirmBtn()
    {
        if(Content.childCount <= 0) return;

        var randomNumber = Random.Range(0, Content.childCount);
        //Debug.LogError("randomCount = " + randomNumber);
        WinnerList.Add(ItemList[randomNumber]);

        UIManager.Instance.AddUIPanel<Popup_SlotMachine_PickUp>().SetPopup(ItemList[randomNumber]);
        DestroyImmediate(Content.GetChild(randomNumber).gameObject);
        ItemList.RemoveAt(randomNumber);
    }

    void OnClickReset()
    {
        while (Content.childCount > 0)
        {
            DestroyImmediate(Content.GetChild(0).gameObject);
        }
        ItemList.Clear();
        WinnerList.Clear();
        UpdateTotalCount();
    }

    void OnClickWinner()
    {
        UIManager.Instance.AddUIPanel<Popup_SlotMachine_Winner>().SetPopup(WinnerList);
    }

    void UpdateTotalCount()
    {
        TotalCount.SetText("총 인원 = " + Content.childCount + " 명");
    }
}
