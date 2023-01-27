using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo;
using UnityEngine.UI;

public class Product_List : MonoBehaviour
{
    public InitOnStart PoolStarter;
    public LoopScrollRect ScrollRect;

    private void Start() 
    {
        SetItems();
    }

    void SetItems()
    {
        var items = DBManager.Instance.GetProductList();
        PoolStarter.SetStart(items.Count);
    }

    public void UpdateItems()
    {
        ScrollRect.ClearCells();
        SetItems();
    }
}
