using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modular_Powerup_Script : MonoBehaviour
{
    // Start is called before the first frame update
    private float _speed = 3f;
    [SerializeField]
    private int _PowerupID;
    private AudioSource _powerupSound;

    void Start()
    {
        _powerupSound = GameObject.Find("Powerup Sound").GetComponent<AudioSource>();

        if (_powerupSound == null)
        {
            Debug.Log("Powerup Sound is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down* _speed * Time.deltaTime);
        if (transform.position.y <= -7f)
        {
            Destroy(this.gameObject);
        }
    }

   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _powerupSound.Play();
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch(_PowerupID)
                {
                    case 0:
                       player.TripleShot();
                       break;
                    case 1:
                        player.Speedup();
                        break;
                    case 2:
                        player.Shield();
                        break;
                }
            }
            
            Destroy(this.gameObject);
        } 
    }
}

