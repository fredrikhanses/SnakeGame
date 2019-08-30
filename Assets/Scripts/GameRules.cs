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
    public int extraLifeForEveryFruitCollected = 3;

    public int winSceneIndex;
    public int loseSceneIndex;

    public int playerLife;
    private int fruitsInPlay;
    private int fruitsCollected;

    public GameRulesIntEvent playerLifeChanged;
    public GameRulesIntEvent onFruitsChanged;

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

    public void OnPlayerLifeAdded()
    {
        playerLife++;
        playerLifeChanged.Invoke(playerLife);
    }

    public void OnPlayerLifeRemoved()
    {
        playerLife--;
        playerLifeChanged.Invoke(playerLife);
        if (playerLife <= 0)
        {
            GameOver();
        }
    }

    public void OnFruitAdded()
    {
        fruitsCollected++;
        fruitsInPlay++;
        onFruitsChanged.Invoke(fruitsInPlay);

        if (fruitsCollected == extraLifeForEveryFruitCollected)
        {
            fruitsCollected = 0;
            OnPlayerLifeAdded();
        }
    }

    public void OnFruitRemoved()
    {
        fruitsInPlay--;
        onFruitsChanged.Invoke(fruitsInPlay);
        if (fruitsInPlay <= 0)
        {
            GameWin();
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
