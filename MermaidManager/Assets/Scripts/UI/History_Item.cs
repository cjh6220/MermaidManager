using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class History_Item : MonoBehaviour
{
    public GameObject Obj;
    public Transform Content;
    public Text Date;
    public ContentSizeFitter CSF;
    Data_Client_Product_History history;
    void ScrollCellIndex(int idx)
    {
        history = DBManager.Instance.SelectedHistory.History[idx];
        Date.text = history.Date.ToString("yyyy/MM/dd");
        for (int i = 0; i < history.ProductList.Count; i++)
        {
            var item = Instantiate(Obj, Content);
            item.GetComponent<History_Product>().SetItem(history.ProductList[i]);
        }
        CSF.SetLayoutVertical();
    }
}
