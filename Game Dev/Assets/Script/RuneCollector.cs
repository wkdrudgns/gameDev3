using System.Collections.Generic;
using UnityEngine;

public class RuneCollector : MonoBehaviour
{
    public Transform player;
    public float collectionRadius = 2f;
    public List<GameObject> runes;
    public AudioSource audioSource;
    public GameManager gameManager;

    private void Start()
    {
        runes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Rune"));
        audioSource = GetComponent<AudioSource>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is missing in RuneCollector.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CollectRune();
        }
    }

    private void CollectRune()
    {
        bool runeCollected = false;

        for (int i = runes.Count - 1; i >= 0; i--)
        {
            var rune = runes[i];

            if (rune != null && Vector3.Distance(player.position, rune.transform.position) <= collectionRadius)
            {
                rune.SetActive(false);
                runes.RemoveAt(i);
                gameManager.IncrementRuneCount();
                runeCollected = true;
                Debug.Log("Collected a rune. Total runes: " + gameManager.GetRuneCount());

                if (audioSource != null)
                {
                    audioSource.Play();
                }
                else
                {
                    Debug.LogWarning("AudioSource is not assigned.");
                }

                break;
            }
        }

        if (!runeCollected)
        {
            Debug.Log("No rune collected in the vicinity.");
        }
    }
}
