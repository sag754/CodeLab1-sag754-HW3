using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text infoText;
    public Text infoTime;
    public Text infoRecord;

    private float timer = 0; //keep track of time
    private int coins = 0;

    private const string PLAY_PREF_KEY_CS = "Coins: ";
    private const string PLAY_PREF_KEY_TM = "Time: ";

    private const string FILE_CS = "/Fantasy-Game-records.txt";
    private const string FILE_ALL_RECORDS = "/All_records.txt";

    //Property
    public int Coins
    {
        get
        {
            return coins;
        }
        set
        {
            coins = value;
            if (coins > mostCoins)
            {
                mostCoins = coins;
            }
        }
    }

    private int mostCoins = 0;

    private int MostCoins
    {
        get
        {
            return mostCoins;
        }
        set
        {
            mostCoins = value;
            //Save it somewhere
            //PlayerPrefs.SetInt(PLAY_PREF_KEY_HS, highScore);
            File.WriteAllText(Application.dataPath + FILE_CS, mostCoins + "");


            allRecords.Add(mostCoins);

            string allRecordsString = "";
            for (int i = 0; i < allRecords.Count; i++)
            {
                allRecordsString = allRecordsString + allRecords[i] + ",";
            }
            File.WriteAllText(Application.dataPath + FILE_ALL_RECORDS, allRecordsString);
        }
    }

    private List<int> allRecords = new List<int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath);

        infoText = GetComponentInChildren<Text>();
        infoTime = GetComponentInChildren<Text>();
        infoRecord = GetComponentInChildren<Text>();

        if (File.Exists(Application.dataPath + FILE_CS))
        {
            string csString = File.ReadAllText(Application.dataPath + FILE_CS);

            print(csString);
            string[] splitString = csString.Split(',');
            mostCoins = int.Parse(splitString[0]);

            for (int i = 0; i < splitString.Length; i++)
            {
                print(splitString[i]);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        infoText.text = "Coins x " + PlayerController.instance.coins;
        infoTime.text = "Time: " + (int)timer;
        infoRecord.text = "Best Time: " + mostCoins;
    }
}
