using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : SingletonBase<UIManager>
{
    public GameObject UIRoot;
    public List<GameObject> uiPanelActiveList = new List<GameObject>();
    GameObject[] uiPanelArr = new GameObject[(int)e_UIType.MaxCount];
    
    void Start()
    {
        
    }

    public GameObject GetUiRoot()
    {
        if (UIRoot == null)
        {
            UIRoot = GameObject.FindWithTag("UIRoot");
        }
        return UIRoot;
    }

    public T AddUIPanel<T>()
    {
        e_UIType _panelType = GetUIPanelType(typeof(T).ToString());
        return AddUIPanel(_panelType, typeof(T).ToString()).GetComponent<T>();
    }

    private GameObject AddUIPanel(e_UIType _panelType, string typeName)
    {
        Debug.Log("AddUIPanel : " + typeName);
        GameObject _popup = null;

        if (uiPanelArr[(int)_panelType] == null)
        {
            GameObject _tempPanel = ResourceManager.Instance.GetUIPanelPrefab(_panelType.ToString());
            if (_tempPanel == null)
            {
                Debug.LogError("---Panel_Prefab_Err----<" + typeName);
                return null;
            }

            _popup = Instantiate(_tempPanel, this.GetUiRoot().transform);
            _popup.name = typeName;
            uiPanelArr[(int)_panelType] = _popup;
        }
        else
        {
            _popup = uiPanelArr[(int)_panelType];
        }

        bool _listCheck = false;

        // uiPanelActiveList 에 잇 을 수 있 기 때 문 에.... 리 스 트에 서 제 거   
        for (int i = uiPanelActiveList.Count - 1; i >= 0; i--)
        {
            if (uiPanelActiveList[i] == null)
            {
                uiPanelActiveList.Remove(uiPanelActiveList[i]);
                continue;
            }

            if (uiPanelActiveList[i].name == _panelType.ToString())
            {
                _listCheck = true;
                break;
            }
        }

        if (_listCheck == false)
        {
            uiPanelActiveList.Add(_popup);
        }
        _popup.SetActive(true);
        _popup.transform.SetAsLastSibling();

        return _popup;
    }

    e_UIType GetUIPanelType(string _panelName)
    {
        for (int i = 0; i < (int)e_UIType.MaxCount; i++)
        {
            if (((e_UIType)i).ToString() == _panelName)
            {
                return (e_UIType)i;
            }
        }
        Debug.Log($"------GetUIPanelType ERR--<{_panelName}>----------------");
        return e_UIType.None;
    }

    public void CloseUiPanelGameObj(GameObject _panel)
    {
        Debug.Log("CloseUiPanelGameObj : " + _panel.name);
        e_UIType type = (e_UIType)Enum.Parse(typeof(e_UIType), _panel.name);

        this.CloseUiPanelType(type);
    }
    
    public void CloseUiPanelType(e_UIType _panelType)
    {
        for (int i = uiPanelActiveList.Count - 1; i >= 0; i--)
        {
            if (uiPanelActiveList[i].name == _panelType.ToString())
            {
                uiPanelActiveList[i].SetActive(false);
                uiPanelActiveList.Remove(uiPanelActiveList[i]);
                break;
            }
        }
    }
}
