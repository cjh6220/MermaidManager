using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class DBManager : SingletonBase<DBManager>
{
    public List<Product> ProductList = new List<Product>();
    public List<Data_Client> ClientList = new List<Data_Client>();
    public Product SelectedProduct = new Product();
    public Data_Client_History SelectedHistory = new Data_Client_History();
    [SerializeField]
    int LastProductIdx;
    [SerializeField]
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
        int lastProductIdx = 0;
        var productOptions = new List<Product_Option>();
        for (int i = 0; i < ProductList.Count; i++)
        {
            for (int a = 0; a < ProductList[i].Products.Count; a++)
            {
                productOptions.Add(ProductList[i].Products[a]);
                if (ProductList[i].Products[a].Id > lastProductIdx)
                {
                    lastProductIdx = ProductList[i].Products[a].Id;
                }
                Debug.Log("ID = " + ProductList[i].Products[a].Id + " / Name = " + ProductList[i].Products[a].Name + " / Option_Id = " + ProductList[i].Products[a].Option_Id + " / Option_Name = " + ProductList[i].Products[a].Option_Name);
            }
        }
        var str = JsonConvert.SerializeObject(productOptions);
#if UNITY_EDITOR
        File.WriteAllText(Application.dataPath + "/ProductDB.json", str);
#else
        File.WriteAllText(Application.persistentDataPath + "/ProductDB.json", str);
#endif

        var clientStr = JsonConvert.SerializeObject(ClientList);
#if UNITY_EDITOR
        File.WriteAllText(Application.dataPath + "/ClientDB.json", clientStr);
#else
        File.WriteAllText(Application.persistentDataPath + "/ClientDB.json", clientStr);
#endif

        PlayerPrefs.SetInt("LastProductIdx", lastProductIdx);
        LastProductIdx = lastProductIdx;
        //PlayerPrefs.SetInt("LastProductIdx", _userData.LastProductIdx);
    }

    void SaveProduct()
    {
        int lastProductIdx = 0;
        var productOptions = new List<Product_Option>();
        for (int i = 0; i < ProductList.Count; i++)
        {
            for (int a = 0; a < ProductList[i].Products.Count; a++)
            {
                productOptions.Add(ProductList[i].Products[a]);
                if (ProductList[i].Products[a].Id > lastProductIdx)
                {
                    lastProductIdx = ProductList[i].Products[a].Id;
                }
                Debug.Log("ID = " + ProductList[i].Products[a].Id + " / Name = " + ProductList[i].Products[a].Name + " / Option_Id = " + ProductList[i].Products[a].Option_Id + " / Option_Name = " + ProductList[i].Products[a].Option_Name);
            }
        }
        var str = JsonConvert.SerializeObject(productOptions);
#if UNITY_EDITOR
        File.WriteAllText(Application.dataPath + "/ProductDB.json", str);
#else
        File.WriteAllText(Application.persistentDataPath + "/ProductDB.json", str);
#endif
        PlayerPrefs.SetInt("LastProductIdx", lastProductIdx);
        LastProductIdx = lastProductIdx;
    }

    void SaveClient()
    {
        var clientStr = JsonConvert.SerializeObject(ClientList);
#if UNITY_EDITOR
        File.WriteAllText(Application.dataPath + "/ClientDB.json", clientStr);
#else
        File.WriteAllText(Application.persistentDataPath + "/ClientDB.json", clientStr);
#endif
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

            //LastClientIdx = PlayerPrefs.GetInt("LastIdx");
            LastProductIdx = PlayerPrefs.GetInt("LastProductIdx");
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/ProductDB.json", "");
            PlayerPrefs.SetInt("LastIdx", 0);
            PlayerPrefs.SetInt("LastProductIdx", 0);
        }

        if (File.Exists(Application.dataPath + "/ClientDB.json"))
        {
            var jsonStrRead_Client = File.ReadAllText(Application.dataPath + "/ClientDB.json");
            var deserializedBarList_Client = JsonConvert.DeserializeObject<List<Data_Client>>(jsonStrRead_Client);
            ClientList = deserializedBarList_Client;
            if (ClientList == null)
            {
                ClientList = new List<Data_Client>();
            }
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/ClientDB.json", "");
        }
#else 
        Debug.Log("LoadJson Android");
        if (File.Exists(Application.persistentDataPath + "/ProductDB.json"))
        {
            var jsonStrRead = File.ReadAllText(Application.persistentDataPath + "/ProductDB.json");
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
            LastProductIdx = PlayerPrefs.GetInt("LastProductIdx");
        }
        else
        {
            File.WriteAllText(Application.persistentDataPath + "/ProductDB.json", "");            
            PlayerPrefs.SetInt("LastProductIdx", 0);
        }

        if (File.Exists(Application.persistentDataPath + "/ClientDB.json"))
        {
            var jsonStrRead_Client = File.ReadAllText(Application.persistentDataPath + "/ClientDB.json");
            var deserializedBarList_Client = JsonConvert.DeserializeObject<List<Data_Client>>(jsonStrRead_Client);
            ClientList = deserializedBarList_Client;
            if (ClientList == null)
            {
                ClientList = new List<Data_Client>();
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
                    //newOption.Total_Box = newItems[i].Total_Box;
                    //newOption.Sell_Count = newItems[i].Sell_Count;
                    //newOption.Error_Count = newItems[i].Error_Count;
                    newOption.Person_Per_Price = newItems[i].Person_Per_Price;

                    newProduct.Products.Add(newOption);
                }
                ProductList.Add(newProduct);
            }
        }
        SaveProduct();
    }

    public void AddProduct(Product newItems)
    {
        newItems.Id = LastProductIdx + 1;
        newItems.Name = newItems.Products[0].Name;
        for (int i = 0; i < newItems.Products.Count; i++)
        {
            newItems.Products[i].Id = newItems.Id;
            newItems.Products[i].Option_Id = i;
        }

        ProductList.Add(newItems);
        SaveProduct();
    }

    public void EditProduct(Product newItems)
    {
        var target = ProductList.Find(t => t.Id == newItems.Id);
        if (target != null)
        {
            target.Products = newItems.Products;
        }
        else
        {
            Debug.LogError("기존 물품 데이터가 없음");
        }
        SaveProduct();
    }

    public void RemoveProductByID(int ID)
    {
        var target = ProductList.Find(t => t.Id == ID);
        if (target != null)
        {
            ProductList.Remove(target);
        }
        SaveProduct();
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

    public Product GetProductByOrder(int order)
    {
        if (order >= ProductList.Count)
        {
            return null;
        }
        else
        {
            return ProductList[order];
        }
    }

    public Product GetProductByID(int ID)
    {
        var target = ProductList.Find(t => t.Id == ID);
        return target;
    }

    public List<Data_Client> GetClientBtName(string name)
    {
        var target = ClientList.FindAll(t => t.Client_Name == name);
        return target;
    }

    public void AddNewClient(Data_Client data)
    {
        var search = ClientList.FindAll(t => t.Client_Name == data.Client_Name && t.NickName == data.NickName);
        if (search.Count <= 0)
        {
            ClientList.Add(data);
            SaveClient();
        }
    }

    public void RemoveClient(Data_Client data)
    {
        var search = ClientList.Find(t => t.Client_Name == data.Client_Name && t.NickName == data.NickName);
        if (search != null)
        {
            ClientList.Remove(search);
            SaveClient();
        }
    }

    public void SaveClientProduct(Data_Client client, List<Data_Selected_Option> optionList)
    {
        for (int i = 0; i < optionList.Count; i++)
        {
            Debug.LogError(i + "번째 물건 = " + optionList[i].Option.Name + " / 옵션명 = " + optionList[i].Option.Option_Name);
        }
        var item = ClientList.Find(t => t.Client_Name == client.Client_Name && t.NickName == client.NickName);
        if (item != null)
        {
            var newProducts = new Data_Client_Product_History();
            newProducts.Date = DateTime.Now;
            newProducts.ProductList = optionList;
#if UNITY_EDITOR
            if (File.Exists(Application.dataPath + "/ClientData/" + client.Client_Name + "~" + client.NickName + ".json"))//고객 정보 있을때
            {
                var jsonStrRead = File.ReadAllText(Application.dataPath + "/ClientData/" + client.Client_Name + "~" + client.NickName + ".json");
                var deserializedBarList = JsonConvert.DeserializeObject<Data_Client_History>(jsonStrRead);
                deserializedBarList.History.Add(newProducts);
                Sort(deserializedBarList);

                var clientStr = JsonConvert.SerializeObject(deserializedBarList);
                File.WriteAllText(Application.dataPath + "/ClientData/" + client.Client_Name + "~" + client.NickName + ".json", clientStr);
            }
            else //고객 정보 없을때
            {
                var newClient = new Data_Client_History();
                newClient.Client = client;
                newClient.History.Add(newProducts);

                var clientStr = JsonConvert.SerializeObject(newClient);
                if (!File.Exists(Application.dataPath + "/ClientData"))
                {
                    Directory.CreateDirectory(Application.dataPath + "/ClientData");
                }
                File.WriteAllText(Application.dataPath + "/ClientData/" + client.Client_Name + "~" + client.NickName + ".json", clientStr);
            }
#else
            if (File.Exists(Application.persistentDataPath + "/ClientData/" + client.Client_Name + "~" + client.NickName + ".json"))//고객 정보 있을때
            {
                var jsonStrRead = File.ReadAllText(Application.persistentDataPath + "/ClientData/" + client.Client_Name + "~" + client.NickName + ".json");
                var deserializedBarList = JsonConvert.DeserializeObject<Data_Client_History>(jsonStrRead);
                deserializedBarList.History.Add(newProducts);
                Sort(deserializedBarList);

                var clientStr = JsonConvert.SerializeObject(deserializedBarList);
                File.WriteAllText(Application.persistentDataPath + "/ClientData/" + client.Client_Name + "~" + client.NickName + ".json", clientStr);
            }
            else //고객 정보 없을때
            {
                var newClient = new Data_Client_History();
                newClient.Client = client;
                newClient.History.Add(newProducts);

                var clientStr = JsonConvert.SerializeObject(newClient);
                if (!File.Exists(Application.persistentDataPath + "/ClientData"))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/ClientData");
                }                
                File.WriteAllText(Application.persistentDataPath + "/ClientData/" + client.Client_Name + "~" + client.NickName + ".json", clientStr);    
            }
#endif
        }
    }

    public Data_Client_History LoadClientHistory(Data_Client data)
    {
#if UNITY_EDITOR
        if (File.Exists(Application.dataPath + "/ClientData/" + data.Client_Name + "~" + data.NickName + ".json"))
        {
            var jsonStrRead = File.ReadAllText(Application.dataPath + "/ClientData/" + data.Client_Name + "~" + data.NickName + ".json");
            var deserializedBarList = JsonConvert.DeserializeObject<Data_Client_History>(jsonStrRead);
            Sort(deserializedBarList);
            SelectedHistory = deserializedBarList;
            return deserializedBarList;
        }
#else
        if (File.Exists(Application.persistentDataPath + "/ClientData/" + data.Client_Name + "~" + data.NickName + ".json"))
        {
            var jsonStrRead = File.ReadAllText(Application.persistentDataPath + "/ClientData/" + data.Client_Name + "~" + data.NickName + ".json");
            var deserializedBarList = JsonConvert.DeserializeObject<Data_Client_History>(jsonStrRead);
            Sort(deserializedBarList);
            SelectedHistory = deserializedBarList;
            return deserializedBarList;
        }
#endif
        else
        {
            SelectedHistory = new Data_Client_History();
            return null;
        }
    }

    void Sort(Data_Client_History data)
    {
        data.History.Sort((char1, char2) =>
        {
            var check1 = char1;
            var check2 = char2;

            var value = check2.Date.CompareTo(check1.Date);

            return value;
        });
    }
}
