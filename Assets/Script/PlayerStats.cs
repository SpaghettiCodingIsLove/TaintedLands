using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Text NewLevelText;
    public GameObject NewLevelUI;
    public GameObject FinalBoss;
    public GameObject GameEndingPanel;
    public Button StartGame;
    public GameObject GameStartingPanel;
    public GameObject RedDiamond;
    public GameObject YellowDiamond;
    public GameObject GreenDiamond;
    public GameObject BlueDiamond;
    #endregion

    #region player stats variables
    public int[] expNeeded;
    public int currentExp = 0;
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
    public bool redDiamondFound = false;
    public bool greenDiamondFound = false;
    public bool yellowDiamondFound = false;
    public bool blueDiamondFound = false;


    private float timeToStopHealling = 0f;

    public int NumOfKills = 0;
    [SerializeField]
    public int NumOfFoundDiamonds = 0;

    public ParticleSystem Healling;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartGame.onClick.AddListener(delegate () {
            GameStartingPanel.SetActive(false);
        });

        Debug.Log("START " + SaveSystem.doLoadFromFile.ToString());
        if(SaveSystem.doLoadFromFile == true)
        {
            Debug.Log("ODCZYT");
            LoadPLayer();
            SaveSystem.doLoadFromFile = false;
            Debug.Log("START " + SaveSystem.doLoadFromFile.ToString());
        }
        else
        {
            GameStartingPanel.SetActive(true);
            MaxHealth = MaxHealth * Level;
            CurrentHealth = MaxHealth;
            Level = 1;
            Money = 100;
            currentExp = 0;
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
        if(currentExp >= expNeeded[Level])
        {
            Level++;
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

            NewLevelText.text = $"You have reached new level {Level}.";
            NewLevelUI.SetActive(true);
        }

        if(SaveSystem.isFinallBossDead == true)
        {
            GameEndingPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Time.time - timeToStopHealling > 1)
        {
            Healling.Stop();
        }


        if(Input.GetKeyDown(KeyCode.L))
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

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (amountOfHealthPotion > 0)
            {
                Healling.Play();
                if (CurrentHealth + 50 >= MaxHealth)
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
                timeToStopHealling = Time.time;
            }
        }
    }

   
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("sword"))
        {
            TakeDamage(30.0f);
        }

        if (collision.gameObject.CompareTag("bullet"))
        {
            TakeDamage(50.0f);
        }

        if (collision.gameObject.CompareTag("fireBall"))
        {
            TakeDamage(30.0f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("iceBall"))
        {
            TakeDamage(30.0f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("skull"))
        {
            TakeDamage(20.0f);
        }

        if (collision.gameObject.CompareTag("skeletonmonster"))
        {
            TakeDamage(10.0f);
        }

        if (collision.gameObject.CompareTag("sword"))
        {
            TakeDamage(30.0f);
        }

        if (collision.gameObject.CompareTag("wizard"))
        {
            TakeDamage(30.0f);
        }

        if (collision.gameObject.CompareTag("redDiamond"))
        {
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.position = new Vector3(2690.2f, 106.5f, 836f);
            collision.gameObject.SetActive(true);
            redDiamondFound = true;
            diamondWasFound();
        }

        if (collision.gameObject.CompareTag("blueDiamond"))
        {
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.position = new Vector3(2647.3f, 106.5f, 835.76f);
            collision.gameObject.SetActive(true);
            redDiamondFound = true;
            diamondWasFound();
        }

        if (collision.gameObject.CompareTag("yellowDiamond"))
        {
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.position = new Vector3(2647.3f, 106.5f, 791.33f);
            collision.gameObject.SetActive(true);
            redDiamondFound = true;
            diamondWasFound();
        }

        if (collision.gameObject.CompareTag("greenDiamond"))
        {
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.position = new Vector3(2690.1f, 106.5f, 792.76f);
            collision.gameObject.SetActive(true);
            redDiamondFound = true;
            diamondWasFound();
        }
    }

    private void diamondWasFound()
    {
        Level += 1;
        LevelUI.text = Level.ToString();
        Money += 300;
        MaxHealth += 100;
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(CurrentHealth);
        moneyInShop.text = Money.ToString();
        NumOfFoundDiamonds++;
        SetMoneyText();
        if (damageProtection > 0.4)
            damageProtection -= 0.1f;

        if (NumOfFoundDiamonds < 4)
            NewLevelText.text = $"You have reached level {Level}.\nYou have to find {4 - NumOfFoundDiamonds} more diamonds to save our lands.\n Keep going, we believe in you!!!";
        else
            NewLevelText.text = $"You found all of the stolen diamonds.\nNow you'll have to fight your last battle.";
        NewLevelUI.SetActive(true);
        if (NumOfFoundDiamonds < 4)
        {
            PlayerManager.instance.player.SetActive(false);
            PlayerManager.instance.player.transform.position = new Vector3(2666.1f, 103f, 766.7f);
            PlayerManager.instance.player.SetActive(true);
        }
        else
        {
            PlayerManager.instance.player.SetActive(false);
            PlayerManager.instance.player.transform.position = new Vector3(900.9f, 133f, 1060f);
            PlayerManager.instance.player.SetActive(true);
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
        currentExp = data.currentExp;
        Money = data.Money;
        Level = data.Level;
        MaxHealth = data.MaxHealth;
        CurrentHealth = data.CurrentHealth;
        armourPrice = data.ArmourPrice;
        damageProtection = data.DamageProtection;
        amountOfHealthPotion = data.AmountOfHealthPotion;
        NumOfKills = data.NumOfKills;
        NumOfFoundDiamonds = data.NumOfFoundDiamonds;
        Vector3 position;
        redDiamondFound = data.redDiamondFound;
        blueDiamondFound = data.blueDiamondFound;
        yellowDiamondFound = data.yellowDiamondFound;
        greenDiamondFound = data.greenDiamondFound;
        position.x = data.Position[0];
        position.y = data.Position[1];
        position.z = data.Position[2];
        transform.position = position;

        if(redDiamondFound)
        {
            RedDiamond.SetActive(false);
            RedDiamond.transform.position = new Vector3(2690.2f, 106.5f, 836f);
            RedDiamond.SetActive(true);
        }

        if(blueDiamondFound)
        {
            BlueDiamond.SetActive(false);
            BlueDiamond.transform.position = new Vector3(2647.3f, 106.5f, 835.76f);
            BlueDiamond.SetActive(true);
        }

        if(yellowDiamondFound)
        {
            YellowDiamond.SetActive(false);
            YellowDiamond.transform.position = new Vector3(2647.3f, 106.5f, 791.33f);
            YellowDiamond.SetActive(true);
        }

        if(greenDiamondFound)
        {
            GreenDiamond.SetActive(false);
            GreenDiamond.transform.position = new Vector3(2690.1f, 106.5f, 792.76f);
            GreenDiamond.SetActive(true);
        }
    }

    public void LoadPlayerAfterDeath()
    {
        SaveSystem.doLoadFromFile = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public void AddMoney(int m)
    {
        Money += m;
        moneyAmount.text = Money.ToString();
        moneyInShop.text = Money.ToString();
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
