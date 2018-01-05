using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int Demage { get; private set; }

    public Weapon(int id, string name, string description, int buyPrice, int sellPrice, string icon,int demage) : base(id, name, description, buyPrice, sellPrice, icon)
    {
        this.Demage = demage;
        base.ItemType = Type.Weapon;
    }
}
