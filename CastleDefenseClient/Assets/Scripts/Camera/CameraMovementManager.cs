using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraMovementManager : MonoBehaviour
{
    [SerializeField] float cameraSensitivity = 4;
    [SerializeField] float cameraZoomAmount = 8;
    [SerializeField] PixelPerfectCamera targetCamera;

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
    }

    private void ZoomCamera(float zoomAmount)
    {
        cameraZoomAmount = Mathf.Clamp(cameraZoomAmount + zoomAmount * Time.deltaTime * 100 * cameraZoomAmount, 8, 48);
        targetCamera.assetsPPU = (int)(cameraZoomAmount);
    }
}
