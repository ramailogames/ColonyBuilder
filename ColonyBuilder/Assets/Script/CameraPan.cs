using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float panSpeed = 10f;
    public float damping = 5f;
    public Vector2 panLimit = new Vector2(10f, 10f);

    private Vector3 targetPosition;
    private Vector3 lastMousePosition;
    private bool isPanning;

    private void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            isPanning = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPanning = false;
        }

        if (isPanning)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 pan = new Vector3(-delta.x, -delta.y, 0) * panSpeed * Time.deltaTime;
            targetPosition += pan;
        }

        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, -panLimit.x, panLimit.x),
            Mathf.Clamp(targetPosition.y, -panLimit.y, panLimit.y),
            transform.position.z // Maintain original Z position
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);

        lastMousePosition = Input.mousePosition;
    }
}
