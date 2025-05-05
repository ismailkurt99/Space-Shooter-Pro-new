using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _explosionAudio;
    void Start()
    {
        Destroy(this.gameObject, 2.5f);
    }

    void Update()
    {
        
    }
}
