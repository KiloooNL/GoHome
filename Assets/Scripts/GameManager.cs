using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static int currentScore;
    public static int highScore;
    public static int money;

    public static int currentLevel = 1;
    public static int unlockedLevel;

    // Init GUI Skin
    public GUISkin skin;
    public Rect timerRect;

    // Timer
    public float startTime;
    private string currentTime;
    public Color timerWarningColor;
    public Color defaultTimerColor;

    // dont destroy on new scene load
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        // Check if timer should be enabled
        // based on menu select button
        if(MainMenu.timerEnabled == true)
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

    public static void LoadMainMenu()
    {
        print("Loading main menu...");
        MainMenu.timerEnabled = false;
        SceneManager.LoadScene("main_menu");
    }

    public static void CompleteLevel()
    {
        // Player reached the end
        print("Player reached end of level.");
        currentLevel++;
        print("Loading next level: Level " + currentLevel);

        //SceneManager.LoadScene("Level " + currentLevel);
        // for now just load main_menu
        print("You win!");
        LoadMainMenu();
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

        GUI.Label(timerRect, currentTime, skin.GetStyle("Timer Label"));
    }
}
