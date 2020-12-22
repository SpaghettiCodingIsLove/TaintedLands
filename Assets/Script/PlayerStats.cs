using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    #region game objects
    public HealthBar healthBar;
    public Text moneyAmount;
    public Text moneyInShop;
    public Text potionsAmount;
    public Text armourPriceText;
    public Button buyHealth;
    public Button useHealthPotion;
    public Text potionAmountInventory;
    public GameObject ShopUI;
    public GameObject InventoryUI;
    public Button buyArmour;
    #endregion

    #region player stats variables
    public int Money = 100;
    public int Level = 1;
    public float CurrentHealth;
    public float MaxHealth = 100;
    public static bool isShopOpen = false;
    public static bool isInventoryOpen = false;
    public static bool canOpenShop = false;
    public int armourPrice = 100;
    public float damageProtection = 1.0f;
    public int amountOfHealthPotion = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if(SaveSystem.doLoadFromFile == true)
        {
            LoadPLayer();
        }
        else
        {
            MaxHealth = MaxHealth * Level;
            CurrentHealth = MaxHealth;
        }

        potionsAmount.text = amountOfHealthPotion.ToString();
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(CurrentHealth);
        SetMoneyText();
        armourPriceText.text = armourPrice.ToString();
        moneyInShop.text = Money.ToString();
        buyHealth.onClick.AddListener(delegate () {
            BuyHealth();
        });
        buyArmour.onClick.AddListener(delegate ()
        {
            BuyArmour();
        });
        useHealthPotion.onClick.AddListener(delegate ()
        {
            if(amountOfHealthPotion > 0)
            {
                if(CurrentHealth + 50 >= MaxHealth)
                {
                    CurrentHealth = MaxHealth;
                }
                else
                {
                    CurrentHealth += 50;
                }
                amountOfHealthPotion -= 1;
                healthBar.SetHealth(CurrentHealth);
                potionsAmount.text = amountOfHealthPotion.ToString();
                potionAmountInventory.text = amountOfHealthPotion.ToString();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(20);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isShopOpen)
            {
                ResumeShop();
            }
            else
            {
                PauseShop();
            }
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            if (isInventoryOpen)
            {
                ResumeInventory();
            }
            else
            {
                PauseInventory();
            }
        }
    }

    #region Saving and Loading player stats
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPLayer()
    {
        PlayerData data = SaveSystem.LoadData();

        Money = data.Money;
        Level = data.Level;
        MaxHealth = data.MaxHealth;
        CurrentHealth = data.CurrentHealth;
        armourPrice = data.ArmourPrice;
        damageProtection = data.DamageProtection;
        amountOfHealthPotion = data.AmountOfHealthPotion;
        Vector3 position;
        position.x = data.Position[0];
        position.y = data.Position[1];
        position.z = data.Position[2];
        transform.position = position;
    }

    #endregion

    #region Other

    void TakeDamage(float damage)
    {
        CurrentHealth -= damage*damageProtection;
        healthBar.SetHealth(CurrentHealth);
    }

    void SetMoneyText()
    {
        moneyAmount.text = Money.ToString();
    }

    #endregion

    #region Buy health and armor methods

    private void BuyHealth()
    {
        if(Money >= 50)
        {
            Money -= 50;
            SetMoneyText();
            amountOfHealthPotion += 1;
            moneyInShop.text = Money.ToString();
            potionsAmount.text = amountOfHealthPotion.ToString();
        }
    }

    private void BuyArmour()
    {
        if(Money >= armourPrice)
        {
            Money -= armourPrice;
            SetMoneyText();
            damageProtection = damageProtection - 0.1f;
            armourPrice = armourPrice + 50*Level;
            moneyInShop.text = Money.ToString();
            armourPriceText.text = armourPrice.ToString();
        }
    }

    #endregion


    #region Open and close shop, inventory

    void ResumeShop()
    {
        ShopUI.SetActive(false);
        isShopOpen = false;
    }

    void PauseShop()
    {
        moneyInShop.text = Money.ToString();
        ShopUI.SetActive(true);
        isShopOpen = true;
    }

    void ResumeInventory()
    {
        InventoryUI.SetActive(false);
        isInventoryOpen = false;
    }

    void PauseInventory()
    {
        potionAmountInventory.text = amountOfHealthPotion.ToString();
        InventoryUI.SetActive(true);
        isInventoryOpen = true;
    }

    #endregion
}
