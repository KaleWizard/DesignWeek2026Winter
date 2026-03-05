using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BreathUI : MonoBehaviour
{
    Slider slider;
    AnimationCurve curve;
    Blow blowScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        blowScript = FindAnyObjectByType<Blow>();
        if (blowScript == null)
            Debug.LogWarning($"{gameObject.name} expected a script of type Blow but did not find one");
    }

    // Update is called once per frame
    void Update()
    {
        float easedValue = curve.Evaluate(blowScript.lastBreathValue);
        slider.value = math.remap(0, 1, 0.15f, 1, easedValue);
    }
}
