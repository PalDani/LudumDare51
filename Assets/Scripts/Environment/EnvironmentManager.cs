using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{

    public List<GameObject> environmentObjects;

    public void ChangeEnvironment()
    {
        CameraShake.Shake(1, 0.25f);
        for (int i = 0; i < environmentObjects.Count - 1; i++)
        {
            int changeTargetIndex = i;
            while (changeTargetIndex == i)
                changeTargetIndex = Random.Range(0, environmentObjects.Count);

            GameObject current = environmentObjects[i];
            GameObject target = environmentObjects[changeTargetIndex];

            Vector3 tmp = target.transform.position;
            target.transform.position = current.transform.position;
            current.transform.position = tmp;
        }
        print("Env changed");
    }
}
