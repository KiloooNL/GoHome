using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    //Count
    public int currentScore;
    public int highScore;
    public int money;
    public int coinCount;
    private int totalCoinCount;

    // Levels
    public int currentLevel = 1;
    public int unlockedLevel;

    // Init GUI Skin
    public GUISkin skin;
    public Rect timerRect;

    // Timer variables
    public float startTime;
    private string currentTime;
    public Color timerWarningColor;
    public Color defaultTimerColor;

    // References
    public GameObject coinParent;



    // dont destroy on new scene load
    void Start()
    {
        // Total coin = current coin count.
        // NOTE: This will display an int of all coins on level
        //       Useful for debugging, also shows a score of 
        //       COLLECTED out of COINS ON LEVEL (eg. To start with: 0/10).
        totalCoinCount = coinParent.transform.childCount;

        /**********************************************************
        ** TODO: 
        *  - Add a feature where collecting all coins on map
        *    shows a message or plays a sound.
        *  - Maybe change the coin GUI font to reflect this as well.
        **********************************************************/

        /*********************
        * Get Level Progress *
        *********************/
        if(PlayerPrefs.GetInt("Level Completed") > 0)
        {
            currentLevel = PlayerPrefs.GetInt("Level Completed");
        } else
        {
            currentLevel = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        startTime -= Time.deltaTime;
        currentTime = string.Format("{0:0.0}", startTime);

        // Add function:
        // if (countCount == totalCoinCount) {
        //      rewardPlayer();
        // }
        

        if (startTime <= 0)
        {
            startTime = 0;
            print("You're out of time.");

            if (SceneManager.GetActiveScene().name != "main_menu")
            {
                // clear timer, etc. load main menu
                Destroy(gameObject);
                LoadMainMenu();
            }
        }
    }

    public void LoadMainMenu()
    {
        print("Loading main menu...");
        SceneManager.LoadScene("main_menu");
    }

    public void CompleteLevel()
    {

        if(currentLevel < 3)
        {
            // Player reached the end
            print("Player reached end of level.");
            currentLevel++;
            PlayerPrefs.SetInt("Level Completed", currentLevel);
            PlayerPrefs.SetInt("Level " + currentLevel.ToString() + " score", currentScore);


            print("Loading next level: Level " + currentLevel);
            SceneManager.LoadScene(currentLevel);
        } else
        {
            // If no more levels, failsafe, load main menu.
            print("No more levels?\nFailsafe: Loading main menu...");
            LoadMainMenu();
        }
    }

    public void AddCoin()
    {
        coinCount++;
        // add sound here...
    }


    void OnGUI()
    {
        GUI.skin = skin;
        if(startTime < 10f)
        {
            skin.GetStyle("Timer Label").normal.textColor = timerWarningColor;
        } else
        {
            skin.GetStyle("Timer Label").normal.textColor = defaultTimerColor;
        }

        // Coins
        GUI.Label(new Rect(45,100,200,200), "Coins: " + coinCount.ToString() + "/" + totalCoinCount.ToString(), skin.GetStyle("Coin"));

        // Timer
        GUI.Label(timerRect, "Time: " + currentTime, skin.GetStyle("Timer Label"));
    }
}
