using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text pointText;
    [SerializeField] private TMP_Text timertText;

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        //TODO: If time left, put Instances into vars
        healthText.text = "HP\t" + PlayerData.Instance.GetStats().health;
        damageText.text = "Dmg\t" + PlayerData.Instance.GetStats().damage;
        pointText.text = "Points\t" + PlayerData.Instance.GetStats().points;
        timertText.text = "Time left<br>" + Timer.Instance.GetCurrentTime();
    }
}
