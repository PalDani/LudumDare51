using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private Transform particlesParent;
    [SerializeField] private Dictionary<string,ParticleSystem> effects;

    public static EffectManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        effects = new Dictionary<string, ParticleSystem>();
        foreach (Transform pObj in particlesParent)
        {
            print("Loading effect: " + pObj.name);
            effects.Add(pObj.name,pObj.GetComponent<ParticleSystem>());
        }
        print("Loaded " + effects.Count + " effects.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEffect(string name)
    {
        effects[name].Play();
    }

    public void StopEffect(string name)
    {
        effects[name].Stop();
    }
}
