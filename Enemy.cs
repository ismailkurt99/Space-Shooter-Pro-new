using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;


public class Enemy : MonoBehaviour
{
  [SerializeField]
  private float _speed = 4f;
  private Player _player;
  private Animator _anim;
  [SerializeField]
  private GameObject _explosionEffect;
  [SerializeField]
  private GameObject _enemyLaser;
  private float _canfire = -0.5f;
  private float _fireRate = 2.0f;
  void Start()
  {
    _player = GameObject.Find("Player").GetComponent<Player>();
    _anim = GetComponent<Animator>();

    if(_anim == null)
    {
      Debug.LogError("Enemy animator is null!");
    }
  }

  void Update()
  {
    transform.Translate(Vector3.down * _speed * Time.deltaTime);
    if(transform.position.y < -6f)
    {
      float randomPosition = Random.Range(-8f, 8f);
      transform.position = new Vector3(randomPosition, 8, 0);
    }

    if(Time.time > _canfire)
    {
      _canfire = Time.time + _fireRate;
      StartCoroutine(RandomFireRoutine());
    }
  }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" )
        {
          Player player = other.transform.GetComponent<Player>();
          if (_player != null)
          {
          _player.Damage();
          }
          GameObject newExplosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
          _anim.SetTrigger("OnEnemyDeath");
          _speed = 0;
          Destroy(this.gameObject, 2.5f);
        }
        else if (other.tag == "Laser")
        {
          Destroy(other.gameObject);
          if(_player != null)
          {
            _player.AddScore(10);
          }
          GameObject newExplosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
          _anim.SetTrigger("OnEnemyDeath");
          _speed = 0;
          Destroy(GetComponent<Collider2D>());
          Destroy(this.gameObject, 2.5f);
        }
  }

  IEnumerator RandomFireRoutine()
  {
    GameObject newLaser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
    yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
  }
  
}