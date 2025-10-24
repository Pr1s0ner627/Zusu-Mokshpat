using UnityEngine;
using System.Collections;

public class DiceRoller : MonoBehaviour
{
    public int rolledNumber { get; private set; }
    public float rollDuration = 1.5f;
    public AudioSource rollSfx;

    private bool isRolling = false;

    public IEnumerator RollDice()
    {
        if (isRolling) yield break;
        isRolling = true;

        rollSfx?.Play();

        float timer = 0f;
        while (timer < rollDuration)
        {
            rolledNumber = Random.Range(1, 7);
            timer += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
        Debug.Log($"Dice Rolled: {rolledNumber}");
    }
}
