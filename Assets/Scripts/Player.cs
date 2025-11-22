using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedActive = false;
    [SerializeField]
    private bool _shieldsActive = false;

    [SerializeField]
    private float _tripleShotExpireTime = 0f;
    [SerializeField]
    private float _speedExpireTime = 0f;

    private float _initialZ = 0f;
    private float _xBound = 10f;
    private float _yBound = 8f;
    private Vector3 offset = new Vector3(0f, 1.05f, 0f);

    [SerializeField]
    private float _powerUpTime = 5.0f;
    [SerializeField]
    private float _speedPowerUpMultiplier = 2f;

    [SerializeField]
    private GameObject _playerShieldVisual;

    [SerializeField]
    private int _score;

    [SerializeField]
    private int _shieldAlreadyActiveBonus = 10;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    GameObject _rightEngineFireObject;
    [SerializeField]
    GameObject _leftEngineFireObject;
    [SerializeField]
    GameObject _shipFireObject;

    [SerializeField]
    Animator _rightEngineFireAnim;
    [SerializeField]
    Animator _leftEngineFireAnim;
    [SerializeField]
    Animator _shipFireAnim;

    [SerializeField]
    private float _deathAnimTime = 3f;

    [SerializeField]
    private bool _inDestructionSequence = false;

    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);

        InstantiateObjects();

        CheckObjects();

        DisableFireObjects();

        PlayerShieldDisplay();
    }

    void InstantiateObjects()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _rightEngineFireObject = GameObject.Find("Right_Engine_Fire");

        _leftEngineFireObject = GameObject.Find("Left_Engine_Fire");

        _shipFireObject = GameObject.Find("Ship_Fire");

        _rightEngineFireAnim = GameObject.Find("Right_Engine_Fire").GetComponent<Animator>();

        _leftEngineFireAnim = GameObject.Find("Left_Engine_Fire").GetComponent<Animator>();

        _shipFireAnim = GameObject.Find("Ship_Fire").GetComponent<Animator>();
    }

    void CheckObjects()
    {
        if (!_spawnManager)
        {
            Debug.LogError("SpawnManager is NULL");
        }

        if (!_uiManager)
        {
            Debug.LogError("UIManager is NULL");
        }

        if (!_rightEngineFireObject)
        {
            Debug.LogError("Right Engine Fire Object is NULL");
        }

        if (!_leftEngineFireObject)
        {
            Debug.LogError("Left Engine Fire Object is NULL");
        }

        if (!_shipFireObject)
        {
            Debug.LogError("Ship Fire Object is NULL");
        }

        if (!_rightEngineFireAnim)
        {
            Debug.LogError("Right Engine Fire Animator is NULL");
        }

        if (!_leftEngineFireAnim)
        {
            Debug.LogError("Left Engine Fire Animator is NULL");
        }

        if (!_shipFireAnim)
        {
            Debug.LogError("Ship Fire Animator is NULL");
        }
    }

    void DisableFireObjects()
    {
        if (_rightEngineFireObject)
        {
            _rightEngineFireObject.SetActive(false);
        }
        if (_leftEngineFireObject)
        {
            _leftEngineFireObject.SetActive(false);
        }
        if (_shipFireObject)
        {
            _shipFireObject.SetActive(false);
        }
    }

    void Update()
    {

        if (_inDestructionSequence)
        {
            return;
        }
        
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        if (_isTripleShotActive && (Time.time > _tripleShotExpireTime))
        {
            _isTripleShotActive = false;
        }

        if (_isSpeedActive && (Time.time > _speedExpireTime))
        {
            _isSpeedActive = false;
            _speed /= _speedPowerUpMultiplier;
        }
    }

    public float xBound
    {
        get { return _xBound; }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, _initialZ);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.x > _xBound)
        {
            transform.position = new Vector3(-_xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -_xBound)
        {
            transform.position = new Vector3(_xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.y > _yBound)
        {
            transform.position = new Vector3(transform.position.x, -_yBound, transform.position.z);
        }
        else if (transform.position.y < -_yBound)
        {
            transform.position = new Vector3(transform.position.x, _yBound, transform.position.z);
        }
    }

    void FireLaser()
    {
         _canFire = Time.time + _fireRate;

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }
    }
    public void Damage()
    {
        if (_shieldsActive)
        {
            _shieldsActive = false;

            PlayerShieldDisplay();

            return;
        }

        _lives--;

        PlayFireAnimation();

        _uiManager.UpdateLivesDisplay();

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();

            _inDestructionSequence = true;

            Destroy(this.gameObject, _deathAnimTime);
        }
    }
    public void TripleShotPowerUp()
    {
        if (!_isTripleShotActive)
        {
            _isTripleShotActive = true;

            _tripleShotExpireTime = Time.time + _powerUpTime;
        }
        else
        {
            _tripleShotExpireTime += _powerUpTime;
        }
    }

    public void SpeedPowerUp()
    {
        if (!_isSpeedActive)
        {
            _isSpeedActive = true;

            _speed *= _speedPowerUpMultiplier;

            _speedExpireTime = Time.time + _powerUpTime;
        }
        else
        {
            _speedExpireTime += _powerUpTime;
        }
    }
    public void ShieldPowerUp()
    {
        if (_shieldsActive)
        {
            AddScore(_shieldAlreadyActiveBonus);
            return;
        }

        _shieldsActive = true;

        PlayerShieldDisplay();
    }

    public void PlayerShieldDisplay()
    {
        PlayerShieldDisplay(_shieldsActive);
    }

    public void PlayerShieldDisplay(bool isActive)
    {
        if (_playerShieldVisual)
        {
            _playerShieldVisual.SetActive(isActive);
        }
    }

    public void AddScore(int points)
    {
        _score += points;

        _uiManager.UpdateScore();
    }
    void PlayFireAnimation()
    {
        if (_lives == 2)
        {
            _rightEngineFireObject.SetActive(true);
        }

        else if (_lives == 1)
        {
            _leftEngineFireObject.SetActive(true);
        }

        else if (_lives < 1)
        {
            _shipFireObject.SetActive(true);
        }
    }

    public int Score
    {
        get { return _score; }
    }

    public int Lives
    {
        get { return _lives; }
    }
}
