using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    
    [SerializeField] Camera viewCam;
    
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] GameObject bulletImpact;

    public static PlayerController instance;

    public float moveSpeed = 5f;

    private Vector2 _moveInput;
    private Vector2 _mouseInput;
    private Rigidbody2D _rb;
    private bool _canShoot = true;
    //private bool _isSlower = false;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardMovement();
        //MouseMovement();
        Shoot();

    }

    private void KeyboardMovement()
    {
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //Vector3 moveHorizontal = transform.up * -_moveInput.x;
        Vector3 moveVertical = transform.right * _moveInput.y;
        float turnDiff = Input.GetAxis("Horizontal") * turnSpeed;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - turnDiff);
        _rb.velocity = (/*moveHorizontal +*/ moveVertical) * moveSpeed;
    }

    private void MouseMovement()
    {
        _mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - _mouseInput.x);

        viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, _mouseInput.y, 0f));
    }
    private void Shoot()
    {
        if (_canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("fire!");
                Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Instantiate(bulletImpact, hit.point, transform.rotation);
                }
                else
                {
                    print("nothing");
                }
                _canShoot = false;
                StartCoroutine(PlayerReload());
            }
            
        }
    }

    IEnumerator PlayerReload()
    {
        //if(!isSlower)
        //rewinding weapon sound
        yield return new WaitForSeconds(2f);
        //reload animation
        _canShoot = true;
    }
}
