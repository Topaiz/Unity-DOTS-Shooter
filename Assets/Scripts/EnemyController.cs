using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;


    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }
}
