using UnityEngine;
using UnityEngine.InputSystem;

public class BreathInput : MonoSingleton<BreathInput>
{
    public static float Value => Instance.lastInputValue;
    public static float AccumulatedDelta 
    { 
        get 
        {
            float value = Instance.lastInputValue - Instance.lastReadValue;
            Instance.lastReadValue = Instance.lastInputValue;
            return value;
        }
    }

    InputAction leftBreath;
    InputAction rightBreath;

    float lastInputValue = 0;
    float lastReadValue = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftBreath = InputSystem.actions.FindAction("BreathLeft");
        rightBreath = InputSystem.actions.FindAction("BreathRight");
    }

    // Update is called once per frame
    void Update()
    {
        lastInputValue = (leftBreath.ReadValue<float>() + rightBreath.ReadValue<float>()) / 2;

#if UNITY_EDITOR
        if (Keyboard.current.digit1Key.isPressed)
            lastInputValue = 0.33f;
        else if (Keyboard.current.digit2Key.isPressed)
            lastInputValue = 0.67f;
        else if (Keyboard.current.digit3Key.isPressed)
            lastInputValue = 1;
#endif
    }
}
