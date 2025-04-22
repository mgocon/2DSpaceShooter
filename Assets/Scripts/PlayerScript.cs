using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 rawInput;
    //bounds of camera
    Vector2 minBounds;
    Vector2 maxBounds;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;

    [SerializeField] float paddingTop;

    [SerializeField] float paddingDown;

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>(); //Get the Shooter component 
    }

    void Start()
    {
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 delta = rawInput * Time.deltaTime * moveSpeed;

        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);

        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingDown, maxBounds.y - paddingTop);

        transform.position = newPos; // Update the player's position
    }
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log("Input Vecotr: " + rawInput);
    }
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0)); //bottom left corner of the camera
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1)); //top right corner of the camera
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed; //Set the isFiring property of the Shooter component based on the input value
            Debug.Log("Fire: " + value.isPressed);
        }
    }

}
