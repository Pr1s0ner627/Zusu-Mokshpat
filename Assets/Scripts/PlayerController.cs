using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int currentTile = 0;
    public float moveSpeed = 3f;
    private BoardManager boardManager;
    private bool isMoving = false;

    private void Start()
    {
        boardManager = Object.FindFirstObjectByType<BoardManager>();
        if (boardManager == null)
        {
            Debug.LogError("BoardManager not found in scene.");
            return;
        }
        transform.position = boardManager.GetTilePosition(currentTile);
    }

    public IEnumerator MovePlayer(int steps)
    {
        if (isMoving) yield break;
        isMoving = true;

        int targetTile = currentTile + steps;
        if (targetTile >= boardManager.tiles.Length)
            targetTile = boardManager.tiles.Length - 1;

        for (int i = currentTile + 1; i <= targetTile; i++)
        {
            Vector3 targetPos = boardManager.GetTilePosition(i);
            yield return StartCoroutine(MoveToTile(targetPos));
        }

        currentTile = targetTile;

        // Check for hidden doors or pitfalls
        int newTile = boardManager.CheckForSpecialTile(currentTile);
        if (newTile != currentTile)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 targetPos = boardManager.GetTilePosition(newTile);
            yield return StartCoroutine(MoveToTile(targetPos));
            currentTile = newTile;
        }

        isMoving = false;
    }

    private IEnumerator MoveToTile(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }
}
