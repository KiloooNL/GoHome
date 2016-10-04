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
    private Rect timerRect = new Rect(445, 5, 0, 0);
    private Rect coinRect = new Rect(695, 5, 0, 0);

    // Timer variables
    public float startTime;
    private string currentTime;
    public Color timerWarningColor;
    public Color defaultTimerColor;

    // References
    public GameObject coinParent;

    // Booleans
    private bool showWinScreen = false;
    private bool completed;

    public int winScreenWidth, winScreenHeight;

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

        // Add function:
        // if (countCount == totalCoinCount) {
        //      rewardPlayer();
        // }

        if (!completed)
        {
            startTime -= Time.deltaTime;
            currentTime = string.Format("{0:0.0}", startTime);

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
    }

    public void LoadMainMenu()
    {
        print("Loading main menu...");
        SceneManager.LoadScene("main_menu");
    }

    public void CompleteLevel()
    {
        showWinScreen = true;
        completed = true;
    }

    void LoadNextLevel()
    {

        if (currentLevel < 3)
        {
            // Player reached the end
            print("Player reached end of level.");
            currentLevel++;

            SaveGame();
            print("Loading next level: Level " + currentLevel);
            LoadNextLevel();
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            // If no more levels, failsafe, load main menu.
            print("No more levels?\nFailsafe: Loading main menu...");
            LoadMainMenu();
        }
    }

    void SaveGame()
    {
        print("Saving game...");
        PlayerPrefs.SetInt("Level Completed", currentLevel);
        PlayerPrefs.SetInt("Level " + currentLevel.ToString() + " score", currentScore);
    }

    public void AddCoin()
    {
        coinCount++;
        // add sound here...
    }


    void OnGUI()
    {
        GUI.skin = skin;

        // Timer warning
        if (startTime < 10f)
        {
            skin.GetStyle("Timer Label").normal.textColor = timerWarningColor;
        } else
        {
            skin.GetStyle("Timer Label").normal.textColor = defaultTimerColor;
        }
        // Coins
        GUI.Label(coinRect, "Coins: " + coinCount.ToString() + "/" + totalCoinCount.ToString(), skin.GetStyle("Coin"));

        // Timer
        GUI.Label(timerRect, "Time: " + currentTime, skin.GetStyle("Timer Label"));

        if(showWinScreen)
        {
            // Center to screen
            // additionally:
            // to centre and adjust based on screen size automatically:
            // Rect(screenX, screenY, winBoxWidth, winBoxHeight); 
            float screenX = Screen.width / 2 - (Screen.width * 0.5f / 2);
            float screenY = Screen.height / 2 - (Screen.height * 0.5f / 2);
            float winBoxWidth = Screen.width * 0.5f;
            float winBoxHeight = Screen.height * 0.5f;
            
            Rect winScreenRect = new Rect(Screen.width / 2 - (winScreenWidth/2), Screen.height/2 - (winScreenHeight/2), winScreenWidth, winScreenHeight);
            GUI.Box(winScreenRect, "Level completed!");

            // Get Level Score
            int finTime = (int)startTime;
            currentScore = coinCount * finTime;

            if (GUI.Button(new Rect(winScreenRect.x + 20, winScreenRect.y + winScreenRect.height - 60, 150, 40), "Continue"))
            {
                LoadNextLevel();
            }


            if (GUI.Button(new Rect(winScreenRect.x + winScreenRect.width - 170, winScreenRect.y + winScreenRect.height - 60, 150, 40), "Quit"))
            {
                LoadMainMenu();
            }

            // TODO:
            // Extra features?
            // Maybe add a star rating based on how the player did the level.
            // eg 3 stars = all coins collected + end time is < xx

            // Print score
            GUI.Label(new Rect(winScreenRect.x + 20, winScreenRect.y + 40, 300, 50), "Completed level " + currentLevel + 1, "levelScore");
            GUI.Label(new Rect(winScreenRect.x + 20, winScreenRect.y + 90, 300, 50), "Level Score: " + currentScore.ToString(), "levelScore");


        }
    }
}
