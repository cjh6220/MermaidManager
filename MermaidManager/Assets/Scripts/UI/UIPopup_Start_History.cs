using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo;
using UnityEngine.UI;
public class UIPopup_Start_History : MonoBehaviour
{
    public InitOnStart Scroll;
    public LoopScrollRect LoopScroll;
    public ContentSizeFitter CSF;
    public void SetHistory()
    {
        LoopScroll.ClearCells();
        Scroll.SetStart(DBManager.Instance.SelectedHistory.History.Count);        
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)CSF.transform);
    }
}
