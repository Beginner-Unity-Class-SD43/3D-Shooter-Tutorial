using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float runMultiplier = 2f;
    [SerializeField] float jumpPower = 7f;
    [SerializeField] float gravity = 10f;


    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    float curSpeedX;
    float curSpeedY;


    [SerializeField] bool canMove = true;


    [SerializeField] Image healthBar;
    [SerializeField] float maxHealth = 100f;
    float health;

    CharacterController characterController;

    [SerializeField] GameObject deathScreen;

    Gun gun;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        deathScreen.SetActive(false);
        gun = GetComponentInChildren<Gun>();

        health = maxHealth;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (canMove)
        {
            curSpeedX = walkSpeed * Input.GetAxis("Vertical");
            curSpeedY = walkSpeed * Input.GetAxis("Horizontal");
        }
        else
        {
            curSpeedX = 0;
            curSpeedY = 0;
        }

        if (isRunning)
        {
            curSpeedX *= runMultiplier;
            curSpeedY *= runMultiplier;
        }


        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }


    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canMove = false;
        gun.canShoot = false;
    }
}
