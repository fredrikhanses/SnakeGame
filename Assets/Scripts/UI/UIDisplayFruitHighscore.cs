using UnityEngine;
using UnityEngine.UI;

public class UIDisplayFruitHighscore : MonoBehaviour
{
    public Text text = null;

    private void Awake()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
    }

    public void DisplayFruitHighscore(int highscore)
    {
        if (highscore < 10)
        {
            text.text = $"{0}{highscore}";
        }
        else
        {
            text.text = $"{highscore}";
        }
    }
}