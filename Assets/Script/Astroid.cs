using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    public float _rotationSpeed = 5f;
    public float _speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Translate(Vector3.down * _rotationSpeed * Time.deltaTime);
        // måste få astroiden att rotera.
    }
}
