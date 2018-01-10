using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class GridUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action<Transform> OnEnter=null;
    public static Action OnExit=null;

    //通过继承IPointerEnterHandler接口实现函数
    public void OnPointerEnter(PointerEventData eventData)
    {        
        //pointerEnter是鼠标进入的gameObject对象
        if (eventData.pointerEnter.tag == "Grid")
        {
            if (OnEnter != null)
            {
                OnEnter(transform);//这里的transform就是eventData.pointerEnter.transform，不需要写全
            }            
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       if(eventData.pointerEnter.tag=="Grid")
       {
            if (OnExit != null)
                OnExit();
       }
    }
}
