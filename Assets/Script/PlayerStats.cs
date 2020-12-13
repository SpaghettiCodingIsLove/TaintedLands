using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBar;
    public Text moneyAmount;
    public Text moneyInShop;
    public Button buyHealth;
    public int Money = 100;
    public static int Level = 1;
    public int CurrentHealth;
    public int MaxHealth = 100;
    public static bool isShopOpen = false;
    public static bool canOpenShop = false;
    public GameObject ShopUI;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = MaxHealth* Level;
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        SetMoneyText();
        moneyInShop.text = Money.ToString();
        buyHealth.onClick.AddListener(delegate () {
            BuyHealth();
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
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        healthBar.SetHealth(CurrentHealth);
    }


    void SetMoneyText()
    {
        moneyAmount.text = Money.ToString();
    }

    void BuyHealth()
    {
        if(Money >= 50)
        {
            Money -= 50;
            SetMoneyText();
            if (CurrentHealth + 50 >= MaxHealth)
                CurrentHealth = MaxHealth;
            else
            {
                CurrentHealth += CurrentHealth;
                healthBar.SetHealth(CurrentHealth);
            }
            moneyInShop.text = Money.ToString();
        }
    }

    void Resume()
    {
        ShopUI.SetActive(false);
        isShopOpen = false;
    }

    void Pause()
    {
        moneyInShop.text = Money.ToString();
        ShopUI.SetActive(true);
        isShopOpen = true;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "market")
        {
            canOpenShop = true;
        }
        else
            canOpenShop = false;
    }
}
