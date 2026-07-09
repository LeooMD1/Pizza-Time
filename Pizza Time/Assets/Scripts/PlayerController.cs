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

    [SerializeField] private float impactForceThreshold;
    [SerializeField] private float impactFrameTime;

    [Header("--------References--------")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Material SimpleSky;
    [SerializeField] private Npcstuff npc;
    
    [SerializeField] private Material impactFrame;
    
    [SerializeField] private Shake shake;

    [Header("--------Groundcheck--------")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private bool grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {      
        Cursor.lockState = CursorLockMode.Locked;
        
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

        timeOfDay += 0.0001f;

        SimpleSky.mainTextureOffset = new Vector2(timeOfDay, 0);

        foreach (GameObject npcs in npc.ingameNPCs)
        {
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != Ground && collision.impulse.sqrMagnitude > impactForceThreshold * impactForceThreshold)
        {
            StartCoroutine(Impact(impactFrameTime));
        }
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


    private IEnumerator Impact(float timer)
    {
        StartCoroutine(shake.Shaking());

        impactFrame.SetInt("_active", 1);
        yield return new WaitForSeconds(timer);
        impactFrame.SetInt("_Invert", 1);
        yield return new WaitForSeconds(timer);
        impactFrame.SetInt("_Invert", 0);
        impactFrame.SetInt("_Colour", 1);
        yield return new WaitForSeconds(timer);
        impactFrame.SetInt("_active", 0);
        impactFrame.SetInt("_Invert", 0);
        impactFrame.SetInt("_Colour", 0);



    }
}
