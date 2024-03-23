using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 8.0f;
    private UI_Manager _Ui_manager;
    private Animator _Animator;
    private bool _selfdestroyed;
    private AudioSource _explosionAudioData;
    private Vector3 direction;

    void Start()
    {
      direction = new Vector3(Random.Range(-0.5f, 0.5f), -1, 0);
      
      _Animator = transform.GetComponent<Animator>();

      if (_Animator == null)
      {
          Debug.Log("The Animator is NULL");
      }

      _explosionAudioData = GetComponent<AudioSource>();
      if (_explosionAudioData == null)
      {
          Debug.Log("The explosion audio data on the enemy is NULL");
      }

      _Ui_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
      if (_Ui_manager == null)
      {
          Debug.Log("Ui manager could not be loaded");
      }

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-13f, 13f);
            transform.position = new Vector3(randomX, 8f, 0);
            direction = new Vector3(Random.Range(-0.5f, 0.5f), -1, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       Debug.Log("Hit: " + other.transform.name);
       if (other.tag == "Player" && _selfdestroyed == false)
       {
           _selfdestroyed = true;
           _Ui_manager.increaseScore();
           Player player = other.transform.GetComponent<Player>();

           if (player != null)
           {
               player.Damage();
           }
           _speed = 0;
           _explosionAudioData.Play();
           _Animator.SetTrigger("OnEnemyDeath");
           Destroy(this.gameObject, 2.8f);
       }   
       
       if (other.tag == "Laser")
       {
           if (_selfdestroyed == false)
           {
                Destroy(other.gameObject);
                _selfdestroyed = true;
                _speed = 0;
                _Ui_manager.increaseScore();
                _explosionAudioData.Play();
                _Animator.SetTrigger("OnEnemyDeath");
                Destroy(this.gameObject, 2.8f);
           }
           
       }

    }

    
}
