using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
/// <summary>
/// 分别继承名实现各种事件接口，这里需要注意的是IDragHandler所实现的OnDrag虽然没有用，但是这里也必须继承IDragHandler接口
/// </summary>
public class GridUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    ,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    #region Mouse Enter& Exit
    public static Action<Transform> OnEnter ;
    public static Action OnExit ;

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
    #endregion
    #region Mouse Drag
    public static Action<Transform> OnLeftBeginDrag;
    public static Action<Transform,Transform> OnLeftEndDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnLeftBeginDrag != null)
            {
                OnLeftBeginDrag(transform);
            }                
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
     
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnLeftEndDrag != null)
            {
                if (eventData.pointerEnter == null)
                {
                    OnLeftEndDrag(transform,null);
                }
                else
                {
                    OnLeftEndDrag(transform,eventData.pointerEnter.transform);
                }
                
            }
            
        }
    }
    #endregion
}
