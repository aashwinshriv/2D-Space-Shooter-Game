using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private bool _isGameOver = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            Debug.Log("R key pressed");
            SceneManager.LoadScene("Game");
        }
    }

    public void Restarter()
    {
        _isGameOver = true;
    }
}
