using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    [Header("Hidden Object")]
    [SerializeField] private Renderer[] objectRenderers;

    private bool isRevealed;

    // Called when the object is revealed by the flashlight
    void Awake()
    {
        objectRenderers = GetComponentsInChildren<Renderer>(true);
        SetVisible(false);
    }

    // Public method to reveal the object
    public void Reveal()
    {
        // If the object is already revealed, do nothing
        if (isRevealed)
            return;

        isRevealed = true;
        SetVisible(true);
    }

    // Private method to set the visibility of the object
    private void SetVisible(bool visible)
    {
        foreach (Renderer r in objectRenderers)
        {
            if (r != null)
            {
                r.enabled = visible;
            }
        }
    }
}
