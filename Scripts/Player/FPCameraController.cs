using UnityEngine;

public class FPCameraController : CameraScript
{
    [SerializeField] GameObject waterOverlay;
    public override void Init(GameObject player)
    {
        mode = CameraMode.FIRST;
        _player = player;
    }

    private float _xRotation = 0f;
    public override void Run()
    {
        transform.localPosition = new Vector3();

        float mouseX = Input.GetAxis("Mouse X") * Constants.FPBaseMultiplier * Time.deltaTime * Settings.mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Constants.FPBaseMultiplier * Time.deltaTime * Settings.mouseSensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -85f, 85f);

        _player.transform.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        waterOverlay.SetActive(transform.position.y <= 15f);
    }
}