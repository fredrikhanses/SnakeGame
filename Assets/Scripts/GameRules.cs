using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameRulesIntEvent : UnityEvent<int>
{

}

public class GameRules : MonoBehaviour
{
    public static GameRules instance;
    public GameRulesIntEvent onFruitsChanged;
    public GameRulesIntEvent onFruitHighscoreChanged;
    public GameRulesIntEvent onTimeHighscoreChanged;
    public int winSceneIndex;
    public int loseSceneIndex;
    private int fruitHighscore;
    private int timeHighscore;
    private int fruitsEaten;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fruitHighscore = PlayerPrefs.GetInt("fruitHighscore");
        timeHighscore = PlayerPrefs.GetInt("timeHighscore");
        onFruitHighscoreChanged.Invoke(fruitHighscore);
        onTimeHighscoreChanged.Invoke(timeHighscore);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("fruitHighscore", fruitHighscore);
        PlayerPrefs.SetInt("timeHighscore", timeHighscore);
        PlayerPrefs.Save();
    }

    public void UpdateFruitHighscore()
    {
        onFruitHighscoreChanged.Invoke(fruitHighscore);
    }

    public void UpdateTimeHighscore()
    {
        onTimeHighscoreChanged.Invoke(timeHighscore);
    }

    public void OnFruitAdded()
    {
        fruitsEaten++;
        onFruitsChanged.Invoke(fruitsEaten);
        if(fruitsEaten > fruitHighscore)
        {
            fruitHighscore = fruitsEaten;
            timeHighscore = Mathf.RoundToInt(Time.timeSinceLevelLoad);
            UpdateFruitHighscore();
            UpdateTimeHighscore();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(loseSceneIndex);
    }

    public void GameWin()
    {
        SceneManager.LoadScene(winSceneIndex);
    }
}