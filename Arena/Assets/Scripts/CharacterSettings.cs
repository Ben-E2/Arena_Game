using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSettings", menuName = "Scriptable Objects/Character Settings")]
public class CharacterSettings : ScriptableObject
{
    public string team;

    public float movementSpeed;
    public float maxHealth;

    public float minDistanceFromPlayer;
    public float maxDistanceFromPlayer;

    public bool isPlayer;
    public bool isRanged;
    public bool isPredictive;

    public GameObject Prefab;

    public int spawnCost;
}
