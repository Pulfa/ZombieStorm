using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : DefaultEntity
{
    [SerializeField] public Transform player;
    public float speed;
    public float distanceBetween;
    public int reward = 10;


    private float distance;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (Health <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        GameManager.Instance.AddKill(reward);
        Destroy(gameObject);
    }
}
