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

        // display final score
        timeText.text = "Your time: " + StringHelper.SecondToString(saveManager.CurrentSaveFile.Timer);

        difficultyText.text = "Difficulty: " + GetDifficultyText(saveManager.CurrentSaveFile.Difficulty);

        UIAnimation.PlayAnimations();
        
        // reset win state
        SaveManager.Win = false;
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
