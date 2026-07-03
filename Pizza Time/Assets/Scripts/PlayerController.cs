using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions inputActions;

    [Header("--------Variables--------")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float desiredMoveSpeed;
    
    [SerializeField] private float timeOfDay;
    
    [SerializeField] private float turnSpeed;
    [SerializeField] private float turnAmount;
    
    [SerializeField] private Vector3 playerInput;
    [SerializeField] private Vector3 playerTurn;

    [Header("--------References--------")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Material SimpleSky;

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


        SimpleSky.mainTextureOffset = new Vector2(0.2f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        playerInput = inputActions.Player.Move.ReadValue<Vector3>();
        playerTurn = inputActions.Player.Turn.ReadValue<Vector3>();
        

        // ground check and stuff
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        if (grounded)
            rb.linearDamping = 2;
        else 
            rb.linearDamping = 0;

        /*if(grounded && playerInput.z > 0.1f && playerTurn.x > 0.1f && moveSpeed > 26)
            rb.linearDamping = 0;
        
        else if (grounded && playerInput.z < 0.1f && playerTurn.x < 0.1f && moveSpeed > 26)
            rb.linearDamping = 0;

        else if (grounded && playerInput.z > 0.1f && playerTurn.x < 0.1f && moveSpeed > 26)
            rb.linearDamping = 0;

        else if (grounded && playerInput.z < 0.1f && playerTurn.x > 0.1f && moveSpeed > 26)
            rb.linearDamping = 0;

        else if (grounded && playerInput.z > 0.1f && playerTurn.x == 0)
            rb.linearDamping = 2;

        else if (grounded && playerInput.z == 0 && playerTurn.x == 0)
            rb.linearDamping = 2;*/


        timeOfDay += 0.0001f;

        SimpleSky.mainTextureOffset = new Vector2(timeOfDay, 0);
    }

    private void FixedUpdate()
    {
        MovePlayer(); 
    }

    private void MovePlayer()
    {
        
        print(inputActions.Player.Move.ReadValue<Vector3>());

        rb.AddForce(transform.rotation * playerInput * moveSpeed, ForceMode.Force);


        print(playerTurn);

        turnAmount += playerTurn.x;

        //transform.rotation = Quaternion.Euler(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        transform.Rotate(new Vector3(0, playerTurn.x * turnSpeed * Time.deltaTime, 0));

        if (playerInput.z > 0.1f)
            moveSpeed = Mathf.Lerp(moveSpeed, desiredMoveSpeed, 0.01f);
        
        else if (playerInput.z == 0)
            moveSpeed = 0;
        
        else if (playerInput.z < 0)
            moveSpeed = Mathf.Lerp(moveSpeed, desiredMoveSpeed, 0.01f);

    }
}
