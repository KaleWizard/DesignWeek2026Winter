using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance => GetInstance();

    /// <summary>
    /// Finds a singleton in the scene or creates one if none exists
    /// </summary>
    /// <returns></returns>
    private static T GetInstance()
    {
        if (!instance)
        {
            instance = FindAnyObjectByType<T>();
            if (!instance)
            {
                new GameObject(typeof(T).ToString(), typeof(T));
            } else if (instance.gameObject.scene.name != "DontDestroyOnLoad")
            {
                instance.transform.parent = null;
                DontDestroyOnLoad(instance.gameObject);
            }
        }

        return instance;
    }

    private void Awake()
    {
        GetInstance();

        if (instance && instance != this)
        {
            Debug.LogWarning($"Destroying duplicate {typeof(T)} component.");
            Destroy(this);
        }
    }

    /// <summary>
    /// Empty function to begin the MonoSingleton's behaviour
    /// </summary>
    public static void Wake() { }
}
