using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] Transform viewCam;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float mouseSensitivity = 1f;

    private Vector2 _moveInput;
    private Vector2 _mouseInput;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //WASD Movement
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 moveHorizontal = transform.up * -_moveInput.x;
        Vector3 moveVertical = transform.right * _moveInput.y;

        rb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

        //Mouse Movement
        _mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - _mouseInput.x);

        viewCam.localRotation = Quaternion.Euler(viewCam.localRotation.eulerAngles + new Vector3(0f, _mouseInput.y, 0f));
    }
}
