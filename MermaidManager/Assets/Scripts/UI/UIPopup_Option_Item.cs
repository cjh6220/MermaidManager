using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup_Option_Item : MonoBehaviour
{
    void ScrollCellIndex (int idx) 
    {
		string name = "Cell " + idx.ToString ();
		// if (text != null) 
        // {
		// 	text.text = name;
		// }
        // if (image != null)
        // {
        //     image.color = Rainbow(idx / 50.0f);
        // }
		gameObject.name = name;
	}
}
