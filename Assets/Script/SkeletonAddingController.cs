using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkeletonAddingController : MonoBehaviour
{

    public GameObject skeletonMonster;
    public GameObject male;
    public GameObject female;

    public Text XPositionText;
    public Text YPositionText;
    public Text ZPositionText;

    
    public Button FastTravellToVillage;
    public Button FastTravellToVillage2;
    public Button GoToMenuAfterEnding;
    
    Transform target;
    // Start is called before the first frame update
    void Start()
    {

        FastTravellToVillage.onClick.AddListener(delegate () {
            PlayerManager.instance.player.SetActive(false);
            PlayerManager.instance.player.transform.position = new Vector3(829.6f, 160f, 2807.6f);
            PlayerManager.instance.player.SetActive(true);
        });

        FastTravellToVillage2.onClick.AddListener(delegate () {
            PlayerManager.instance.player.SetActive(false);
            PlayerManager.instance.player.transform.position = new Vector3(2666.1f, 160f, 766.7f);
            PlayerManager.instance.player.SetActive(true);
        });

        GoToMenuAfterEnding.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        });

        target = PlayerManager.instance.player.transform;

        XPositionText.text = target.position.x.ToString();
        YPositionText.text = target.position.y.ToString();
        ZPositionText.text = target.position.z.ToString();

        for (int i = 0; i < 50; i++)
        {
            CreateSkeletonMonster();
        }

        for (int i = 0; i < 50; i++)
        {
            CreateSkeletonMonster2();
        }

        for (int i = 0; i < 25; i++)
        {
            CreateMaleCitizen();
        }

        for(int i = 0; i < 25; i++)
        {
            CreateFemaleCitizen();
        }

        for (int i = 0; i < 20; i++)
        {
            CreateMaleCitizen2();
        }

        for (int i = 0; i < 20; i++)
        {
            CreateFemaleCitizen2();
        }
    }

    void Update()
    {
        XPositionText.text = target.position.x.ToString();
        YPositionText.text = target.position.y.ToString();
        ZPositionText.text = target.position.z.ToString();
    }

    void CreateSkeletonMonster()
    {
        Vector3 vector = new Vector3(Random.Range(1769.0f, 3100.0f), 170.0f, Random.Range(1411.0f, 3000.0f));
        Instantiate(skeletonMonster, vector, skeletonMonster.transform.rotation);
    }

    void CreateSkeletonMonster2()
    {
        Vector3 vector = new Vector3(Random.Range(880.0f, 2201.0f), 170.0f, Random.Range(1173.0f, 3116.0f));
        Instantiate(skeletonMonster, vector, skeletonMonster.transform.rotation);
    }

    void CreateMaleCitizen()
    {
        Vector3 vector = new Vector3(Random.Range(2553.0f, 2785.0f), 100.0f, Random.Range(501.0f, 900.0f));
        Instantiate(male, vector, male.transform.rotation);
    }

    void CreateFemaleCitizen()
    {
        Vector3 vector = new Vector3(Random.Range(2553.0f, 2785.0f), 100.0f, Random.Range(501.0f, 900.0f));
        Instantiate(female, vector, female.transform.rotation);
    }

    void CreateMaleCitizen2()
    {
        Vector3 vector = new Vector3(Random.Range(835.0f, 1003.0f), 136.0f, Random.Range(2728.0f, 2874.0f));
        Instantiate(male, vector, male.transform.rotation);
    }

    void CreateFemaleCitizen2()
    {
        Vector3 vector = new Vector3(Random.Range(835.0f, 1003.0f), 136.0f, Random.Range(2728.0f, 2874.0f));
        Instantiate(female, vector, female.transform.rotation);
    }
}
