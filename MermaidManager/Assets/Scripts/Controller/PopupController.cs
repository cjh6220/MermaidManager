using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : SingletonBase<PopupController>
{
    public Transform PopupRoot;
    List<GameObject> PopupList = new List<GameObject>();

    public GameObject AddPopup(string name)
    {
        GameObject popup = null;
        var item = PopupList.Find(t => t.name == name);
        if (item != null)
        {
            return item;
        }
        else
        {
            var obj = Resources.Load<GameObject>("UI/" + name);
            if (obj != null)
            {
                popup = Instantiate(obj, Vector3.zero, Quaternion.identity, PopupRoot);
                PopupList.Add(popup);
            }
        }
        return popup;
    }

    public void ClosePopup(string name)
    {
        for (int i = 0; i < PopupList.Count; i++)
        {
            if (PopupList[i].name == name)
            {
                PopupList[i].SetActive(false);
            }
        }
    }
}
