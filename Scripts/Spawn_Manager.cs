using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    private bool _stopSpawning = false;
    
    [SerializeField]
    private GameObject[] powerups;
    private float _waittime = 2.0f;
    private float _waitmultiplier = 0.90f;
    private float _endPosition = 11.5f;   
    

    void Start()
    {
       
        
    }

    public void StarttheGame()
    {
        
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerupSpawn());
        StartCoroutine(SpeedupEnemies());
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 PosToSpawn = new Vector3(Random.Range(-_endPosition, _endPosition), 7f, 0);          
            GameObject newEnemy = Instantiate(_EnemyPrefab, PosToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
        
            yield return new WaitForSeconds(_waittime);
            Debug.Log("The wait time is: " + _waittime);
        }
    }

     IEnumerator SpeedupEnemies()
    {

        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(5.0f);
            if (_waittime >= 0.2f)
            {
                _waittime *= _waitmultiplier;
            }
        }
        
    }

    IEnumerator PowerupSpawn()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(0f, 10f));
            int randomPowerup = Random.Range(0,3);
            GameObject tripshot = Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-_endPosition, _endPosition), 9f, 0), Quaternion.identity);
            
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
