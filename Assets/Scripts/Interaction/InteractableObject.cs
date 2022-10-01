using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent onInteraction;

    private void Start()
    {
        tag = "Interactable";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
