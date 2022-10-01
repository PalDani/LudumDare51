using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public enum CharacterDirection { LEFT = 1, UP = 2, RIGHT = 3, DOWN = 4, }

    [SerializeField] private int pointPrize = 1;

    [Header("Enemy data")]
    [SerializeField] protected float health = 3;
    [SerializeField] protected float movementSpeed = 1;
    [SerializeField] protected float attackSpeed = 1;
    [SerializeField] protected float attackRange = 1;
    [SerializeField] protected int attackDamage = 1;
    [SerializeField] protected float attackCooldown = 1;
    [SerializeField] protected bool canAttack = true;

    [Header("Enemy effects")]
    [SerializeField] protected ParticleSystem attackEffect;
    [SerializeField] protected ParticleSystem takeDamageEffect;
    [SerializeField] protected ParticleSystem dieEffect;
    [SerializeField] protected ParticleSystem knockbackEffect;

    [Header("Animations")]
    [SerializeField] protected Animator animator;
    
    public Transform target;
    private Vector2 targetPosition;
    private Vector2 selfPosition;
    private Rigidbody2D rb;
    public float distance;
    public CharacterDirection direction;
    public Vector2 directionVec;
    public float angle;
    public Vector2 moveDir;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        target = CharacterController.Instance.transform;

        tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    
    void Update()
    {
        targetPosition = new Vector2(target.position.x, target.position.y);
        selfPosition = new Vector2(transform.position.x, transform.position.y);
        distance = GetDistanceToTarget();

        GetDirectionToPlayer();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void LateUpdate()
    {
        animator.SetFloat("DirectionX", moveDir.x);
        animator.SetFloat("DirectionY", moveDir.y);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    private void FollowPlayer()
    {
        MoveTowardsPlayer();

        if(IsInAttackRange() && canAttack)
        {
            StartCoroutine(StartAttack());
        }
    }

    private void MoveTowardsPlayer()
    {
        moveDir = (targetPosition - selfPosition).normalized;
        rb.velocity = moveDir * movementSpeed;
        Debug.DrawRay(transform.position, moveDir);
    }

    private float GetDistanceToTarget()
    {
        return Vector2.Distance(selfPosition, targetPosition);
    }

    private void GetDirectionToPlayer()
    {
        var res = (targetPosition - selfPosition).normalized;
        directionVec = res;
        angle = Vector2.Angle(Vector2.left, res);

        if (res.x < 0 && angle < 45)
        {
            direction = CharacterDirection.LEFT;
        } else if (res.y > 0 && angle > 45 && res.x < 0.7f)
        {
            direction = CharacterDirection.UP;
        } else if (res.x > 0 && angle > 135 && res.y < 0.7f)
        {
            direction = CharacterDirection.RIGHT;
        } else
        {
            direction = CharacterDirection.DOWN;
        }
    }

    private bool IsInAttackRange() => GetDistanceToTarget() <= attackRange;

    protected virtual Collider2D CastAttackArea()
    {
        return Physics2D.OverlapCircle(selfPosition, attackRange);
    }

    protected virtual void Attack()
    {
        //TODO: Get the forward of the enemy and cast the attack there, so the player can dash
        //TODO: Play animation, particle effect and sound effect
        var result = CastAttackArea();
        if (result != null)
        {
            canAttack = false;
            PlayerData.Instance.ModifyHealth(-attackDamage);
        } else
        {
            AttackMissed();
        }

        animator.ResetTrigger("Attack");
        StartCoroutine(AttackCooldown());
    }

    protected IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackSpeed);
        if (IsInAttackRange() && canAttack)
        {
            Attack();
            animator.SetTrigger("Attack");
        }
            
    }

    protected virtual void AttackMissed()
    {
        print("Attack missed"); 
        //TODO: Play miss effect?
    }

        protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void Die()
    {
        PlayerData.Instance.ModifyPoint(pointPrize);
        //TODO: Play die sound and effect, leave blood splat
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health >= 0)
        {
            Die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
