using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellOfLife : MonoBehaviour
{
    [Header("Well settings")]
    [SerializeField] private int healMin = 1;
    [SerializeField] private int healMax = 3;
    [SerializeField] private bool canUse = true;
    [SerializeField] private int envChangeRefersh = 0;
    [SerializeField] private ParticleSystem usableEffect;

    [SerializeField] private int envChangeCount = 0;

    public void UseWell()
    {
        if (!canUse)
            return;

        print("Well of life used");
        canUse = false;
        usableEffect.Stop();

        int heal = Random.Range(healMin, healMax);
        PlayerData.Instance.ModifyHealth(heal);
    }

    public void OnEnvironmentChange()
    {
        if (!canUse)
        {
            envChangeCount += 1;
        }

        if (envChangeCount >= envChangeRefersh)
        {
            envChangeCount = 0;

            canUse = true;
            usableEffect.Play();
        }
    }
}
