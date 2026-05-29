using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    //IDs for power up
    // 0 = triple shot
    // 1 = speed
    // 2 = shields
    [SerializeField]
    private int _powerupId;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //move down at speed of 3
        transform.Translate(Vector3.down* _speed * Time.deltaTime);
        //destroy when leave of the screen
        if(transform.position.y < -5.54f)
        {
            Destroy(this.gameObject);
        }
    }

    //OnTriggerCollision
    //Only be collectable by the player (Hint: use tag)
    //on collected destroy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //communicate with the player script
            //handle to the component i want
            //assign the component to the player
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if (player != null)
            {
                //if powerupId is 0 triple shot
                //else if powerupId is 1 speed
                //else if powerupId is 2 shield
                if (_powerupId == 0)
                {
                    player.TripleShotActive();
                }
                else if (_powerupId == 1)
                {
                    player.SpeedActive();
                }
                else if(_powerupId == 2)
                {
                    player.ShieldActive();
                }

            }
            Destroy(this.gameObject);
        }
    }
}
