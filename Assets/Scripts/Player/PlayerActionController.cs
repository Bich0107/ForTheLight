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

    void OnChangeWeapon(InputValue _value)
    {
        if (!GameManager.Instance.PlayerControlStatus()) return;

        float value = _value.Get<float>();
        if (value < 0f)
        {
            gunHolder.Back();
        }
        else
        {
            gunHolder.Next();
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
