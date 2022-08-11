using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5.5f;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        _animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

     void FixedUpdate()
    {
        //If both horizoznal and vertical input are used movement direction is normalized
        if(_movement.x != 0.0f && _movement.y != 0.0f)
        {
          _rb.MovePosition(_rb.position + _movement.normalized * Speed * Time.fixedDeltaTime);
        }
        else
        {
        _rb.MovePosition(_rb.position + _movement * Speed * Time.fixedDeltaTime);
        }
    }
}
