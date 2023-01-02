using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : SingletonBase<DBManager>
{
    List<Product> ProductList = new List<Product>();
    int LastProductIdx;

    private void Start() 
    {
        GetProductTable();
    }

    void GetProductTable()
    {
        LoadFile();
    }

    void SaveFile()
    {
        for (int i = 0; i < ProductList.Count; i++)
        {
            for (int a = 0; a < ProductList[i].Products.Count; a++)
            {
                Debug.Log("ID = " + ProductList[i].Products[a].Id + " / Name = " + ProductList[i].Products[a].Name + " / Option_Id = " + ProductList[i].Products[a].Option_Id + " / Option_Name = " + ProductList[i].Products[a].Option_Name);
            }
        }
    }

    void LoadFile()
    {

    }

    public void AddProduct(List<Product_Option> newItems)
    {
        for (int i = 0; i < newItems.Count; i++)
        {
            if (!SearchDuplicatedItem(newItems[i]))
            {
                var newProduct = new Product();
                newProduct.Id = LastProductIdx + 1;
                newProduct.Name = newItems[0].Name;
                for (int a = 0; a < newItems.Count; a++)
                {
                    var newOption = new Product_Option();
                    newOption.Id = newProduct.Id;
                    newOption.Name = newProduct.Name;
                    newOption.Option_Id = JH_Util.GetOptionId(newProduct);
                    newOption.Option_Name = newItems[i].Option_Name;
                    newOption.OneBox_Total_Count = newItems[i].OneBox_Total_Count;
                    newOption.Person_Per_Count = newItems[i].Person_Per_Count;
                    newOption.Total_Box = newItems[i].Total_Box;
                    newOption.Sell_Count = newItems[i].Sell_Count;
                    newOption.Error_Count = newItems[i].Error_Count;
                    newOption.Person_Per_Price = newItems[i].Person_Per_Price;

                    newProduct.Products.Add(newOption);
                }
                ProductList.Add(newProduct);
            }
        }
        SaveFile();
    }

    bool SearchDuplicatedItem(Product_Option Item)
    {
        for (int i = 0; i < ProductList.Count; i++)
        {
            if (ProductList[i].Id == Item.Id)
            {
                Item.Id = JH_Util.GetOptionId(ProductList[i]);
                ProductList[i].Products.Add(Item);
                return true;
            }
        }
        return false;
    }

    public List<Product> GetProductList()
    {
        return ProductList;
    }
}
