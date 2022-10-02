using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PortalGate : MonoBehaviour
{

    [SerializeField] private GameObject portalWindow;
    [SerializeField] private TMP_Text statistics;

    private string GenerateStatisticsText()
    {
        return $"Collected points: { PlayerData.Instance.GetStats().points }<br>Survived Waves: {WaveManager.Instance.waveCount}";
    }

    public void ShowPortalWindow()
    {
        PauseStatus.Instance.IsPaused = true;

        statistics.text = GenerateStatisticsText();
        portalWindow.SetActive(true);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ContinueGame()
    {
        PauseStatus.Instance.IsPaused = false;
        portalWindow.SetActive(false);
    }
}
