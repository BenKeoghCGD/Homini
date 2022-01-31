using UnityEngine;

public class FPCameraController : CameraScript
{
    public override void Init(GameObject player)
    {
        mode = CameraMode.FIRST;
        _player = player;
    }

    private float _xRotation = 0f;
    public override void Run()
    {
        transform.position = new Vector3();
        transform.rotation = new Quaternion();

        float mouseX = Input.GetAxis("Mouse X") * Constants.FPBaseMultiplier * Time.deltaTime * Settings.mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Constants.FPBaseMultiplier * Time.deltaTime * Settings.mouseSensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _player.transform.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}