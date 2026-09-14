using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    float horizontal, vertical;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0, vertical) * 5 * Time.deltaTime);
    }
}
