using UnityEngine;

public class FindTerrain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var components = FindObjectsOfType<Terrain>();
        foreach (var component in components)
        {
            Debug.Log(component.gameObject.name);
        }
    }
}
