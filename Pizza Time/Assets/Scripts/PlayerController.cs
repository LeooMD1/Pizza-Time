using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions inputActions;

    [Header("--------Variables--------")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float desiredMoveSpeed;
    
    [SerializeField] private float turnSpeed;
    [SerializeField] private float turnAmount;
    
    [SerializeField] private Vector3 playerInput;
    [SerializeField] private Vector3 playerTurn;

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
        playerInput = inputActions.Player.Move.ReadValue<Vector3>();
        playerTurn = inputActions.Player.Turn.ReadValue<Vector3>();
        

        // ground check and stuff
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        if (grounded)
            rb.linearDamping = 2;
        else 
            rb.linearDamping = 0;
        

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
            moveSpeed = Mathf.Lerp(moveSpeed, desiredMoveSpeed, 0.005f);
        
        else if (playerInput.z == 0)
            moveSpeed = 0;
        
        else if (playerInput.z < 0)
            moveSpeed = Mathf.Lerp(moveSpeed, desiredMoveSpeed, 0.005f);

    }


}
