using UnityEngine;

public class EnemyFire : MonoBehaviour
{
[SerializeField]
  private float speed = 8f;
  private Player _player;
  private void Start()
  {
   _player = GameObject.Find("Player").GetComponent<Player>();
  }    
  void Update()
  {
    transform.Translate(Vector3.down * speed * Time.deltaTime);

    if(transform.position.y < 8)
    {
      if(transform.parent != null)
      {
        Destroy(transform.parent.gameObject);
      }
      Destroy(this.gameObject);
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
        }
    }
}