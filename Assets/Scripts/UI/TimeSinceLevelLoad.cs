using UnityEngine;
using UnityEngine.UI;

public class TimeSinceLevelLoad : MonoBehaviour
{
    public Text time = null;
    public static float seconds;

    private void Awake()
    {
        if(time == null)
        {
            time = GetComponent<Text>();
        }
    }

    void FixedUpdate()
    {
        seconds = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        if(seconds < 10)
        {
            time.text = $"{0}{0}{seconds}";
        }
        else if(seconds < 100)
        {
            time.text = $"{0}{seconds}";
        }
        else
        {
            time.text = $"{seconds}";
        }
    }
}