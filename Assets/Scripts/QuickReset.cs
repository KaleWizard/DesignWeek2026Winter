using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class QuickReset : MonoSingleton<QuickReset>
{
    public UnityEvent OnSceneReload = new();

    void Update()
    {
        if (Keyboard.current.rKey.isPressed)
        {
            OnSceneReload?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
