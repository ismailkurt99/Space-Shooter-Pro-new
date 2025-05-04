using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 0.01f;
    [SerializeField]
    private GameObject _explosionEffect;
    private Player _player;
    private SpawnManager _spawnManager;


    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(0, 0, _rotateSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(_player != null)
            {
                _player.Damage();
            }
            GameObject newExplosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.1f);
        }
        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            GameObject newExplosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.1f);
        }
    }
}
