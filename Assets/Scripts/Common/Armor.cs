using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Aromor : Item
{
  
    public int Power { get; private set; }
    public int Defend { get; private set; }
    public int Agility { get; private set; }
   
    public Aromor(int id, string name, string description, int buyPrice, int sellPrice, string icon,int power,int defend,int agility) 
        : base(id, name, description, buyPrice, sellPrice, icon)
    {
        this.Power = power;
        this.Defend = defend;
        this.Agility = agility;
        base.ItemType = Type.Armor;
    }
}

