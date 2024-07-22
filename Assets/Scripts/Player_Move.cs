using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    //Move
    [SerializeField] private float _speedMove;
    private Rigidbody2D _rb;
    //Attack
    [SerializeField] private GameObject _bullet;
    [SerializeField] Transform _firePoint;
    [SerializeField] private float _atkSpeed, _cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(horizontal, 0).normalized;
        _rb.velocity = movement * _speedMove * Time.deltaTime;
    }
    void Attack()
    {
        _cooldown -= Time.deltaTime;
        if (_cooldown > 0)
        {
            return;
        }
        Instantiate(_bullet, _firePoint.position, transform.rotation);
        _cooldown = _atkSpeed;
    }
}
