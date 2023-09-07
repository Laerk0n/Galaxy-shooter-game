using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isSheldActive = false;
    [SerializeField]
    private GameObject _tripelshotPrefab;
    [SerializeField]
    private GameObject _speedPowerupPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score;
    private UIManager _uIManager;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manger is NULL.");
        }
    }

    void Update()
    {
        CalculateMovments();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovments()
    {
        float horizontalInputt = Input.GetAxis("Horizontal");
        float verticalInputt = Input.GetAxis("Vertical");

        Speedboost(horizontalInputt, verticalInputt, _speed, _isSpeedBoostActive ? _speedMultiplier : 1);
        
        if (transform.position.y >= 2)
        {
            transform.position = new Vector3(transform.position.x, 2, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 10.8f)
        {
            transform.position = new Vector3(-10.8f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.8f)
        {
            transform.position = new Vector3(10.8f, transform.position.y, 0);
        }
    }

    void Speedboost(float horizontalInputt, float verticalInputt, float speed, float speedMultiplier = 0)
    {
        
        transform.Translate(new Vector3(horizontalInputt, verticalInputt, 0) * speed * speedMultiplier * Time.deltaTime);
    }

    void FireLaser()
    {
        {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripelshotPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

            }

        }
    }

    public void Damage()
    {
        if (_isSheldActive == true)
        {
            _isSheldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        _lives--;

        _uIManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripelPowerDownRutin());
    }

    IEnumerator TripelPowerDownRutin()
    {

        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBostActive()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBostDownRutin());
    }

    IEnumerator SpeedBostDownRutin()
    {
        yield return new WaitForSeconds(6.0f);
        _isSpeedBoostActive = false;
    }
    public void SheildBostActive()
    {
        _shieldVisualizer.SetActive(true);
        _isSheldActive = true;

    }
    public void AddScore(int points)
    {
        _score += points;
        _uIManager.UpdateScore(_score);
    }

}
