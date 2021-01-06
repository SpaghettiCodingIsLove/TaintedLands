using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsMenager : MonoBehaviour
{
    public int numOfKills = 0;
    public int numOfFoundDiamonds = 0;

    public Text isComplited1;
    public Text isComplited2;
    public Text isComplited3;
    public Text isComplited4;

    public Text numOfKillsUI1;
    public Text numOfKillsUI2;
    public Text numOfKillsUI3;
    public Text numOfFoundDiamondsUI;

    public GameObject AchievemntsPanel;

    public Button openAchievementsPanel;

    void Start()
    {
        openAchievementsPanel.onClick.AddListener(delegate () {
            if (numOfKills < 10)
            {
                numOfKillsUI1.text = numOfKills.ToString();
                isComplited1.text = "NO";
            }
            else
            {
                isComplited1.text = "YES";
                numOfKillsUI1.text = "10";
            }

            if (numOfKills < 25)
            {
                numOfKillsUI2.text = numOfKills.ToString();
                isComplited2.text = "NO";
            }
            else
            {
                isComplited2.text = "YES";
                numOfKillsUI2.text = "25";
            }

            if (numOfKills < 50)
            {
                numOfKillsUI3.text = numOfKills.ToString();
                isComplited3.text = "NO";
            }
            else
            {
                isComplited3.text = "YES";
                numOfKillsUI3.text = "50";
            }

            numOfFoundDiamondsUI.text = numOfFoundDiamonds.ToString();
            if (numOfFoundDiamonds < 4)
                isComplited4.text = "NO";
            else
                isComplited4.text = "YES";

            AchievemntsPanel.SetActive(true);
        });
    }
}
