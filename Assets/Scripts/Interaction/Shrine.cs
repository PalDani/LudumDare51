using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shrine : MonoBehaviour
{
    [Header("Shrine settings")]
    public bool isAvailable = true;

    [Header("UI")]
    public GameObject shrineWindow;
    [Header("First card")]
    public TMP_Text p1_name;
    public TMP_Text p1_description;
    public Image p1_icon;
    public Powerup current_p1;
    [Header("Second card")]
    public TMP_Text p2_name;
    public TMP_Text p2_description;
    public Image p2_icon;
    public Powerup current_p2;

    public void OpenShrine()
    {
        var cards = PowerupManager.Instance.GetPowerupsForShrine();
        SetCards(cards[0], cards[1]);
        SetWindowState(true);
    }

    private void SetCards(Powerup p1, Powerup p2)
    {
        p1_name.text = p1.name;
        //p1_icon.sprite = p1.icon;
        p1_description.text = $"This boost adds {p1.valueMin} - {p1.valueMax} to your {p1.powerupType} stat.";
        current_p1 = p1;

        p2_name.text = p2.name;
        //p2_icon.sprite = p2.icon;
        p2_description.text = $"This boost adds {p2.valueMin} - {p2.valueMax} to your {p2.powerupType} stat.";
        current_p2 = p2;
    }

    public void SetWindowState(bool status)
    {
        shrineWindow.SetActive(status);
    }

    public void ChooseCard(int number)
    {
        if (number == 1)
        {
            current_p1.ActivatePowerup();
        } else
        {
            current_p2.ActivatePowerup();
        }

        isAvailable = false;
        SetWindowState(false);
    }

    public void ResetShrine()
    {
        isAvailable = true;
    }
}

