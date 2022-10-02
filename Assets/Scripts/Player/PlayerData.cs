using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public class PlayerStats
    {
        public int health = 10;
        public int points = 0;
        public float damage = 1;
    }

    [SerializeField] private PlayerStats stats;
    [SerializeField] private GameObject gameOverWindow;

    public static PlayerData Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        stats = new PlayerStats();
    }

    public void Die()
    {
        PauseStatus.Instance.IsPaused = true;
        CharacterController.Instance.gameObject.SetActive(false);
        gameOverWindow.SetActive(true);
    }

    public PlayerStats GetStats() => stats;
    public void ModifyHealth(int amount)
    {

        if (amount > 0)
        {
            CharacterController.Instance.sound.PlaySound("Heal");
            EffectManager.Instance.PlayEffect("Heal");
        }
        else
        {
            EffectManager.Instance.PlayEffect("Damage");
            CharacterController.Instance.sound.PlaySound("Damage");
            CameraShake.Shake(0.25f, 0.015f);
        }

        stats.health += amount;
        if (stats.health <= 0)
        {
            Die();
            print("Player dead. Game over.");
        }
    }
    public void ModifyPoint(int amount) => stats.points += amount;
    public void ModifyDamage(int amount) => stats.damage += amount / 10;
}
