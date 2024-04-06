using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5.0f;

    [SerializeField]
    private float rotationSpeed = 720.0f;

    [SerializeField]
    private Animator anim;

    private Rigidbody rb;
    private Vector3 inputVector;
    private PlayerInventory inventory;
    private PlayerUI playerUI;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<PlayerInventory>();
        playerUI = GetComponent<PlayerUI>();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void Update()
    {
        PlayerInput();
    }


    private void PlayerInput()
    {
        float xMove = SimpleInput.GetAxis("Horizontal");
        float zMove = SimpleInput.GetAxis("Vertical");

        inputVector = new Vector3(xMove, 0.0f, zMove);
    }

    private void PlayerMovement()
    {
        //inputVector.Normalize();

        bool isPlayerMoving = inputVector.sqrMagnitude > 0f;

        if (isPlayerMoving)
        {
            inputVector.Normalize();
        }
        
        anim.SetBool("IsRunning", isPlayerMoving);

        rb.MovePosition(rb.position + inputVector * movementSpeed * Time.fixedDeltaTime);

        if (inputVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputVector, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactible"))
        {
            other.GetComponent<IItemInteraction>().Interact(inventory);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactible"))
        {
            other.GetComponent<IItemInteraction>().EndInteract();
        }
    }
}