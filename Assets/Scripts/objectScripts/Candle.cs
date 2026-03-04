using UnityEngine;

public class Candle : BlowBehaviour
{
    public bool isLit = true;
    public GameObject flame;

    protected override void OnBlown(Vector3 blowDirection)
    {
        if (!isLit) return;

        //only blow if the player presses button to blow (rather than as soon as hitbox collides;
        if (BreathInput.Value <= 0.1f) return;

        isLit = false;

        if (flame != null)
            flame.SetActive(false);
    }
}