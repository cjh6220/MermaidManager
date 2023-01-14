using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Util
{
    public static int GetOptionId(Product item)
    {
        int idx = 0;
        for (int i = 0; i < item.Products.Count; i++)
        {
            if (item.Products[i].Option_Id > idx)
            {
                idx = item.Products[i].Option_Id;
            }
        }

        if (idx == 0)
        {
            return 0;
        }
        else
        {
            return idx + 1;
        }
    }

    public static string TransIntToWon(int Price)
    {
        var str = "";
        str = string.Format("{0:0,0}", Price)  + "₩";
        return str;
    }
}
