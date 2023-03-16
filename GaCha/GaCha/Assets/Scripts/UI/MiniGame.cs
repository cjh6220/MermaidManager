using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public Button SlotMachineBtn;

    private void Awake()
    {
        SlotMachineBtn.onClick.AddListener(OnClickSlotMachine);
    }

    void OnClickSlotMachine()
    {
        UIManager.Instance.AddUIPanel<Popup_SlotMachine>();
    }
}
