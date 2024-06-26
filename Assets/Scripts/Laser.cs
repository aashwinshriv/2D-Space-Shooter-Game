using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.up * _speed * Time.deltaTime); 

       if (transform.position.y >= 7.5f)
       {
           if (transform.parent != null)
           {
               Destroy(transform.parent.gameObject);
           }
           Destroy(this.gameObject);
       }
    }
}
