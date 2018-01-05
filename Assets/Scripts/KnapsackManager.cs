using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackManager : MonoBehaviour {

    private Dictionary<int, Item> ItemList = new Dictionary<int, Item>();
    public GridPanelUI GridPanelUI;
    public ItemInfoUI ItemInfoUI;

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
        //添加事件监听
        GridUI.OnEnter+=Grid_OnEnter;
        GridUI.OnExit+=Grid_OnExit;
    }

    public void StoreItem(int itemId)
    {
        if (!ItemList.ContainsKey(itemId))
        {
            return;
        }
        Item temp = ItemList[itemId];
        //使用Resources对象以Resources为根路径获取游戏物体
        GameObject itemPfb = Resources.Load<GameObject>("Prefabs/Item");
        //修改预制体的名字
        itemPfb.GetComponent<ItemUI>().UpdateItemName(temp.Name);       
        //通过GridPanelUI来获取一个空的格子
        Transform emptyGrid=GridPanelUI.GetEmptyGrid();
        if (emptyGrid == null)
        {
            Debug.LogWarning("背包已满！");
            return;
        }
        //在场景中示例化该物体
        GameObject itemGo = GameObject.Instantiate(itemPfb);
        itemGo.transform.SetParent(emptyGrid);
        //设置与父物体的相对位置为零
        itemGo.transform.localPosition = Vector3.zero;
        //设置大小为原大小
        itemGo.transform.localScale = Vector3.one;

        ItemModel.StoreItem(emptyGrid.name, temp);

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
    
    
    /// <summary>
    /// 鼠标进入事件监听函数
    /// </summary>
    public void Grid_OnEnter(Transform gridTransform)
    {      
        Item item = ItemModel.GetItem(gridTransform.name);
        if(item==null)            
            return;             
        ItemInfoUI.UpdateItemInfo(item.Name);

    }
    /// <summary>
    /// 鼠标移出时间监听函数
    /// </summary>
    public void Grid_OnExit()
    {
        
    }
}
