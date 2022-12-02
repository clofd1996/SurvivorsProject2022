using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    enum GiantState : int
    {
        Idle = 0,
        Chasing = 1,
        Attacking = 2,
        Injury = 3,
        Berserk = 4
    }

    [SerializeField] GameObject knifePrefab;
    private Animator animator;

    GiantState giantState = GiantState.Idle;
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
            case GiantState.Idle:
                waitTimer -= Time.deltaTime;
                if (waitTimer <=0)
                {
                    giantState = GiantState.Chasing;
                }
                break;
            case GiantState.Chasing:
                base.Update();
                float distance = Vector3.Distance(transform.position, player.transform.position);
                animator.SetBool("IsWalking", true);
                if (distance < 5f)
                {
                    giantState = GiantState.Attacking;
                }
                break;
            case GiantState.Attacking:
                animator.SetBool("IsWalking", false);
                animator.SetTrigger("Attack");
                giantState = GiantState.Idle;
                waitTimer = 5f;
                break;
            //case GiantState.Injury:
            //    base.Update();
            //    float hpRatio = HP / MaxHP;
            //    if (hpRatio)
            //    else if (hpRatio <= 0.5) // Under 50% HP
            //    {
            //        giantState = GiantState.Berserk; // goes into Berserk state
            //    }
            //    break;
            case GiantState.Berserk:
                //base.Update(); // only stays in Berserk
                animator.SetTrigger("Attack"); // continuously be in attacking state                
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
            giantState = GiantState.Idle;
        }
        else if (hpRatio <= 0.5)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            giantState = GiantState.Berserk;
        }

    }

    public void SpawnKnife()
    {
        Instantiate(knifePrefab, transform.position, Quaternion.identity);
    }
}
