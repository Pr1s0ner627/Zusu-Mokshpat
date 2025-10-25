using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public DiceRoller diceRoller;
    public PlayerController player;
    public AudioSource winSfx;

    private bool playerTurn = true;
    private bool gameOver = false;

    void Update()
    {
        if (playerTurn && Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            StartCoroutine(HandleTurn());
        }
    }

    private IEnumerator HandleTurn()
    {
        playerTurn = false;

        yield return StartCoroutine(diceRoller.RollDice());
        int result = diceRoller.GetRolledNumber();
        yield return StartCoroutine(player.MovePlayer(result));

        if (player.currentTile >= player.boardManager.tiles.Length - 1)
        {
            Debug.Log("🎉 Player Escaped the Manor!");
            winSfx?.Play();
            gameOver = true;
        }

        playerTurn = true;
    }
}
