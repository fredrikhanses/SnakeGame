using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public Text highscore = null;
    private float seconds;

    private void Awake()
    {
        if (highscore == null)
        {
            highscore = GetComponent<Text>();
        }
    }
}