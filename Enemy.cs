using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
  [SerializeField]
  private float _speed = 4f;
  private Player _player;
  private Animator _anim;

  void Start()
  {
    _player = GameObject.Find("Player").GetComponent<Player>();
    _anim = GetComponent<Animator>();

    if(_anim == null)
    {
      Debug.LogError("animator is null!");
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
      _anim.SetTrigger("OnEnemyDeath");
      _speed = 0;
      Destroy(this.gameObject, 2.5f);
    }
  }
}