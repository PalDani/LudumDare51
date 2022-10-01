using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    public static PowerupManager Instance;
    [SerializeField] private Powerup[] powerups;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPowerups();
    }


    public Powerup[] GetPowerupsForShrine()
    {
        var r1 = Random.Range(0, powerups.Length);
        var r2 = Random.Range(0, powerups.Length);

        while (r1 == r2)
        {
            r2 = Random.Range(0, powerups.Length);
        }

        var p1 = powerups[r1];
        p1.value = Random.Range(p1.valueMin, p1.valueMax);

        var p2 = powerups[r2];
        p2.value = Random.Range(p2.valueMin, p2.valueMax);

        return new Powerup[2] { p1, p2 };
    }

    private void LoadPowerups() {
        powerups = Resources.LoadAll<Powerup>("Powerups");
        print("Loaded " + powerups.Length + " powerups.");
    } 
}
