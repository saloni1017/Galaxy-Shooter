using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    private float _speedPwerup = 8.5f;
    public GameObject laserPrefeb;
    public GameObject tripleShotPrefeb;
    public GameObject speedPowerupPrefeb;
    public GameObject playerShield;
    public GameObject _leftEngine;
    public GameObject _rightEngine;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;

    private SpawnManager _spawnManager;

    public int _lives = 3;

    // variable to know isTripleShotActive
    [SerializeField]
    private bool _isTripleShotActive = false;
    
    [SerializeField]
    private bool _isSpeedPowerupActive = false;
    
    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private int _score = 0;

    private UIManager _uiManager;

    //audio clip
    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); //find object and get component

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //_leftEngine = GameObject.Find("Left_Engine").GetComponent<SpriteRenderer>();
        //_rightEngine = GameObject.Find("Right_Engine").GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        if(_audioSource == null)
        {
            Debug.Log("Audio source is null");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        // if space press spawn gameobject
        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();

        }
    }

    void CalculateMovement()
    {
        //transform.position += new Vector3(1f, 0, 0);
        // time.deltaTime = convert frames to sec
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right* horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); 

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if(_isSpeedPowerupActive)
        {
            transform.Translate(direction * _speedPwerup * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        // if player y is greater than 0
        // y position = 0
        // else if y position is less than -4.37
        // y postion = -4.37
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.37)
        {
            transform.position = new Vector3(transform.position.x, -4.37f, 0);
        }

        // if player x position is greater than 11.20
        // x = -11.20
        // else if x position is less than -11.20
        // x = 11.20
        if (transform.position.x >= 9)
            transform.position = new Vector3(-9f, transform.position.y, 0);
        else if (transform.position.x <= -9f)
        {
            transform.position = new Vector3(9f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;

        //if space is press
        //if isTripleShotActive is true
        //fire 3 lasers
        //else 
        //fire 1 laser
        //Instantiate 3 lasers (for triple shot prefeb)
        if (_isTripleShotActive == true)
        {
            Instantiate(tripleShotPrefeb, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefeb, transform.position + new Vector3(0f,1.05f,0f), Quaternion.identity);
        }

        //play sound on firing
        _audioSource.Play();
    }

    public void Damage()
    {
        //if shield is active deactivate shield
        if(_isShieldActive == true)
        {
            playerShield.gameObject.SetActive(false);   
            _isShieldActive = false;
            return;
        }
        _lives--;
        //if live is 2 enable right engine
        //else if lives is 1 enable left engine
        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);

        //check if dead
        // destroy us
        if(_lives < 1)
        {
            // communicate spawnmanager let them know the player is die or not
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        // _isTripleShotActive is true
        _isTripleShotActive = true;
        StartCoroutine(TripleShotInactive());
        // start the power down coroutine for triple shot
    }

    IEnumerator TripleShotInactive()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedActive()
    {
        _isSpeedPowerupActive= true;
        StartCoroutine(SpeedPoweupInactive());
    }

    IEnumerator SpeedPoweupInactive()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedPowerupActive = false;
    }

    public void ShieldActive()
    {
        playerShield.gameObject.SetActive(true);
        _isShieldActive = true;
    }

    //method to add 10 to score
    //communicate with the UIManager script
    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }
}
