using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Demo;

public class UIPopup_Option : MonoBehaviour
{
    public Button ExitBtn;
    public InitOnStart PoolStarter;
    public LoopScrollRect ScrollRect;

    private void Awake()
    {
        ExitBtn.onClick.AddListener(ClosePopup);
    }

    private void Start()
    {
        SetItems();
    }

    void SetItems()
    {
        var items = DBManager.Instance.GetProductList();
        PoolStarter.SetStart(items.Count);
    }

    void ClosePopup()
    {
        PopupController.Instance.ClosePopup("Popup_Option");
    }

    public void ResetItems()
    {
        ScrollRect.ClearCells();        
        SetItems();
    }

    public void RefreshCells()
    {
        ScrollRect.RefreshCells();
    }

    public void RemoveLastItem()
    {
        //ScrollRect.DeleteItemAtEnd();
    }
}
