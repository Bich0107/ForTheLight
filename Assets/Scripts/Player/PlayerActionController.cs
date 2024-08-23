using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    [SerializeField] GunHolder gunHolder;
    bool isHolding = false;

    void Update()
    {
        if (isHolding)
        {
            gunHolder.HoldTrigger();
        }
    }

    void OnFire(InputValue _value)
    {
        if (!GameManager.Instance.PlayerControlStatus()) return;

        if (_value.isPressed)
        {
            isHolding = true;
        }
        else
        {
            isHolding = false;
            gunHolder.ReleaseTrigger();
        }
    }

    void OnTalk(InputValue _value)
    {
        if (_value.isPressed)
        {
            DialogueSystem.Instance.NextDialogue();
        }
    }

    public void Reset()
    {
        isHolding = false;
        gunHolder.Reset();
    }
}
