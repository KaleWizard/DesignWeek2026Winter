using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialController : MonoSingleton<TutorialController>
{
    public enum ControlSceme : byte { Gamepad, Keyboard }

    public List<Collider> activeTriggers = new();
    [SerializeField] float speed;

    AnimationCurve ease;

    [SerializeField] CanvasGroup gamepadGroup;
    [SerializeField] CanvasGroup keyboardGroup;

    CanvasGroup uiCG;

    float uiAlpha = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ease = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        TryUpdateControlScheme();

        uiAlpha += ((activeTriggers.Count > 0) ? 1 : -1) * speed * Time.deltaTime;

        uiAlpha = Mathf.Clamp01(uiAlpha);
        uiCG.alpha = ease.Evaluate(uiAlpha);
    }

    void TryUpdateControlScheme()
    {
        ControlSceme scheme = GetControlScheme();

        if (scheme == ControlSceme.Gamepad)
        {
            keyboardGroup.alpha = 0;
            uiCG = gamepadGroup;
        } else if (scheme == ControlSceme.Keyboard)
        {
            gamepadGroup.alpha = 0;
            uiCG = keyboardGroup;
        }
    }

    ControlSceme GetControlScheme()
    {
        var devices = InputSystem.devices;

        foreach (var device in devices)
        {
            if (device is Gamepad)
                return ControlSceme.Gamepad;
        }

        return ControlSceme.Keyboard;
    }
}
