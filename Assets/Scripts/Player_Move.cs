using System;
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
    public BulletManager bulletManager;
    //Animator
    private Animator _anim;
    private String currentAnim;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
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

        if (Math.Abs(horizontal) > 0.1f)
        {
            changeAnim("run");
            transform.rotation = Quaternion.Euler(new Vector3(0, (horizontal > 0.1f) ? 0 : -180, 0));
            _rb.velocity = movement * _speedMove * Time.deltaTime;
        }
        else
        {
            changeAnim("idle");
            _rb.velocity = Vector2.zero;
        }
    }
    private void changeAnim(String animName)
    {
        if (currentAnim != animName)
        {
            _anim.ResetTrigger(animName);
            currentAnim = animName;
            _anim.SetTrigger(currentAnim);
        }
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


        // thay doi hieu ung dan
        //if (bulletManager == null)
        //{
        //    Debug.LogError("BulletManager is not assigned.");
        //    return;
        //}

        //BulletData currentBulletData = bulletManager.GetCurrentBulletData();
        //if (currentBulletData == null)
        //{
        //    Debug.LogError("Current BulletData is null.");
        //    return;
        //}

        //GameObject bullet = Instantiate(currentBulletData.bulletPrefab, _firePoint.position, _firePoint.rotation);
        //if (bullet == null)
        //{
        //    Debug.LogError("Bullet prefab is null.");
        //    return;
        //}

        //bullet.GetComponent<bulletScript>().Initialize(currentBulletData);
    }
}
