using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour {
    public Text OutlineText;
    public Text ContentText;

    public void UpdateItemInfo(string content)
    {
        OutlineText.text = content;
        ContentText.text = content;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
