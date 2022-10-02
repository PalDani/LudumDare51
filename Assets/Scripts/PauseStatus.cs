using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStatus : MonoBehaviour
{
    public bool IsPaused;

    public static PauseStatus Instance;

    private void Awake()
    {
        Instance = this;
    }
}
