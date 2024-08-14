using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HavingRune : MonoBehaviour
{
    GameManager gm;
    public TextMeshProUGUI statText;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm == null)
        {
            Debug.LogError("GameManager not found in the scene.");
            return;
        }

        gm.runeCount = 0;

        if (statText == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned.");
        }
    }

    private void Update()
    {
        if (statText != null && gm != null)
        {
            statText.text = ": " + gm.runeCount.ToString() + " / " + gm.requiredRunes.ToString();
        }
    }
}
