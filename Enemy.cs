using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
  [SerializeField]
  private float _speed = 4f;
  private Player _player;

  void Start()
  {
    _player = GameObject.Find("Player").GetComponent<Player>();
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
      if (_player != null)
      {
       _player.Damage();
      }
      
      Destroy(this.gameObject);
    }
    else if (other.tag == "Laser")
    {
      Destroy(other.gameObject);
      if(_player != null)
      {
        _player.AddScore(10);
      }
      Destroy(this.gameObject);
    }
  }
}