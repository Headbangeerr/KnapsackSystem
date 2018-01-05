using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class GridUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action OnEnter;
    public static Action OnEixt;

    //通过继承IPointerEnterHandler接口实现函数
    public void OnPointerEnter(PointerEventData eventData)
    {        
        //pointerEnter是鼠标进入的gameObject对象
        if (eventData.pointerEnter.tag == "Grid")
        {
            Debug.Log(eventData.pointerEnter.name);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ////pointerEnter是鼠标进入的gameObject对象
        //if (eventData.pointerEnter.tag == "Grid")
        //{
        //    Debug.Log("鼠标离开了");
        //}
    }
}
