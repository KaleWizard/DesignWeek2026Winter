using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(100)]
public class BreathInput : MonoSingleton<BreathInput>
{
    public static float Value => Instance.lastInputValue;
    public static float FixedDelta => Instance.lastValueFixed - Instance.secondLastValueFixed;

    InputAction leftBreath;
    InputAction rightBreath;

    float lastInputValue = 0;

    float secondLastValueFixed = 0;
    float lastValueFixed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftBreath = InputSystem.actions.FindAction("BreathLeft");
        rightBreath = InputSystem.actions.FindAction("BreathRight");
    }

    // Update is called once per frame
    void Update()
    {
        lastInputValue = GetInputValue();

        if (Keyboard.current.digit1Key.isPressed || Mouse.current.leftButton.isPressed)
            lastInputValue = 0.33f;
        else if (Keyboard.current.digit2Key.isPressed)
            lastInputValue = 0.67f;
        else if (Keyboard.current.digit3Key.isPressed || Mouse.current.rightButton.isPressed)
            lastInputValue = 1;
    }

    private void FixedUpdate()
    {
        secondLastValueFixed = lastValueFixed;
        lastValueFixed = lastInputValue;
    }

    float GetInputValue()
    {
        return (leftBreath.ReadValue<float>() + rightBreath.ReadValue<float>()) / 2;
    }
}
