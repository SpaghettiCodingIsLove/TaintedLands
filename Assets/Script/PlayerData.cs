using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Money;
    public int Level;
    public float MaxHealth;
    public int ArmourPrice;
    public float CurrentHealth;
    public float DamageProtection;
    public int AmountOfHealthPotion;
    public bool RedDiamondVisibility = false;
    public bool RedDiamondVisibilityLevel1 = false;
    public float[] Position;

    public PlayerData(PlayerStats player)
    {
        Money = player.Money;
        Level = player.Level;
        MaxHealth = player.MaxHealth;
        ArmourPrice = player.armourPrice;
        DamageProtection = player.damageProtection;
        AmountOfHealthPotion = player.amountOfHealthPotion;
        CurrentHealth = player.CurrentHealth;
        RedDiamondVisibility = player.redDiamondVisibility;
        RedDiamondVisibilityLevel1 = player.redDiamondVisibilityLevel1;
        Position = new float[3];
        Position[0] = player.transform.position.x;
        Position[1] = player.transform.position.y;
        Position[2] = player.transform.position.z;

    }
}
