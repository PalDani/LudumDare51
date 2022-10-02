using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winery : MonoBehaviour
{

    private bool wineAcquired = false;

    [SerializeField] private ParticleSystem effect; 

    // Start is called before the first frame update
    void Start()
    {
        CheckWineAcrquireState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWine()
    {
        if(wineAcquired != true)
        wineAcquired = true;
        EffectManager.Instance.PlayEffect("Wine Acquired");
        CameraFollow.Instance.globalSound.PlaySound("WineAcquire");
        CheckWineAcrquireState();
    }

    public void CheckWineAcrquireState()
    {
        if(wineAcquired)
        {
            effect.Stop();
        } else
        {
            effect.Play();
        }
    }
}
