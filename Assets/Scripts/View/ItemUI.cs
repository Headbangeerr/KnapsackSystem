using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemUI : MonoBehaviour {

    public Text ItemName;
    public void UpdateItemName(string itemName)
    {
        ItemName.text = itemName; 
    }

}
