using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody _rb;
    private Vector3 _movements = new Vector3(0, 0, 0);
    private Vector3 spawnPos;
    
    public bool canMove;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        spawnPos = transform.position;
    }
    private void Update()
    {
        if (canMove)
            move();
        else
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
    }

    private void move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        _movements.x = hAxis * moveSpeed;
        _movements.y = _rb.velocity.y;
        _movements.z = vAxis * moveSpeed;

        _rb.velocity = _movements;
    }

    public void resetPosition()
    {
        transform.position = spawnPos;
    }
}
