using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _firePoint;

    private float _timer;

    private void Awake()
    {
        _timer = _fireRate;
    }

    void Update()
    {
        //Movement
        Vector2 input;

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(input.x, input.y, 0f) * _speed;
        Vector3 newPosition = transform.position + velocity * Time.deltaTime;
        transform.localPosition = newPosition;

        //Shooting
        _timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (_timer <= 0f)
            {
                Shoot();
                _timer = _fireRate;
            }
        }
    }

    void Shoot()
    {
        Instantiate(_projectile, _firePoint.position, _firePoint.rotation);
    }
}
