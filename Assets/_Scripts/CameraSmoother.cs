using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoother : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothingSpeed = 0.20f;
    [SerializeField] Vector3 offset;

    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(offset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothingSpeed);
    }
}
