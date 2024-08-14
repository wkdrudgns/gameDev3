using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public float timerDuration = 60f;
    public TextMeshProUGUI timerText;
    public GameObject gameOverUI;
    public Transform cameraTransform;
    public float shakeDuration = 1f;
    public float shakeMagnitude = 0.1f;
    public Image fadeImage;
    public float fadeDuration = 2f;

    private float timeRemaining;
    private bool isGameOver = false;
    private Vector3 originalCameraPosition;

    private void Start()
    {
        timeRemaining = timerDuration;
        originalCameraPosition = cameraTransform.localPosition;
        StartCoroutine(TimerCoroutine());
    }

    private void Update()
    {
        if (isGameOver)
            return;

        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        GameOver();
    }

    private void GameOver()
    {
        isGameOver = true;
        StartCoroutine(ShakeCamera());
        StartCoroutine(FadeOut());
        Debug.Log("Game Over");
    }

    private IEnumerator ShakeCamera()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomPoint = originalCameraPosition + Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = randomPoint;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalCameraPosition;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }
}
