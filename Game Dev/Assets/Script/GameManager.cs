using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public List<Transform> escapePoints;
    public int requiredRunes = 15;

    public int runeCount = 0;
    public Transform activeEscapePoint;
    public Image fadeImage;
    public Button myButton;
    public Text myText;

    public Canvas targetCanvas;
    public Canvas fadeCanvas;
    public GameObject endGameUI;

    private float fadeSpeed = 1f;
    private bool isFading = false;
    private float escapeTriggerDistance = 2f;

    private void Start()
    {
        foreach (var escapePoint in escapePoints)
        {
            if (escapePoint != null)
            {
                escapePoint.gameObject.SetActive(false);
            }
        }

        if (endGameUI != null)
        {
            endGameUI.SetActive(false);
        }

        SetupFadeImage();
        SetupUIElements();
    }

    private void SetupFadeImage()
    {
        if (fadeImage == null)
        {
            GameObject fadeImageObject = new GameObject("FadeImage");
            fadeImage = fadeImageObject.AddComponent<Image>();
            fadeImage.color = Color.clear;
            fadeImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

            if (fadeCanvas == null)
            {
                Debug.LogError("Fade Canvas is not assigned.");
                return;
            }

            fadeImage.transform.SetParent(fadeCanvas.transform, false);
            fadeImage.rectTransform.SetAsLastSibling();  // Ensure it's the last element in the Canvas

            Canvas canvas = fadeImage.GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = fadeImage.gameObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            canvas.sortingOrder = 0;  // Ensure it's behind other UI elements
        }
    }

    private void SetupUIElements()
    {
        if (targetCanvas == null)
        {
            Debug.LogError("Target Canvas is not assigned.");
            return;
        }

        if (myButton != null)
        {
            myButton.transform.SetParent(targetCanvas.transform, false);
            myButton.gameObject.SetActive(false);

            RectTransform buttonRectTransform = myButton.GetComponent<RectTransform>();
            buttonRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            buttonRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            buttonRectTransform.pivot = new Vector2(0.5f, 0.5f);
            buttonRectTransform.anchoredPosition = new Vector2(0, 0);
        }

        if (myText != null)
        {
            myText.transform.SetParent(targetCanvas.transform, false);
            myText.gameObject.SetActive(false);

            RectTransform textRectTransform = myText.GetComponent<RectTransform>();
            textRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            textRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            textRectTransform.pivot = new Vector2(0.5f, 0.5f);
            textRectTransform.anchoredPosition = new Vector2(0, 50);
        }
    }

    private void Update()
    {
        if (runeCount >= requiredRunes && activeEscapePoint == null)
        {
            ActivateEscapePoint();
        }

        if (isFading)
        {
            PerformFadeToWhite();
        }

        if (activeEscapePoint != null)
        {
            float distanceToEscapePoint = Vector3.Distance(player.position, activeEscapePoint.position);
            if (distanceToEscapePoint <= escapeTriggerDistance)
            {
                if (!isFading)
                {
                    StartFadeToWhite();
                }
            }
        }
    }

    public void IncrementRuneCount()
    {
        runeCount++;
    }

    public int GetRuneCount()
    {
        return runeCount;
    }

    private void ActivateEscapePoint()
    {
        if (escapePoints.Count > 0)
        {
            int randomIndex = Random.Range(0, escapePoints.Count);
            activeEscapePoint = escapePoints[randomIndex];
            if (activeEscapePoint != null)
            {
                activeEscapePoint.gameObject.SetActive(true);
            }
        }
    }

    private void StartFadeToWhite()
    {
        if (!isFading)
        {
            isFading = true;
        }
    }

    private void PerformFadeToWhite()
    {
        if (fadeImage != null)
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Color.white, fadeSpeed * Time.deltaTime);

            if (fadeImage.color.a >= 0.99f)
            {
                fadeImage.color = Color.white;
                isFading = false;

                if (endGameUI != null)
                {
                    endGameUI.SetActive(true);
                }

                if (myButton != null)
                {
                    myButton.gameObject.SetActive(true);
                }

                if (myText != null)
                {
                    myText.gameObject.SetActive(true);
                }

                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (player != null && activeEscapePoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(activeEscapePoint.position, escapeTriggerDistance);
        }
    }
}
