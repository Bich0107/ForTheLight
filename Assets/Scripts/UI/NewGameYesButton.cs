using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameYesButton : MonoBehaviour
{
    Button button;

    void Start() {
        button = GetComponent<Button>();
    }

    public void ResetOnClick(){
        button.onClick.RemoveAllListeners();
    }
}
