using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    //用于存储背包项目信息
    public int ID { get;private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int BuyPrice { get; private set; }//购买价格
    public int SellPrice { get; private set; }//出售价格
    public string Icon { get; private set; }//图标地址
    public enum Type { Armor, Consumable, Weapon }//项目类型
    public Type ItemType { get; protected set; }

    public Item(int id,string name ,string description, int buyPrice,int sellPrice, string icon)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Icon = icon;
    }

	
}
