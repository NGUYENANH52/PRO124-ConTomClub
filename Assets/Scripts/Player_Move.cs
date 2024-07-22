using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //Move
    [SerializeField] private float _speedMove;
    private Rigidbody2D _rb;
    //Animation
    private Animator _anim;
    private String currentAnim;
    //Attack
    [SerializeField] private GameObject _bullet;
    [SerializeField] Transform _firePoint;
    [SerializeField] private float _atkSpeed, _cooldown = 0;
    //Finish
    public GameObject Panel, Button, Text;
    //Slider
    public Slider _healthBar;
    //Music
    public AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //Move
        _rb = GetComponent<Rigidbody2D>();
        //Anim
        //_anim = GetComponent<Animator>();
        //Slider
        _healthBar.value = 3;
        //Audio
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Move();
        Attack();*/
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical).normalized;


        /*if (Math.Abs(horizontal) > 0.1f || Math.Abs(vertical) > 0.1f)
        {
            changeAnim("run");
            transform.rotation = Quaternion.Euler(new Vector3(0, (horizontal > 0.1f) ? 0 : -180, 0));
            _rb.velocity = movement * _speedMove * Time.deltaTime;
        }
        else
        {
            changeAnim("idle");
            _rb.velocity = Vector2.zero;
        }*/
    }

    /*private void changeAnim(String animName)
    {
        if (currentAnim != animName)
        {
            _anim.ResetTrigger(animName);
            currentAnim = animName;
            _anim.SetTrigger(currentAnim);
        }
    }*/

    void Attack()
    {
        _cooldown -= Time.deltaTime;
        if (_cooldown > 0)
        {
            return;
        }

        //Fire Bullet
        if (Input.GetKey(KeyCode.F))
        {
            //changeAnim("attack");

            Instantiate(_bullet, _firePoint.position, transform.rotation);

            _cooldown = _atkSpeed;


        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("lv2"))
        {
            SceneManager.LoadScene("Map 2");
        }
        if (other.gameObject.CompareTag("lv3"))
        {
            SceneManager.LoadScene("Map 3");
        }
        if (other.gameObject.CompareTag("finish"))
        {
            Time.timeScale = 0;
            Panel.SetActive(true);
            Button.SetActive(true);
            Text.SetActive(true);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            _healthBar.value--;
            if (_healthBar.value == 0)
            {
                //changeAnim("death");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            Debug.Log("Va cham voi Enemy");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            var name = other.attachedRigidbody.name;
            Destroy(GameObject.Find(name), 0f);

            _healthBar.value -= 0.5f;

            if (_healthBar.value == 0)
            {
                //changeAnim("death");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    public void isCheck()
    {
        Debug.Log("Toggle Music");
        _audioSource.mute = !_audioSource.mute;
    }
}
