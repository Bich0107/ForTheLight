using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] float timer;
    public float Timer => timer;
    bool isCounting = false;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();

        timer = saveManager.CurrentSaveFile.Timer;
    }

    void Update()
    {
        if (isCounting && saveManager != null)
        {
            timer += Time.deltaTime;
            saveManager.CurrentSaveFile.Timer = timer;
        }
    }

    public void StartTimer() => isCounting = true;

    public void StopTimer() => isCounting = false;

    public void ResetTimer() => timer = 0;
}
