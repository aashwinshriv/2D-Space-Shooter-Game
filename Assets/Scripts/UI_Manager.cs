using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _shieldText;
    private int _shieldcount = 0;
    private int _score = 0;
    [SerializeField]
    private Image[] _Lifecount;
    [SerializeField]
    private Sprite _noLife;
    [SerializeField]
    private Text _gameover;
    [SerializeField]
    private Text _restartLevel;
    private GameManager _Game_Manager;
    [SerializeField]
    private Text _speedBoostText;
   // private bool _speedBoostActive = false;
    private float _speedBoostTimer = 10f;
    private int _speedCoroutine = 0;




    void Start()
    {
        _scoreText.text = "Score: 0";
        _shieldText.text = "Shield Count: 0";
        _Game_Manager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        

        if (_Game_Manager == null)
        {
            Debug.Log("The Game Manager or Speed boost UI is null");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        _speedBoostText.text = "Speed Boost: " + _speedBoostTimer + " s";
    }
    public void increaseScore()
    {
        _score += 10;
        _scoreText.text = "Score: "+ _score;
    }

    IEnumerator Countdownspeed()
    {
        
        while (_speedBoostTimer > 0)
        {
            
            yield return new WaitForSeconds(1);
            _speedBoostTimer -= 1;
            
        }
        _speedBoostText.gameObject.SetActive(false);
        _speedCoroutine = 0;
    }

    public void ActivateSpeedBoost()
    {
        _speedBoostTimer = 10f;
        _speedCoroutine++;
        _speedBoostText.gameObject.SetActive(true);
        if (_speedCoroutine == 1)
        {
            StartCoroutine(Countdownspeed());
        }
        else if (_speedCoroutine > 1)
        {
            return;
        }
        

    }

    public void _SpeedReduction()
    {
        _speedBoostTimer--;
        _speedBoostText.text = "Speed Boost: " + _speedBoostTimer + " s";
    }
    public void increaseShields()
    {
        _shieldcount++;
        _shieldText.text = "Shield Count: " + _shieldcount;
    }

    public void reduceShields()
    {
        _shieldcount--;
        _shieldText.text = "Shield Count: " + _shieldcount;
    }

    public void _lifeUpdater(int lifenumber)
    {
        _Lifecount[lifenumber].sprite = _noLife;
    }

    public void Gameovershow()
    {
        _gameover.gameObject.SetActive(true);
        _restartLevel.gameObject.SetActive(true);
        _Game_Manager.Restarter();
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameover.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameover.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
    
}

