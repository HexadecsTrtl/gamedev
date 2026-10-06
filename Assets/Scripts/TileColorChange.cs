using UnityEngine;

public class TileColorChange : MonoBehaviour
{
    [SerializeField] IncreaseHealthTrigger healTrigger;
    [SerializeField] Material usedMaterial;

    Renderer tileRenderer;

    private void Start()
    {
        tileRenderer = GetComponent<Renderer>();
    }

    void OnEnable()
    {
        if (healTrigger != null)
            healTrigger.OnActivated += HandleActivated;
    }

    void OnDisable()
    {
        if (healTrigger != null)
            healTrigger.OnActivated -= HandleActivated;
    }

    void HandleActivated()
    {
        tileRenderer.material = usedMaterial; 
    }
}
