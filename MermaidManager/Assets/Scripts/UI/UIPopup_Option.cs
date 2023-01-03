using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Demo;

public class UIPopup_Option : MonoBehaviour
{
    public InitOnStart PoolStarter;

    private void Start() 
    {
        PoolStarter.SetStart(50);
    }

    void SetItems()
    {
        // var items = DBManager.Instance.GetProductList();
        // for (int i = 0; i < items.Count; i++)
        // {
        //     var item = Instantiate(Obj,Content);
        //     //item.GetComponent<UIPopup_Option_Item>().SetItem(items[i]);
        // }
    }
}
