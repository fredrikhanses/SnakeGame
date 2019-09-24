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
    public int winSceneIndex;
    public int loseSceneIndex;
    private int fruitsInPlay;
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

    public void OnFruitAdded()
    {
        fruitsInPlay++;
        onFruitsChanged.Invoke(fruitsInPlay);
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
