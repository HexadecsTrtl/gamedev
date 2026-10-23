using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    [Header("Layer Masks")]
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected LayerMask obstacleMask;

    [Header("References")]
    [SerializeField] EndScreenUI endScreen;

    static bool caught;

    protected virtual void Awake()
    {
        caught = false;   // reset after a scene reload
    }

    protected void Catch(string message)
    {
        // detection runs every frame; only trigger once
        if (caught) return;   
        caught = true;

        if (endScreen != null)
            endScreen.ShowEndScreen(message);
    }
}