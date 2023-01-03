using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DBManager : SingletonBase<DBManager>
{
    List<Product> ProductList = new List<Product>();
    int LastProductIdx;
    int LastClientIdx;

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
        var productOptions = new List<Product_Option>();
        for (int i = 0; i < ProductList.Count; i++)
        {
            for (int a = 0; a < ProductList[i].Products.Count; a++)
            {
                productOptions.Add(ProductList[i].Products[a]);
                Debug.Log("ID = " + ProductList[i].Products[a].Id + " / Name = " + ProductList[i].Products[a].Name + " / Option_Id = " + ProductList[i].Products[a].Option_Id + " / Option_Name = " + ProductList[i].Products[a].Option_Name);
            }
        }
        var str = JsonConvert.SerializeObject(productOptions);
#if UNITY_EDITOR
        File.WriteAllText(Application.dataPath + "/ProductDB.json", str);
#else
        File.WriteAllText(Application.persistentDataPath + "/ProductDB.json", str);
#endif

//         var clientStr = JsonConvert.SerializeObject(_userData.ClientList);
// #if UNITY_EDITOR
//         File.WriteAllText(Application.dataPath + "/ClientDB.json", clientStr);
// #else
//         File.WriteAllText(Application.persistentDataPath + "/ClientDB.json", clientStr);
// #endif

//         PlayerPrefs.SetInt("LastIdx", _userData.LastIdx);
//         PlayerPrefs.SetInt("LastProductIdx", _userData.LastProductIdx);
    }

    void LoadFile()
    {
        #if UNITY_EDITOR
        Debug.Log("LoadJson Editor");
        if (File.Exists(Application.dataPath + "/ProductDB.json")) //상품 파일 있을때
        {
            var jsonStrRead = File.ReadAllText(Application.dataPath + "/ProductDB.json");
            var deserializedBarList = JsonConvert.DeserializeObject<List<Product_Option>>(jsonStrRead);
            var tableProducts = new List<Product>();
            if (deserializedBarList != null)
            {
                for (int i = 0; i < deserializedBarList.Count; i++)
                {
                    var item = tableProducts.Find(t => t.Id == deserializedBarList[i].Id);
                    if (item != null) //이미 저장됨
                    {
                        item.Products.Add(deserializedBarList[i]);
                    }
                    else
                    {
                        var newProduct = new Product();
                        newProduct.Id = deserializedBarList[i].Id;
                        newProduct.Name = deserializedBarList[i].Name;
                        newProduct.Products.Add(deserializedBarList[i]);

                        tableProducts.Add(newProduct);
                    }
                }
            }
            ProductList = tableProducts;
            // _userData.ProductList = deserializedBarList;
            // if (_userData.ProductList == null)
            // {
            //     _userData.ProductList = new List<Product>();
            // }

            LastClientIdx = PlayerPrefs.GetInt("LastIdx");
            LastProductIdx = PlayerPrefs.GetInt("LastProductIdx");
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/ProductDB.json", "");
            PlayerPrefs.SetInt("LastIdx", 0);
            PlayerPrefs.SetInt("LastProductIdx", 0);
        }

        // if (File.Exists(Application.dataPath + "/ClientDB.json"))
        // {
        //     var jsonStrRead_Client = File.ReadAllText(Application.dataPath + "/ClientDB.json");
        //     var deserializedBarList_Client = JsonConvert.DeserializeObject<List<Data_Client>>(jsonStrRead_Client);
        //     _userData.ClientList = deserializedBarList_Client;
        //     if (_userData.ClientList == null)
        //     {
        //         _userData.ClientList = new List<Data_Client>();
        //     }
        // }
        // else
        // {
        //     File.WriteAllText(Application.dataPath + "/ClientDB.json", "");
        // }
#else 
        Debug.Log("LoadJson Android");
        if (File.Exists(Application.persistentDataPath + "/ProductDB.json"))
        {
            var jsonStrRead = File.ReadAllText(Application.persistentDataPath + "/ProductDB.json");
            var deserializedBarList = JsonConvert.DeserializeObject<List<Product>>(jsonStrRead);
            _userData.ProductList = deserializedBarList;
            if (_userData.ProductList == null)
            {
                _userData.ProductList = new List<Product>();
            }

            _userData.LastIdx = PlayerPrefs.GetInt("LastIdx");
            _userData.LastProductIdx = PlayerPrefs.GetInt("LastProductIdx");
        }
        else
        {
            File.WriteAllText(Application.persistentDataPath + "/ProductDB.json", "");
            PlayerPrefs.SetInt("LastIdx", 0);
            PlayerPrefs.SetInt("LastProductIdx", 0);
        }

        if (File.Exists(Application.persistentDataPath + "/ClientDB.json"))
        {
            var jsonStrRead_Client = File.ReadAllText(Application.persistentDataPath + "/ClientDB.json");
            var deserializedBarList_Client = JsonConvert.DeserializeObject<List<Data_Client>>(jsonStrRead_Client);
            _userData.ClientList = deserializedBarList_Client;
            if (_userData.ClientList == null)
            {
                _userData.ClientList = new List<Data_Client>();
            }
        }
        else
        {
            File.WriteAllText(Application.persistentDataPath + "/ClientDB.json", "");
        }
#endif

        //SendMessage(MessageID.Event_Set_Log, "DB Load Success");
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
