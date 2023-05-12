using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraMovementManager : MonoBehaviour
{
    [Header("Movement Options")]
    [SerializeField] float cameraSensitivity = 4;
    [SerializeField] Vector2 horizontalLimit;
    [SerializeField] Vector2 verticalLimit;

    [Header("Zoom Options")]
    [SerializeField] float cameraZoomAmount = 8;
    [SerializeField] Vector2Int cameraZoomLimit = new Vector2Int(6, 28);

    [Header("References")]
    [SerializeField] PixelPerfectCamera targetCamera;
    private Camera targetCameraComponent;

    void Awake()
    {
        targetCameraComponent = targetCamera.GetComponent<Camera>();
    }

    private void Update()
    {
        Vector2 keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Vector2 mouseInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Vector2 touchInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        MoveCamera(keyboardInput * cameraSensitivity);
        ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void MoveCamera(Vector2 moveVector)
    {
        targetCamera.transform.position += (Vector3)moveVector * Time.deltaTime;

        Vector2 horizontalBounds = new Vector2(
            targetCameraComponent.orthographicSize * targetCameraComponent.aspect,
            targetCameraComponent.orthographicSize * targetCameraComponent.aspect);

        Vector2 verticalBounds = new Vector2(
        targetCameraComponent.orthographicSize,
        targetCameraComponent.orthographicSize);

        targetCamera.transform.position = new Vector3(
            Mathf.Clamp(targetCamera.transform.position.x, horizontalLimit.x + horizontalBounds.x, horizontalLimit.y - horizontalBounds.y),
            Mathf.Clamp(targetCamera.transform.position.y, verticalLimit.x + verticalBounds.x, verticalLimit.y - verticalBounds.y),
            targetCamera.transform.position.z
            );
    }

    private void ZoomCamera(float zoomAmount)
    {
        cameraZoomAmount = Mathf.Clamp(cameraZoomAmount + zoomAmount * Time.deltaTime * 100 * cameraZoomAmount, cameraZoomLimit.x, cameraZoomLimit.y);
        targetCamera.assetsPPU = (int)(cameraZoomAmount);
    }
}
