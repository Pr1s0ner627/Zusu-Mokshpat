using UnityEngine;

public class DiceZoneController : MonoBehaviour
{
    public Transform player;
    public float zoneRadius = 1.5f;  // minimum safe distance from player
    public Vector3 zoneCenterOffset = new Vector3(0f, 1f, 0f);

    [Header("References")]
    public GameObject diceArena;  // Assign your dice area (Box Collider)
    public DiceRoller diceRoller; // Assign dice script

    private Vector3 originalZonePos;

    void Start()
    {
        if (diceArena != null)
            originalZonePos = diceArena.transform.position;
    }

    void Update()
    {
        if (player == null || diceArena == null) return;

        float distance = Vector3.Distance(player.position, diceArena.transform.position);

        // Reposition dice arena away from player if too close
        if (distance < zoneRadius)
        {
            // Find a nearby free tile by offsetting from player
            Vector3 offset = (Random.insideUnitSphere * 3f);
            offset.y = 0;
            diceArena.transform.position = player.position + offset + zoneCenterOffset;
        }
    }

    public void ResetZone()
    {
        if (diceArena != null)
            diceArena.transform.position = originalZonePos;
    }
}
