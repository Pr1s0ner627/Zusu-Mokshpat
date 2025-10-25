using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class DiceRoller : MonoBehaviour
{
    [Header("Dice Roll Settings")]
    public float torqueForce = 8f;
    public float rollCooldown = 2.5f;
    public AudioSource rollSfx;

    private Rigidbody rb;
    private bool isRolling = false;
    private int rolledNumber;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.angularDamping = 0.5f;
    }

    public IEnumerator RollDice()
    {
        if (isRolling) yield break;
        isRolling = true;

        // Reset dice position and rotation
        transform.position = new Vector3(0f, 1.5f, 0f);
        transform.rotation = Random.rotation;

        // Add random torque & force
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
        rb.AddTorque(Random.onUnitSphere * torqueForce, ForceMode.Impulse);

        if (rollSfx != null)
            rollSfx.Play();

        // Wait for dice to stop moving
        yield return new WaitForSeconds(rollCooldown);
        yield return new WaitUntil(() => rb.IsSleeping());

        rolledNumber = GetNumberFromFace();
        Debug.Log($"🎲 Dice Landed On: {rolledNumber}");

        isRolling = false;
    }

    private int GetNumberFromFace()
    {
        // Approximate by checking which local axis points upward
        Vector3 up = transform.up;
        Vector3 down = -transform.up;
        Vector3 forward = transform.forward;
        Vector3 back = -transform.forward;
        Vector3 right = transform.right;
        Vector3 left = -transform.right;

        Vector3 worldUp = Vector3.up;

        float maxDot = -Mathf.Infinity;
        string face = "";

        // Compare all six directions to see which is facing up
        if (Vector3.Dot(up, worldUp) > maxDot) { maxDot = Vector3.Dot(up, worldUp); face = "up"; }
        if (Vector3.Dot(down, worldUp) > maxDot) { maxDot = Vector3.Dot(down, worldUp); face = "down"; }
        if (Vector3.Dot(forward, worldUp) > maxDot) { maxDot = Vector3.Dot(forward, worldUp); face = "forward"; }
        if (Vector3.Dot(back, worldUp) > maxDot) { maxDot = Vector3.Dot(back, worldUp); face = "back"; }
        if (Vector3.Dot(right, worldUp) > maxDot) { maxDot = Vector3.Dot(right, worldUp); face = "right"; }
        if (Vector3.Dot(left, worldUp) > maxDot) { maxDot = Vector3.Dot(left, worldUp); face = "left"; }

        // Assign your dice numbering based on which face is which
        switch (face)
        {
            case "up": return 1;
            case "down": return 6;
            case "forward": return 2;
            case "back": return 5;
            case "right": return 3;
            case "left": return 4;
            default: return Random.Range(1, 7);
        }
    }

    public int GetRolledNumber() => rolledNumber;
}
