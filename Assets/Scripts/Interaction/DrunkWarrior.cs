using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DrunkWarrior : MonoBehaviour
{
    [Header("Drunk warrior settings")]
    [SerializeField] private float damageRange = 5;
    [SerializeField] private int minDamage = 5;
    [SerializeField] private int maxDamage = 10;
    [SerializeField] private LayerMask castLayerMask;
    [SerializeField] private ParticleSystem usableEffect;
    [SerializeField] private ParticleSystem castEffect;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        OnEnvChange();
    }

    public void UseDrunkWarrior()
    {
        if (!Winery.wineAcquired)
            return;

        Winery.wineAcquired = false;

        usableEffect.Stop();
        //castEffect.Play();
        animator.SetBool("Attack", true);
        
        
        StartCoroutine(DoDamage());
        StartCoroutine(StopAnimation());
        
    }

    private IEnumerator DoDamage()
    {
        yield return new WaitForSeconds(0.25f);
        var dmg = Random.Range(minDamage, maxDamage);
        

        var enemies = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), damageRange, castLayerMask);
        print(enemies.Length);
        print(dmg);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(dmg);
        }
    }

    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Attack", false);
    }

    public void OnEnvChange()
    {
        animator.SetBool("Attack", false);
        if (Winery.wineAcquired)
        {
            usableEffect.Play();
            
        } else
        {
            usableEffect.Stop();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }

}
