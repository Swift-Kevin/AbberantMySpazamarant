using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerStatManager statManager;
    [SerializeField] private float vertLookDegree;
    [SerializeField] private float cameraSpeedVert;
    [SerializeField] private float cameraSpeedHori;
    float xRot;

    void Start()
    {

    }

    void Update()
    {
        AimCamera();
    }

    private void AimCamera()
    {
        Vector2 input = InputManager.Instance.CameraReadVal();
        xRot += -input.y * cameraSpeedVert * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, -vertLookDegree, vertLookDegree);

        // Rotate Vertically
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        // Rotate horizontally

        if (Mathf.Abs(input.x) > 0)
        {
            transform.parent.Rotate(Vector3.up * input.x * Time.deltaTime * cameraSpeedHori);
        }
    }
}
