using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    [SerializeField] private float _speedMove;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical).normalized;

    }
}
