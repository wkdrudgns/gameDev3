using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaowSay : MonoBehaviour
{
    public float DarkTime = 0f;
    public float messageDisplayDuration = 0.5f;
    MapOpen mapOpen;

    public TextMeshProUGUI sayText;

    private void Start()
    {
        mapOpen = GetComponent<MapOpen>();

        if (sayText == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned.");
        }
    }

    private void Update()
    {
        if (!mapOpen.OnMap)
        {
            DarkTime += Time.deltaTime;

            if (DarkTime >= messageDisplayDuration)
            {
                Say();
            }
        }
        else
        {
            ClearText();
            DarkTime = 0f;
        }

        if (DarkTime > 0f && DarkTime >= messageDisplayDuration)
        {
            ClearText();
        }
    }

    private void Say()
    {
        if (sayText != null)
        {
            sayText.text = "It's too dark. I need to open the map...";
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }

    private void ClearText()
    {
        if (sayText != null)
        {
            sayText.text = "";
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }
}
