using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float jumpForce = 10f;
    private Rigidbody _rb;
    public Transform groundCheck;
    public LayerMask LayerGround;
    //public Animator animator;

    private float axisX;
    private float axisY;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.x);
        }
        //animator.SetFloat("Speed", Mathf.Abs(axisY));
    }
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.5f, LayerGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, 0.5f);
    }

    void FixedUpdate()
    {
        _rb.velocity = axisY * movementSpeed * transform.forward + new Vector3(0, _rb.velocity.y, 0) + axisX * movementSpeed * transform.right;
    }
}