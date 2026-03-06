using UnityEngine;
using UniSense;

public class AbstractDualSenseBehaviour : MonoBehaviour
{
    /// <summary>
    /// The DualSense instance. 
    /// It can be null if no one is connected so always 
    /// use the null-conditional operator <c>?.</c> on it.
    /// </summary>
    public DualSenseGamepadHID DualSense { get; protected set; }

    internal virtual void OnConnect(DualSenseGamepadHID dualSense)
        => DualSense = dualSense;

    internal virtual void OnDisconnect() => DualSense = null;
}
