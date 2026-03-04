using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 5;

    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToDestroy) Destroy(gameObject);
    }
}
