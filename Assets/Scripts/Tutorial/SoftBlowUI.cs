using UnityEngine;

public class SoftBlowUI : MonoSingleton<SoftBlowUI>
{
    [SerializeField] CanvasGroup group;
    [SerializeField] float speed = 2f;

    float minTime = 0.1f;

    float alpha = 0;

    float timer = 0;


    float totalTime = 0;

    public void Increase()
    {
        timer = 0f;

        QuickReset.Instance.OnSceneReload.AddListener(ResetUITimer);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        group.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < minTime && totalTime < 5f)
        {
            alpha += speed * Time.deltaTime;
        }
        else
        {
            alpha -= speed * Time.deltaTime;
        }

        alpha = Mathf.Clamp01(alpha);
        group.alpha = alpha;

        timer += Time.deltaTime;


        if (alpha > 0.9f)
            totalTime += Time.deltaTime;
    }

    void ResetUITimer()
    {
        totalTime = 0;
        group.alpha = alpha = 0;
        timer = 100;
    }
}
