using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Money;
    public int currentExp;
    public int Level;
    public float MaxHealth;
    public int ArmourPrice;
    public float CurrentHealth;
    public float DamageProtection;
    public int AmountOfHealthPotion;
    public float[] Position;
    public int NumOfKills;
    public int NumOfFoundDiamonds;

    public PlayerData(PlayerStats player)
    {
        currentExp = player.currentExp;
        Money = player.Money;
        Level = player.Level;
        MaxHealth = player.MaxHealth;
        ArmourPrice = player.armourPrice;
        DamageProtection = player.damageProtection;
        AmountOfHealthPotion = player.amountOfHealthPotion;
        CurrentHealth = player.CurrentHealth;
        NumOfKills = player.NumOfKills;
        NumOfFoundDiamonds = player.NumOfFoundDiamonds;
        Position = new float[3];
        Position[0] = player.transform.position.x;
        Position[1] = player.transform.position.y;
        Position[2] = player.transform.position.z;

    }
}
