using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    enum BossState : int
    {
        Idle = 0,
        Chasing = 1,
        Attacking = 2,
        Dash = 3
    }

    [SerializeField] GameObject fireballPrefab;
    private Animator animator;

    BossState giantState = BossState.Idle;
    float waitTimer = 2f;
    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        switch (giantState)
        {
            case BossState.Idle:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    giantState = BossState.Chasing;
                }
                break;
            case BossState.Chasing:
                base.Update();
                float distance = Vector3.Distance(transform.position, player.transform.position);
                animator.SetBool("IsWalking", true);
                if (distance < 5f)
                {
                    giantState = BossState.Attacking;
                }
                break;
            case BossState.Attacking:
                animator.SetBool("IsWalking", false);
                animator.SetTrigger("Attack");
                giantState = BossState.Idle;
                waitTimer = 3f;
                break;

            case BossState.Dash:
                animator.SetTrigger("Dash"); // continuously be in attacking state                
                break;
            default:
                break;
        }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        float hpRatio = HP / MaxHP;
        if (hpRatio > 0.5)
        {
            giantState = BossState.Idle;
        }
        else if (hpRatio <= 0.5)
        {
            giantState = BossState.Dash;
        }

    }

    public void SpawnFireball()
    {
        //Instantiate(knifePrefab, transform.position, Quaternion.identity);
        var fireballPrefab = PoolManager.GetInstance().fireballpool.Get();
        fireballPrefab.transform.position = transform.position;
        fireballPrefab.transform.rotation = Quaternion.identity;
        fireballPrefab.SetActive(true);
    }
}
