using UnityEngine;

public class Powerup : MonoBehaviour
{
  [SerializeField]
  private float _speed = 3f;

  [SerializeField] //0 = triple shot, 1 = speed, 2 = shields
  private int powerupId;
  [SerializeField]
  private AudioClip _clip;
  void Update()
  {
    transform.Translate(Vector3.down * _speed * Time.deltaTime);
    if(transform.position.y < -6f )
    {
      Destroy(this.gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if(other.tag == "Player")
    {
      Player player = other.transform.GetComponent<Player>();

      AudioSource.PlayClipAtPoint(_clip, transform.position);

      if (player != null)
      {
        switch(powerupId)
        {
          case 0:
            player.TripleShotActive();
            break;
          case 1:
            player.SpeedBoostActive();
            break;
          case 2:
            player.ShieldActive();
            break;
          default:
            Debug.Log("default value");
            break;
        }
      }

      Destroy(this.gameObject);
    }
  }
}
