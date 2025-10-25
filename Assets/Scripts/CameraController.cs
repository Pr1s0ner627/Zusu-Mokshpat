using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform dice;
    public float smoothSpeed = 2f;
    public Vector3 offset = new Vector3(0f, 6f, -6f);

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private bool isFocusing = false;

    void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (isFocusing || player == null) return;

        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(player);
    }

    public IEnumerator FocusOn(Transform target, float duration = 2f)
    {
        isFocusing = true;
        Vector3 focusPos = target.position + new Vector3(0, 3f, -2f);

        float t = 0;
        while (t < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, focusPos, t);
            transform.LookAt(target);
            t += Time.deltaTime * smoothSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(duration);

        // Return to default view
        t = 0;
        while (t < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + offset, t);
            transform.LookAt(player);
            t += Time.deltaTime * smoothSpeed;
            yield return null;
        }

        isFocusing = false;
    }
}
