using UnityEngine;
using UnityEngine.UI;

public class UISetTextFromInt : MonoBehaviour
{
    public Text text = null;

    private void Awake()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
    }

    public void SetTextFromInt(int value)
    {
        text.text = value.ToString().PadLeft(2, '0');
    }
}
