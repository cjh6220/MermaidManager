using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo;
using UnityEngine.UI;

public class Product_Option_List : MonoBehaviour
{
    public InitOnStart PoolStarter;
    public LoopScrollRect ScrollRect;
    public void SetOption(int id)
    {
        var items = DBManager.Instance.GetProductByID(id);
        DBManager.Instance.SelectedProduct = items;
        PoolStarter.SetStart(items.Products.Count);
    }

    public void ClearAllOption()
    {
        ScrollRect.ClearCells();
    }
}
