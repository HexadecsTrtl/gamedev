using UnityEngine;

public class Debug : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("Trigger entered by: " + other.gameObject.name);
    }
}
