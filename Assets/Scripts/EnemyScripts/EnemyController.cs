using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private Animator myAnim;
    [SerializeField] private NavMeshAgent agent;


    private bool amiDead = false;
    GameObject player;
    private PlayerController _player;
    Transform target;
    


    private float attackCd = 0f;
    private float currentHealth;
    void Start()
    {
         player = GameObject.Find("Player");
        _player = player.GetComponent<PlayerController>();
        currentHealth = _enemyStats.Health;
        target = player.transform;
    }


    void Update()
    {
            attackCd -= Time.deltaTime;
            Movement();
        if (_gameStats.isGameOver == true)
        {
            Destroy(gameObject);
        }

    }

    private void Movement()
    {
        myAnim.SetBool("isAttack", false);
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance < _enemyStats.StoppingDistance && amiDead == false)
        {
            lookTarget();
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isAttack", true);
            Attack();
        }
        if (distance <= _enemyStats.Range && distance > _enemyStats.StoppingDistance && amiDead == false)
        {
            agent.SetDestination(target.position);
            myAnim.SetBool("isRunning", true);
        }
    }

    private void lookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

   private void Attack()
    {
        if (attackCd <= 0f)
        {
            _player.GetDamage(_enemyStats.AttackPower);
            attackCd = 1f / _enemyStats.AttackSpeed;
        }
        
    }

    public void getDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        amiDead = true;
        myAnim.SetBool("isDead", true);
        Destroy(gameObject, 2f);
    }
}
