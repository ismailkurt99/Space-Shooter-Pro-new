using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
  private Vector3 laserOffset = new Vector3 (0, 1.05f, 0);

  [SerializeField]
  private float _speed = 3.5f;
  private float _speedMultiplier = 2;
  [SerializeField]
  private GameObject _laserPrefab;
  [SerializeField]
  private GameObject _tripleLaserPrefab;
  [SerializeField]
  private GameObject _shieldVisualizer;
  [SerializeField]
  private float _fireRate = 0.5f;
  private float _canFire = -1f;
  [SerializeField]
  private int _lives = 3;
  private SpawnManager _spawnManager;
  private bool _isTripleShotActive = false;
  private bool _isSpeedBoostActive = false;
  private bool _isShieldActive = false;
  [SerializeField]
  private int _score;
  private UIManager _uiManager;
  [SerializeField]
  private GameObject _rightEngine, _leftEngine;
  private AudioSource _laserAudio;
  [SerializeField]
  private GameObject _explosionEffect;
  void Start()
  {
    transform.position = new Vector3(0,-3f,0);
    _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    _laserAudio = GetComponent<AudioSource>();

    if (_spawnManager == null)
    {
      Debug.Log("The Spawn Manager is null!");
    }
    if (_uiManager == null)
    {
      Debug.Log("The UI Manager is null!");
    }
  }

  void Update()
  {
    CalculateMovement();
    FireLaser();
  }

  void CalculateMovement()
  {
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");
    
    Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
    if(_isSpeedBoostActive == true){
      transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);
    }
    else if(_isSpeedBoostActive == false)
    {
      transform.Translate(direction * _speed * Time.deltaTime);
    }

    if (transform.position.y >= 0)
    {
      transform.position = new Vector3(transform.position.x, 0, 0);
    }
    else if (transform.position .y <= -3.8f)
    {
      transform.position = new Vector3(transform.position.x, -3.8f, 0);
    }

    if (transform.position.x > 11.3f)
    {
      transform.position = new Vector3(-11.3f, transform.position.y, 0);
    }
    else if (transform.position.x < -11.3f)
    {
      transform.position = new Vector3(11.3f, transform.position.y, 0);
    }
  }
  
  void FireLaser()
  {
    if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
    {
      if(_isTripleShotActive == false)
      {
      _canFire = Time.time + _fireRate;
      Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
      }

      else if(_isTripleShotActive == true)
      {
        Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
      }
      _laserAudio.Play();
    } 
  } 
  public void Damage()
  {
    if(_isShieldActive == true)
    {
      _isShieldActive = false;
      _shieldVisualizer.SetActive(false);
      return;
    }
      _lives --;
      if(_lives == 2)
      {
        _rightEngine.SetActive(true);
      }
      else if(_lives == 1)
      {
        _leftEngine.SetActive(true);
      }

      _uiManager.UpdateLives(_lives);
      if(_lives < 1)
      {
        GameObject newExplosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        _spawnManager.onPlayerDeath();
        Destroy(this.gameObject);
      }
  }

  public void TripleShotActive()
  {
    _isTripleShotActive = true;
    StartCoroutine(TripleShotPowerRoutine());
  }
  IEnumerator TripleShotPowerRoutine()
  {
    yield return new WaitForSeconds(5.0f);
    _isTripleShotActive = false;
  }

  public void SpeedBoostActive()
  {
    _isSpeedBoostActive = true;
    StartCoroutine(SpeedPowerRoutine());

    IEnumerator SpeedPowerRoutine()
    {
      yield return new WaitForSeconds(5.0f);
      _isSpeedBoostActive = false;
    }
  }

  public void ShieldActive()
  {
    _isShieldActive = true;
    _shieldVisualizer.SetActive(true);    
  }

  public void AddScore(int points)
  {
    _score += points;
    _uiManager.UpdateScore(_score);
  }
}