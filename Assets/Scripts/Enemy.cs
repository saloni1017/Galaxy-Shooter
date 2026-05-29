using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private GameObject _laserPrefeb;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    private float _fireRate = 3f;
    private float _canFire = -1;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        //if (Time.time > _canFire)
        //{
        //    _fireRate = Random.Range(3f, 7f);
        //    _canFire = Time.time + _fireRate;
        //    GameObject enemyLaser = Instantiate(_laserPrefeb, transform.position, Quaternion.identity);
        //    Laser[] laser = enemyLaser.GetComponentsInChildren<Laser>();
        //    for (int i = 0; i < laser.Length; i++)
        //    {
        //        laser[i].AssignEnemyLaser();
        //    }
        //}
    }

    void CalculateMovement()
    {
        // move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // withS random x position
        if (transform.position.y < -5.43f)
        {
            float randomx = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomx, 5.54f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit: " +other.transform.name);

        // if other is player
        // damage the player
        // destroy us
        if(other.tag == "Player")
        {
            //damage player
            //other.transform.GetComponent<Player>().Damage();
            // error 
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("Enemy_Destroy");
            _speed = 0f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }

        // if other is laser
        // damage the laser
        // destroy us
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore();
            }
            _anim.SetTrigger("Enemy_Destroy");
            _speed = 0f;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }
}
