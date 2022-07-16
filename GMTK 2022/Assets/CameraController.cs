using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 cameraOffset;

    [SerializeField]
    float smoothness;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = player.position + cameraOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
