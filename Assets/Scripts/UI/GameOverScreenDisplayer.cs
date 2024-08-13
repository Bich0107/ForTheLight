using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameOverScreenDisplayer : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] TextMeshProUGUI endGameText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] AnimationController UIAnimation;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        DOTween.Init();
    }

    void Start()
    {
        // take info from save file
        endGameText.text = SaveManager.Win ? "YOU WIN!" : "GAME OVER";
        timeText.text = "Your time: " + GetTime();
        difficultyText.text = "Difficulty: " + GetDifficultyText(saveManager.CurrentSaveFile.Difficulty);
        UIAnimation.PlayAnimations();
    }

    string GetTime()
    {
        float seconds = saveManager.CurrentSaveFile.Timer;
        // minutes
        int minutes = (int)seconds / 60;

        // seconds
        int secs = (int)seconds % 60;

        // milliseconds
        int milliseconds = (int)((seconds - (int)seconds) * 1000);

        // format string mm:ss:msms
        return String.Format("{0:D2}:{1:D2}:{2:D3}", minutes, secs, milliseconds / 10);
    }

    string GetDifficultyText(Difficulty difficulty)
    {
        switch ((int)difficulty)
        {
            case 0:
                return "NORMAL";
            case 1:
                return "HARD";
        }
        return "";
    }
}
