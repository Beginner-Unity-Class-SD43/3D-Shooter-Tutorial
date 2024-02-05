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

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>().transform;
        agent = GetComponent<NavMeshAgent>();
        score = FindObjectOfType<Score>();

        SetRigidbodyState(true);
        SetColliderState(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            agent.destination = player.position;
        }
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
        isDead = true;
        agent.speed = 0f;
        GetComponent<Animator>().enabled = false;
        SetRigidbodyState(false);
        SetColliderState(true);
        score.AddScore(100);
        Destroy(gameObject, 3f);
    }

    void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            collision.gameObject.GetComponent<FPSController>().TakeDamage(damage);
        }
    }
}
