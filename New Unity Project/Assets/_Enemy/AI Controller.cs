using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private EnemyView enemyView;
    public Transform target;
    public Transform patrolTarget;
    public UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private GameObject sword;
    private bool swordSwung = false;
    
    
    void Update()
    {
        if(enemyView.canSeePlayer == true)
        {
            if(enemyView.playerDistance > 2)
            {
                agent.SetDestination(target.position);
            }
            else if(enemyView.playerDistance < 2)
            {
                transform.LookAt(target);
                swordSwung = !swordSwung;
                sword.GetComponent<Animator>().SetBool("IsSwung", swordSwung);
                StartCoroutine(SwingTime());
            }
            enemyView.angle = 360;
        }
        else
        {
            agent.SetDestination(patrolTarget.position);
            enemyView.angle = 120;
        }
    }

    private IEnumerator SwingTime()
    {
        yield return new WaitForSeconds(1f);
        swordSwung = !swordSwung;
        sword.GetComponent<Animator>().SetBool("IsSwung", swordSwung);
    }
}