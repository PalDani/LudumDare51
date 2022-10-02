using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="LDJAM/Power Up")]
public class Powerup : ScriptableObject
{
    public enum PowerupType
    {
        HP, DMG, SPEED, POINT, NOTHING
    }

    public PowerupType powerupType;
    public Sprite icon;
    public int valueMin = 1;
    public int valueMax = 2;

    public int value;

    public void ActivatePowerup()
    {
        switch (powerupType)
        {
            case PowerupType.HP:
                PlayerData.Instance.ModifyHealth(value);
                break;
            case PowerupType.DMG:
                break;
            case PowerupType.SPEED:
                CharacterController.Instance.ModifySpeed(value);
                break;
            case PowerupType.POINT:
                PlayerData.Instance.ModifyPoint(value);
                break;
            case PowerupType.NOTHING:
                //TODO: Play poop sound or something funny
                break;
            default:
                //Something went wrong
                break;
        }
        CameraFollow.Instance.globalSound.PlaySound("Powerup");
    }
}
