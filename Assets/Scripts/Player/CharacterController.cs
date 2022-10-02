using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{

    public enum CharacterDirection { LEFT = 1, UP = 2, RIGHT = 3, DOWN = 4, }

    [Header("Basic character movement")]
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private float movementSpeedBoostMultiplier = 0.2f;
    [SerializeField] private float dashSpeed = 2;
    [SerializeField] private float dashCooldown = 1;
    [SerializeField] private float attackCooldown = .25f;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;

    [Header("Interaction")] 
    [SerializeField] private float interactionRange = 2;
    [SerializeField] private LayerMask interactionMask;

    [Header("Manually assigned components")]
    [SerializeField] private GameObject attackCollider;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Animator animator;
    
    [Header("Automatically assigned components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D collider;
    [SerializeField] private Vector2 playerDir;
    [SerializeField] private float animatorMovementLastNonZeroX;
    public SoundPlayer sound;

    [Header("Runtime data")]
    [SerializeField] private Vector2 velocity;
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private Vector2 playerPos;
    [SerializeField] private float dashCurrentCooldown = 0;
    [SerializeField] private CharacterDirection direction;

    public static CharacterController Instance;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        sound = GetComponent<SoundPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dashCurrentCooldown = dashCooldown;
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseStatus.Instance.IsPaused)
            return;

        GetInput();
        GetDirection();
    }

    private void FixedUpdate()
    {
        if (PauseStatus.Instance.IsPaused)
            return;

        velocity = new Vector2(playerDir.x * movementSpeed, playerDir.y * movementSpeed) * (isDashing ? dashSpeed * Time.deltaTime: 1);
        rb.velocity = velocity;


        animator.SetFloat("Speed", playerDir.sqrMagnitude);
        if(rb.velocity != Vector2.zero)
        {
            if((playerDir.x < 0 && playerDir.y > 0))
                animator.SetFloat("MovementX", playerDir.x + playerDir.y - 0.01f);
            else
                animator.SetFloat("MovementX", playerDir.x + playerDir.y);
        }

        if (isDashing)
        {
            CharacterController.Instance.sound.PlaySound("Dash");
            animator.SetTrigger("Dash");
        }

        isDashing = false;
        animator.ResetTrigger("Dash");
        animator.ResetTrigger("Attack");
    }

    private void GetInput()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        playerPos = new Vector2(transform.position.x, transform.position.y);

        playerDir = new Vector2(dirX, dirY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
        
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void GetDirection()
    {
        var res = velocity;

        if (res.x < 0 && res.y < 4.25f && res.y > -4.25f)
        {
            direction = CharacterDirection.LEFT;
        }
        else if (res.x > 0 && res.y < 4.25f && res.y > -4.25f)
        {
            direction = CharacterDirection.RIGHT;
        }
        else if (res.y > 0 && res.x < 4.25f && res.x > -4.25f)
        {
            direction = CharacterDirection.UP;
        } else
        {
            direction = CharacterDirection.DOWN;
        }
    }

    private void Attack()
    {
        print("Attack");

        //TODO: Create attack effect towards direction
        //CameraShake.Shake(0.25f, 0.015f);
        animator.SetTrigger("Attack");

        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void Dash()
    {
        if (canDash)
        {
            //EffectManager.Instance.PlayEffect("Dash");
            print("Dash");
            canDash = false;
            isDashing = true;
            StartCoroutine(DashCooldown());
            StartCoroutine(DashTrail());
        }
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(1);
        dashCurrentCooldown -= 1;

        if (dashCurrentCooldown <= 0)
        {
            canDash = true;
            dashCurrentCooldown = dashCooldown;
        }
        else StartCoroutine(DashCooldown());
    }

    private IEnumerator DashTrail()
    {
        collider.enabled = false;
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(0.5f);
        trailRenderer.emitting = false;
        collider.enabled = true;
    }

    private void Interact()
    {
        var hit = Physics2D.OverlapCircle(transform.position, interactionRange, interactionMask);
        if (hit != null)
        {
            InteractableObject obj = hit.GetComponent<InteractableObject>();

            if (obj != null)
            {
                print("Interacted " + obj.name);
                obj.onInteraction.Invoke();
            }
        }
    }

    public void ModifySpeed(int amount) => movementSpeed += amount * movementSpeedBoostMultiplier;
}
