using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform player;
    NavMeshAgent agent;

    [SerializeField] float health = 30f;

    [SerializeField] float damage = 10f;

    Score score;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>().transform;
        agent = GetComponent<NavMeshAgent>();
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position; 
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        score.AddScore(100);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<FPSController>().TakeDamage(damage);
        }
    }
}
