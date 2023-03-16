using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotMachine_Item : MonoBehaviour
{
    public TextMeshProUGUI Name;

    public void SetItem(string name)
    {
        Name.SetText(name);
        gameObject.name = name;
    }
}
