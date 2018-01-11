using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemModel {
    private static Dictionary<string, Item> GridItem = new Dictionary<string, Item>();
    /// <summary>
    /// 放入背包一件物品
    /// </summary>
    /// <param name="name">Grid's name</param>
    /// <param name="item">object of item</param>
    public static  void  StoreItem(string name, Item item)
    {
        if(GridItem.ContainsKey(name))
        {
            DeleteItem(name);
        }
        GridItem.Add(name, item);
    }
    /// <summary>
    /// 获取物品
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static  Item GetItem(string name)
    {
        if (GridItem.ContainsKey(name))
        {
            return GridItem[name];
        }
        return null;
    }
    /// <summary>
    /// 删除物品
    /// </summary>
    /// <param name="name"></param>
    public static  void DeleteItem(string name)
    {
        if (GridItem.ContainsKey(name))
        {
            GridItem.Remove(name);
        }
    }

}
