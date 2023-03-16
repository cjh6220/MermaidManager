using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_SlotMachine_Winner : MonoBehaviour
{
    public GameObject Item;
    public Transform Content;
    public Button CloseBtn;

    private void Awake() 
    {
        CloseBtn.onClick.AddListener(OnClickCloseBtn);
    }

    public void SetPopup(List<string> winnerList)
    {
        ClearPopup();

        for (int i = 0; i < winnerList.Count; i++)
        {
            AddItem(winnerList[i]);
        }
    }

    void AddItem(string name)
    {
        var newItem = Instantiate(Item, Content);
        newItem.GetComponent<Winner_Item>().SetItem(name);
    }

    void OnClickCloseBtn()
    {
        UIManager.Instance.CloseUiPanelType(e_UIType.Popup_SlotMachine_Winner);
    }

    void ClearPopup()
    {
        while (Content.childCount > 0)
        {
            DestroyImmediate(Content.GetChild(0).gameObject);
        }
    }
}
