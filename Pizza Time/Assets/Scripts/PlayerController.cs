using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions inputActions;

    [Header("--------Variables--------")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float turnAmount;

    [Header("--------References--------")]
    [SerializeField] private Rigidbody rb;

    [Header("--------Groundcheck--------")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private bool grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new();
        inputActions.Enable();
        inputActions.Player.Enable();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // ground check and stuff
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        if (grounded)
            rb.linearDamping = 2;
        else 
            rb.linearDamping = 0;
        
        MovePlayer();

    }

    private void MovePlayer()
    {
        
        Vector3 playerInput = inputActions.Player.Move.ReadValue<Vector3>();
        Vector3 playerTurn = inputActions.Player.Turn.ReadValue<Vector3>();
        
        
        print(inputActions.Player.Move.ReadValue<Vector3>());

        rb.AddForce(playerInput * moveSpeed, ForceMode.Force);

        if (playerInput.z > 0.1f)
            Mathf.Lerp(0, moveSpeed, 0.005f);

        print(playerTurn);

        turnAmount += playerTurn.x;

        transform.rotation = quaternion.EulerXYZ(0, turnAmount * turnSpeed, 0);




    }


}
