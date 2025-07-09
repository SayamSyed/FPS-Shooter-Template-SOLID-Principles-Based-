using UnityEngine;
using TMPro;

public class MaxFPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Assign in Inspector

    private float timer;
    private int frames;
    private float fps;

    void Start()
    {
        Application.targetFrameRate = 60; // Cap FPS to 60
        QualitySettings.vSyncCount = 0;   // Disable VSync to let Application.targetFrameRate take control
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;
        frames++;

        if (timer >= 0.5f) // Update FPS every 0.5 seconds
        {
            fps = frames / timer;
            fpsText.text = $"FPS: {Mathf.RoundToInt(fps)}";
            timer = 0f;
            frames = 0;
        }
    }
}