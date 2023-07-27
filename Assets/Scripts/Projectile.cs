using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timer;


    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
