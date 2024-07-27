using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public float _speed;
    public float _lifeTime;
    public GameObject _effectBullet;
    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        //Check va cham
        _rb = GetComponent<Rigidbody2D>();

        //Time to destroy Bullet 
        Destroy(this.gameObject, _lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet move
        _rb.velocity = transform.up * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Destroy Bullet
            Destroy(this.gameObject);

            //Instan Effect (Explore)
            GameObject effectExplore = Instantiate(_effectBullet, transform.position, Quaternion.identity);

            //Destroy Explore
            Destroy(effectExplore, 0.1f);

            //Find & destroy enemy
            /*var name = other.attachedRigidbody.name;
            Destroy(GameObject.Find(name), 0f);*/

        }
    }
}
