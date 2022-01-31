using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _velocity;
    private Transform _groundCheck;
    private bool _grounded;
    public LayerMask groundMask;

    private void Start()
    {
        _controller = FindObjectOfType<CharacterController>();
        _groundCheck = GameObject.Find("Ground").transform;
    }

    private void Update()
    {
        _grounded = Physics.CheckSphere(_groundCheck.position, 0.4f, groundMask);

        if(_grounded && _velocity.y < 0) _velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * (Input.GetKey(KeyCode.LeftControl) ? Constants.PlayerSprintSpeed : Constants.PlayerWalkSpeed) * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _grounded) _velocity.y = Mathf.Sqrt(Constants.PlayerJumpHeight * -2f * Constants.Gravity);

        _velocity.y += Constants.Gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}