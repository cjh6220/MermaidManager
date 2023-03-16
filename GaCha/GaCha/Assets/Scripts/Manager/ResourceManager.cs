using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingletonBase<ResourceManager>
{
    string ui_PrefabPath = "UI/";
    public GameObject GetUIPanelPrefab(string _panelName)
    {
        return Resources.Load<GameObject>(ui_PrefabPath + _panelName);
    }
}
