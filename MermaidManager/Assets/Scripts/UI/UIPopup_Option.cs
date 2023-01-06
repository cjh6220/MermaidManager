using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Demo;
using UnityEngine.UI;

public class UIPopup_Option : MonoBehaviour
{
    public Button ExitBtn;
    public InitOnStart PoolStarter;

    private void Awake()
    {
        ExitBtn.onClick.AddListener(ClosePopup);
    }

    private void Start()
    {
        //PoolStarter.SetStart(50);
    }

    void SetItems()
    {
        var items = DBManager.Instance.GetProductList();
        PoolStarter.SetStart(items.Count);
        // for (int i = 0; i < items.Count; i++)
        // {
        //     var item = Instantiate(Obj,Content);
        //     //item.GetComponent<UIPopup_Option_Item>().SetItem(items[i]);
        // }
    }

    void ClosePopup()
    {
        PopupController.Instance.ClosePopup("Popup_Option");
    }
}
