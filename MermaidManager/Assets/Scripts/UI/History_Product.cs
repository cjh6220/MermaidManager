using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class History_Product : MonoBehaviour
{
    public Text Title;

    public void SetItem(Data_Selected_Option data)
    {
        string str = "";
        str += data.Option.Name + " - " + data.Option.Option_Name;
        if (data.Count > 1)
        {
            str += "(X" + data.Count + ")";
        }
        Title.text = str;
    }
}
