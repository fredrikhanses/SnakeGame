using UnityEngine;
using UnityEngine.UI;

public class UIDisplayTimeHighscore : MonoBehaviour
{
    public Text text = null;

    private void Awake()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
    }

    public void DisplayTimeHighscore(int highscore)
    {
        if (highscore < 10)
        {
            text.text = $"{0}{0}{highscore}";
        }
        else if (highscore < 100)
        {
            text.text = $"{0}{highscore}";
        }
        else
        {
            text.text = $"{highscore}";
        }
    }
}