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
    public GameObject GameOverUI;
    public Text LevelUI;
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
    public bool redDiamondVisibility = false;
    public bool redDiamondVisibilityLevel1 = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if(SaveSystem.doLoadFromFile == true)
        {
            LoadPLayer();
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("findRedDiamond").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("findRedDiamond")[i].SetActive(redDiamondVisibilityLevel1);
                Destroy(GameObject.FindGameObjectsWithTag("findRedDiamond")[i]);
            }
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("redDiamond").Length; i++)
                GameObject.FindGameObjectsWithTag("redDiamond")[i].SetActive(redDiamondVisibility);
            SaveSystem.doLoadFromFile = false;
        }
        else
        {
            MaxHealth = MaxHealth * Level;
            CurrentHealth = MaxHealth;
            Level = 1;
            Money = 100;
        }


        LevelUI.text = Level.ToString();
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

        if(Input.GetKeyDown(KeyCode.Q) && canOpenShop == true)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            TakeDamage(50.0f);
        }

        if (other.gameObject.CompareTag("skeletonmonster"))
        {
            TakeDamage(5.0f);
        }

        if(other.gameObject.CompareTag("sword"))
        {
            TakeDamage(30.0f);
        }

        if(other.gameObject.CompareTag("lava"))
        {
            TakeDamage(CurrentHealth);
        }

        if (other.gameObject.CompareTag("skull"))
        {
            TakeDamage(100.0f);
        }

        if(other.gameObject.CompareTag("market"))
        {
            canOpenShop = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("market"))
        {
            canOpenShop = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            TakeDamage(50.0f);
        }

        if (collision.gameObject.CompareTag("fireBall"))
        {
            TakeDamage(40.0f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("iceBall"))
        {
            TakeDamage(30.0f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("skull"))
        {
            TakeDamage(100.0f);
        }

        if (collision.gameObject.CompareTag("findRedDiamond"))
        {
            Level += 1;
            LevelUI.text = Level.ToString();
            Money += 300;
            MaxHealth += 100;
            CurrentHealth = MaxHealth;
            healthBar.SetMaxHealth(MaxHealth);
            healthBar.SetHealth(CurrentHealth);
            moneyInShop.text = Money.ToString();
            SetMoneyText();
            if (damageProtection > 0.4)
                damageProtection -= 0.1f;
            redDiamondVisibilityLevel1 = false;
            redDiamondVisibility = true;
            collision.gameObject.SetActive(redDiamondVisibilityLevel1);
            Destroy(collision.gameObject);
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("redDiamond").Length; i++)
                GameObject.FindGameObjectsWithTag("redDiamond")[i].SetActive(redDiamondVisibility);
        }

        if (collision.gameObject.CompareTag("sword"))
        {
            TakeDamage(30.0f);
        }

        if (collision.gameObject.CompareTag("lava"))
        {
            TakeDamage(CurrentHealth);
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
        redDiamondVisibility = data.RedDiamondVisibility;
        redDiamondVisibilityLevel1 = data.RedDiamondVisibilityLevel1;
        Vector3 position;
        position.x = data.Position[0];
        position.y = data.Position[1];
        position.z = data.Position[2];
        transform.position = position;
    }

    public void LoadPlayerAfterDeath()
    {
        LoadPLayer();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("findRedDiamond").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("findRedDiamond")[i].SetActive(redDiamondVisibilityLevel1);
            //Destroy(GameObject.FindGameObjectsWithTag("findRedDiamond")[i]);
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("redDiamond").Length; i++)
            GameObject.FindGameObjectsWithTag("redDiamond")[i].SetActive(redDiamondVisibility);
        potionsAmount.text = amountOfHealthPotion.ToString();
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(CurrentHealth);
        SetMoneyText();
        armourPriceText.text = armourPrice.ToString();
        moneyInShop.text = Money.ToString();
    }

    #endregion

    #region Other

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage*damageProtection;
        if (CurrentHealth < 0)
            CurrentHealth = 0;
        healthBar.SetHealth(CurrentHealth);

        if(CurrentHealth == 0)
        {
            GameOverUI.SetActive(true);
        }
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
        Time.timeScale = 1f;
    }

    void PauseShop()
    {
        moneyInShop.text = Money.ToString();
        ShopUI.SetActive(true);
        isShopOpen = true;
        Time.timeScale = 0f;
    }

    void ResumeInventory()
    {
        InventoryUI.SetActive(false);
        Time.timeScale = 1f;
        isInventoryOpen = false;
    }

    void PauseInventory()
    {
        potionAmountInventory.text = amountOfHealthPotion.ToString();
        InventoryUI.SetActive(true);
        isInventoryOpen = true;
        Time.timeScale = 0f;
    }

    #endregion
}
