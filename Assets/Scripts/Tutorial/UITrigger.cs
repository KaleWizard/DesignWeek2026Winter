using UnityEngine;

public class UITrigger : MonoBehaviour
{
    int playerLayerHash;
    new Collider collider;

    private void Start()
    {
        playerLayerHash = LayerMask.NameToLayer("Player");
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayerHash)
        {
            TutorialController.Instance.activeTriggers.Add(collider);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayerHash)
        {
            TutorialController.Instance.activeTriggers.Remove(collider);
        }
    }
}
