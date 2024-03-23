using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    float _speed = 15f;
    private Animator _Animator;
    private AudioSource _explosionSound;
    private Spawn_Manager _SpawnManager;
    void Start()
    {
        _SpawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _Animator = transform.GetComponent<Animator>();
        _explosionSound = transform.GetComponent<AudioSource>();

        if (_SpawnManager == null || _Animator == null || _explosionSound == null)
        {
            Debug.Log("Spawn Manager or Asteroid or Explosion Sound is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1) * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with Asteroid");

        if (other.tag == "Laser")
        {
            _explosionSound.Play();
            _Animator.SetTrigger("Asteroid_Exploded");
            Destroy(other.gameObject);
            _SpawnManager.StarttheGame();
            Destroy(this.gameObject, 1.5f);      
        }

    }

}
