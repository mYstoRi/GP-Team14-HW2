using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 direction = Vector3.zero;

    public float timeUntilDestroy = 30f;
    public float speed = 5f;
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, timeUntilDestroy);
    }

    void Update()
    {
        transform.position = transform.position + direction.normalized*speed*Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerEntity playerEntity = other.gameObject.GetComponent<PlayerEntity>();
            playerEntity.TakesDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 _direction)
    {
        direction = _direction;
    }
}
