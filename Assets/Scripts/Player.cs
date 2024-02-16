using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inputs playerInputs;

    public float moveSpeed;
    public float maxSpeed;
    public float drag;

    public Transform figherModel;
    public Transform propellerModel;
    public GameObject bulletPrefab;

    public Vector2 verticalBounds;
    public Vector2 HorizontalBounds;

    private Vector3 currentMoveVect;

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = new Inputs();
        playerInputs.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Bank();

        if (playerInputs.Player.Fire.triggered)
        {
            Fire();
        }

        propellerModel.eulerAngles += new Vector3(0,0, 1000 * Time.deltaTime);
    }

    private void Move()
    {
        Vector2 moveInput = playerInputs.Player.Movement.ReadValue<Vector2>();
        Vector3 moveVect = new Vector3(moveInput.x, 0, moveInput.y);

        if (transform.position.x < HorizontalBounds.x && moveVect.x < 0) // Left bound
        {
            moveVect.x = 0.5f;
        }
        if (transform.position.x > HorizontalBounds.y && moveVect.x > 0) // Right bound
        {
            moveVect.x = -0.5f;
        }
        if (transform.position.z < verticalBounds.y && moveVect.z < 0) // Top bound
        {
            moveVect.z = 0.5f;
        }
        if (transform.position.z > verticalBounds.x && moveVect.z > 0) // Bottom bound
        {
            moveVect.z = -0.5f;
        }
        
        if (moveVect.magnitude > 0 && currentMoveVect.magnitude < maxSpeed)
        {
            currentMoveVect += moveVect * moveSpeed * Time.deltaTime;
        }

        currentMoveVect = Vector3.MoveTowards(currentMoveVect, Vector3.zero, drag * Time.deltaTime);

        transform.position += currentMoveVect * Time.deltaTime;
    }

    public void Bank()
    {
        Vector2 moveInput = playerInputs.Player.Movement.ReadValue<Vector2>();
        Vector3 moveVect = new Vector3(moveInput.x, 0, moveInput.y);

        float bankAngle = 0;
        if (moveVect.x > 0)
        {
            bankAngle = 15;
        }
        else if (moveVect.x < 0)
        {
            bankAngle = -15;
        }

        float currBankAngle = Mathf.LerpAngle(figherModel.eulerAngles.z, bankAngle, 10 * Time.deltaTime);
        figherModel.eulerAngles = new Vector3(0, 180, currBankAngle);
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, transform.position + new Vector3(3,0,2), Quaternion.identity);
        Instantiate(bulletPrefab, transform.position + new Vector3(-3,0,2), Quaternion.identity);

    }
}
