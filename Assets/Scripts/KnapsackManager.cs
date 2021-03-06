﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class KnapsackManager : MonoBehaviour {

    private Dictionary<int, Item> ItemList = new Dictionary<int, Item>();
    public GridPanelUI GridPanelUI;
    public ItemInfoUI ItemInfoUI;
    public DragItemUI DragItemUI;
    private bool IsShow=false;//装备信息显示标志位
    private bool IsDrag = false;//装备拖动标志位

    private static KnapsackManager _instance;
    public static KnapsackManager Instance {
        get { return _instance; }
    }
    private void Awake()
    {
        //单例
        _instance = this;
        //装载数据
        Load();
        //获取ItemInfo物体上的ItemInfoUI组件，通过public方式获取会造成空指针异常？未解之谜？！
        ItemInfoUI = GameObject.Find("ItemInfo").GetComponent<ItemInfoUI>();
        DragItemUI = GameObject.Find("DragItem").GetComponent<DragItemUI>();    
        //添加事件监听
        GridUI.OnEnter = GridUI_OnEnter;
        GridUI.OnExit = GridUI_OnExit;
        GridUI.OnLeftBeginDrag = GridUI_OnLeftBeginDrag;
        GridUI.OnLeftEndDrag = GridUI_OnLeftEndDrag;
    }

    private void Update()
    {

        if (IsDrag)
        {
            Vector2 position;
            //使用转换工具，将鼠标位置坐标转化为UI控件的相对坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("KnapsackUI").transform as RectTransform, Input.mousePosition, null, out position);
            DragItemUI.Show();
            DragItemUI.SetLocalPosition(position);
        }
        else if (IsShow)
        {
            Vector2 position;
            //使用转换工具，将鼠标位置坐标转化为UI控件的相对坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("KnapsackUI").transform as RectTransform, Input.mousePosition, null, out position);
            ItemInfoUI.Show();          
            ItemInfoUI.SetLocalPosition(position);
            //这里需要将ItemInfo的UI组件的中心设置在左上角
        }                       
    }
   
    public void PutIntoGrid(Item item ,Transform grid)
    {
        GameObject itemPfb = Resources.Load<GameObject>("Prefabs/Item");
        //修改预制体的名字
        itemPfb.GetComponent<ItemUI>().UpdateItemName(item.Name);
        //在场景中示例化该物体
        GameObject itemGo = GameObject.Instantiate(itemPfb);
        itemGo.transform.SetParent(grid);
        //设置与父物体的相对位置为零
        itemGo.transform.localPosition = Vector3.zero;
        //设置大小为原大小
        itemGo.transform.localScale = Vector3.one;
        //存入数据
        ItemModel.StoreItem(grid.name, item);
    }
  
    public void StoreItem(int itemId)
    {
        if (!ItemList.ContainsKey(itemId))
        {
            return;
        }
        Item temp = ItemList[itemId];
          
        //通过GridPanelUI来获取一个空的格子
        Transform emptyGrid=GridPanelUI.GetEmptyGrid();
        if (emptyGrid == null)
        {
            Debug.LogWarning("背包已满！");
            return;
        }
        this.PutIntoGrid(temp, emptyGrid);       
    }
    /// <summary>
    /// 模拟从数据库中访问数据
    /// </summary>      
    public void Load()
    {
        Weapon w1 = new Weapon(0,"大刀","锋利的杀猪刀",1000,500,"",100);
        Weapon w2 = new Weapon(1, "长枪", "修长的长枪", 800, 400, "", 300);
        Weapon w3 = new Weapon(2, "太刀", "锋利的小太刀", 5000, 2500, "", 500);
        Weapon w4 = new Weapon(3, "棍子", "十分顺手的钝器", 100, 50, "", 10);

        Consumable s1 = new Consumable(4, "蓝瓶", "回复一定的MP", 100, 40, "", 0, 100);
        Consumable s2 = new Consumable(5, "红瓶", "回复一定的HP", 100, 40, "", 100, 0);

        Aromor a1 = new Aromor(6, "头盔", "简陋的头盔，貌似只能防御一些轻微攻击", 200, 100, "", 10, 50, 5);
        Aromor a2= new Aromor(7, "胸甲", "简陋的胸甲，貌似只能防御一些轻微攻击", 200, 100, "", 10, 50, 5);
        Aromor a3 = new Aromor(8, "护腿", "简陋的护腿，貌似只能防御一些轻微攻击", 200, 100, "", 10, 50, 5);
        Aromor a4 = new Aromor(9, "护肩", "简陋的护肩，貌似只能防御一些轻微攻击", 200, 100, "", 10, 50, 5);

        ItemList.Add(w1.ID, w1);
        ItemList.Add(w2.ID, w2);
        ItemList.Add(w3.ID, w3);
        ItemList.Add(w4.ID, w4);
        ItemList.Add(s1.ID, s1);
        ItemList.Add(s2.ID, s2);
        ItemList.Add(a1.ID, a1);
        ItemList.Add(a2.ID, a2);
        ItemList.Add(a3.ID, a3);
        ItemList.Add(a4.ID, a4);
    }

    #region 事件回调方法
    /// <summary>
    /// 鼠标进入事件监听函数
    /// </summary>
    private void GridUI_OnEnter(Transform gridTransform)
    {      
        Item item = ItemModel.GetItem(gridTransform.name);
        if(item==null)            
            return;
        string text=GetItemInfo(item);
        ItemInfoUI.UpdateItemInfo(text);
        IsShow = true;
    }
    /// <summary>
    /// 鼠标移出事件监听函数
    /// </summary>
    private  void GridUI_OnExit()
    {
        ItemInfoUI.Hide();
        IsShow = false;
    }
    /// <summary>
    /// 鼠标拖拽事件回调方法
    /// </summary>
    /// <param name="gridTransform"></param>
    private void GridUI_OnLeftBeginDrag(Transform gridTransform)
    {       
        //如果被拖拽的对象没有子物体则直接返回
        if (gridTransform.childCount == 0)
            return;
        else
        {
            Item item = ItemModel.GetItem(gridTransform.name);    
            DragItemUI.UpdateItemName(item.Name);
            Destroy(gridTransform.GetChild(0).gameObject);
            IsDrag = true;
        }
    }
    private void GridUI_OnLeftEndDrag(Transform beginGrid,Transform endGrid)
    {
        if (endGrid == null)//丢弃物品
        {
            ItemModel.DeleteItem(beginGrid.name);
        }
        else if (endGrid.tag == "Grid")//放入一个空的或有物体的格子中
        {
            if (endGrid.childCount == 0)//该格子为空
            {
                Item item = ItemModel.GetItem(beginGrid.name);
                this.PutIntoGrid(item, endGrid);               
                //在完成物品转移以后移除数据               
                ItemModel.DeleteItem(beginGrid.name);
            }
            else//目标格子中已有物品
            {
                Item beginItem = ItemModel.GetItem(beginGrid.name);
                Item endItem = ItemModel.GetItem(endGrid.name);
                //把目标格子中的已有物品先删除掉
                Destroy(endGrid.GetChild(0).gameObject);               
                this.PutIntoGrid(beginItem, endGrid);
                this.PutIntoGrid(endItem, beginGrid);               
            }
        }
        else//放到背包外面
        {
            Item item = ItemModel.GetItem(beginGrid.name);
            this.PutIntoGrid(item, beginGrid);
        }
        IsDrag = false;
        DragItemUI.Hide();
    }
 

    #endregion
    private string GetItemInfo(Item item)
    {
        if (item == null)
            return "";
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<color=red>{0}</color>\n\n", item.Name);
        switch (item.ItemType)
        {
            case Item.Type.Armor:
                Aromor aromor = item as Aromor;
                sb.AppendFormat("力量：{0}\n防御：{1}\n敏捷：{2}\n\n", aromor.Power, aromor.Defend, aromor.Agility);
                break;
            case Item.Type.Consumable:
                Consumable consumable = item as Consumable;
                sb.AppendFormat("HP:{0}\nMP:{1}\n\n", consumable.BackHp, consumable.BackMp);
                break;
            case Item.Type.Weapon:
                Weapon weapon = item as Weapon;
                sb.AppendFormat("攻击力：{0}\n\n", weapon.Demage);
                break;
            default:break;
        }
        sb.AppendFormat("<size=25><color=white>购买价格：{0}\n出售价格：{1}</color></size>\n\n" +
            "<color=yellow><size=20>描述：{2}</size></color>", item.BuyPrice, item.SellPrice, item.Description);
        return sb.ToString();
    }
}
