using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(obj, spawnPoint);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
