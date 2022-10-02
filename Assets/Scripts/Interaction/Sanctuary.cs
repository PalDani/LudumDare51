using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundPlayer))]
public class Sanctuary : MonoBehaviour
{
    [Header("Sanctuary settings")]
    [SerializeField] private float escapeTime = 5;
    [SerializeField] private float castRange = 10;
    [SerializeField] private int castDamageMin = 1;
    [SerializeField] private int castDamageMax = 3;
    [SerializeField] private LayerMask castLayerMask = 10;
    [SerializeField] private bool canUse = true;
    [SerializeField] private int envChangeRefersh = 0;
    [SerializeField] private ParticleSystem usableEffect;
    [SerializeField] private ParticleSystem castEffect;


    [SerializeField] private int envChangeCount = 0;

    private SoundPlayer sound;

    private void Awake()
    {
        sound = GetComponent<SoundPlayer>();
    }

    public void CastSanctuary()
    {
        if (!canUse)
            return;

        print("Sanctuary used");

        canUse = false;
        usableEffect.Stop();

        castEffect.Play();
        sound.PlaySound("SanctuaryCast");

        var enemies = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), castRange, castLayerMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            int dmg = Random.Range(castDamageMin, castDamageMax);
            enemies[i].GetComponent<Enemy>().TakeDamage(dmg);
        }
    }

    public void OnEnvironmentChange()
    {
        if(!canUse)
        {
            envChangeCount += 1;
        }

        if(envChangeCount >= envChangeRefersh)
        {
            envChangeCount = 0;

            canUse = true;
            usableEffect.Play();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, castRange);
    }
}
