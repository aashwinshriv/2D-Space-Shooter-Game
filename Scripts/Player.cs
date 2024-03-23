using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 20f;
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private float _FireRate = 0.05f;
    private float _CanFire = -1f;
    [SerializeField]
    private int _lives = 5;
    private Spawn_Manager _Spawn_Manager;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private bool _TripleShotActive = false;
    [SerializeField]
    private bool _ShieldisActive = false;
    [SerializeField]
    private GameObject _shieldVisualiser;
    [SerializeField]
    private int _shieldcount = 0;
    private UI_Manager _Ui_manager;
    [SerializeField]
    private GameObject _Explosionprefab;
    [SerializeField]
    private GameObject _LeftEngine, _RightEngine;
    private int _tripleshotcount = 0;
    [SerializeField]
    private GameObject _speedVisualizer;
    private bool _sonicShieldActive = false;
    private int _speedShieldCount = 0;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioData;
    private int _endPosition = 12;



    void Start()
    {
        
        _audioData = GetComponent<AudioSource>();
        if (_audioData == null)
        {
            Debug.Log("The audio data on the player is NULL");
        }
        else
        {
            _audioData.clip = _laserSoundClip;
        }

        transform.position = new Vector3(0, -2, 0);
    
        _Spawn_Manager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        if (_Spawn_Manager == null)
        {
            Debug.LogError("Spawn Manager is null");
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
       CalculateMovement();

       if (Input.GetKeyDown(KeyCode.Space) && Time.time > _CanFire)
        {
           FireLaser();
        }
    }

    void CalculateMovement()
    {
        float leftright = Input.GetAxis("Horizontal");
        float updown = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(leftright, updown, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.8f,2),0);

        if (transform.position.x < -_endPosition)
        {
            transform.position = new Vector3(_endPosition, transform.position.y, 0);
        }
        else if (transform.position.x > _endPosition)
        {
            transform.position = new Vector3(-_endPosition, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _CanFire = Time.time + _FireRate;
        _audioData.Play();
        
        if (_TripleShotActive)
        {
            switch (_tripleshotcount)
            {
                case 1:
                    Debug.Log("1 Triple Shots Active");
                    Instantiate(_TripleShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                    break;
                case 2:
                    Debug.Log("2 Triple Shots Active");
                    Instantiate(_TripleShotPrefab, transform.position + new Vector3(-0.5f, 0.8f, 0), Quaternion.identity); 
                    Instantiate(_TripleShotPrefab, transform.position + new Vector3(0.5f, 0.8f, 0), Quaternion.identity);
                    break;
                default:
                    Debug.Log("3 Tripleshots active");
                    Instantiate(_TripleShotPrefab, transform.position + new Vector3(-1f, 0.8f, 0), Quaternion.identity); 
                    Instantiate(_TripleShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity); 
                    Instantiate(_TripleShotPrefab, transform.position + new Vector3(1f, 0.8f, 0), Quaternion.identity);
                    break; 
            }
            
        }
        else
        {
            Instantiate(_LaserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
        
    }

    public void Damage()
    {
        if (_sonicShieldActive)
        {
            return;
        }
        
        if (_ShieldisActive == true)
        {
            if (_shieldcount !=0)
            {
                _shieldcount--;
                _Ui_manager.reduceShields();
            }
            
            if (_shieldcount == 0)
            {
                _ShieldisActive = false;
                _shieldVisualiser.SetActive(false);
            }
            
            return;
        }

        _lives--;

        _Ui_manager._lifeUpdater(_lives);

        if (_lives == 2)
        {
            _LeftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _RightEngine.SetActive(true);
        }

        else if (_lives == 0)
        {
            _Ui_manager.Gameovershow();
            _Spawn_Manager.OnPlayerDeath();
            Instantiate(_Explosionprefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);            
        }
    }

    public void TripleShot()
    {
        _TripleShotActive = true;
        _tripleshotcount++;
        StartCoroutine(Powerdownroutine());
    }
    public void Speedup()
    {
        _speed += 30f;
        _speedVisualizer.SetActive(true);
        _sonicShieldActive = true;
        _speedShieldCount++;
        StartCoroutine(SpeeddownRoutine());
        _Ui_manager.ActivateSpeedBoost();
    }

    public void Shield()
    {
        _shieldcount++;
        _Ui_manager.increaseShields();
        _ShieldisActive = true;
        _shieldVisualiser.SetActive(true);
    }

    IEnumerator SpeeddownRoutine()
    {
        yield return new WaitForSeconds(10f);
        _speed -= 27.5f;
        _speedShieldCount--;
        if (_speedShieldCount == 0)
        {
            _speedVisualizer.SetActive(false);
            _sonicShieldActive = false;
        }
    }
    IEnumerator Powerdownroutine()
    {
        yield return new WaitForSeconds(30.0f);
        
        _tripleshotcount--;
        if (_tripleshotcount == 0)
        {
            _TripleShotActive = false;
        }
    }

    //reduce speed
   
    


}
  