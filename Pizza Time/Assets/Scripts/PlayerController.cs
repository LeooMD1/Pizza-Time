using Mono.Cecil.Cil;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions inputActions;

    [Header("--------Variables--------")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    [Header("--------References--------")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new();
        inputActions.Enable();
        inputActions.Player.Enable();

        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        print(inputActions.Player.Move.ReadValue<Vector3>());

        rb.linearVelocity = new Vector3(rb.linearVelocity.x * moveSpeed, rb.linearVelocity.y);
    }
}
