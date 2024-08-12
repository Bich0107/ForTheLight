using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] Difficulty difficulty;
    [SerializeField] SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        GetComponent<Button>().onClick.AddListener(() => sceneLoader.StartNewGame(difficulty));
    }
}
