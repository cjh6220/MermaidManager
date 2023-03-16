using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackBtn : MonoBehaviour
{
    public GameObject CloseTarget;
    public Button CloseBtn;

    private void Awake()
    {
        CloseBtn.onClick.AddListener(OnClickCloseBtn);
    }

    void OnClickCloseBtn()
    {
        UIManager.Instance.CloseUiPanelGameObj(CloseTarget);
    }
}
