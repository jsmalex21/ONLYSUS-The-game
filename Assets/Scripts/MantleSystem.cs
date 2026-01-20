using UnityEngine;
using System.Collections;

public class MantleSystem : MonoBehaviour
{
    public float detectDistance = 2.5f; // mai mare pentru detectare din aer
    public float mantleHeight = 1.2f;
    public float mantleSpeed = 4f;
    public LayerMask climbableLayer;

    private bool isMantling = false;
    private Vector3 targetPosition;

    private CharacterController characterController;
    private StarterAssets.ThirdPersonController movementScript;
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementScript = GetComponent<StarterAssets.ThirdPersonController>();
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isMantling) return;

        // detectăm marginea
        if (DetectClimbable(out targetPosition))
        {
            // Dacă e pe sol: sare doar la apăsare Space
            // Dacă e în aer: poate face mantle automat
            if (Input.GetKeyDown(KeyCode.Space) || !movementScript.Grounded)
            {
                StartCoroutine(Mantle(targetPosition));
            }
        }
    }

    bool DetectClimbable(out Vector3 topPosition)
    {
        topPosition = Vector3.zero;

        // înălțăm ray-ul ca să nu lovească solul
        Vector3 rayOrigin = transform.position + Vector3.up * 1.3f;

        if (Physics.Raycast(rayOrigin, transform.forward, out RaycastHit hit, detectDistance, climbableLayer))
        {
            // verificăm dacă e spațiu deasupra obiectului
            Vector3 pointAbove = hit.point + Vector3.up * mantleHeight;

            if (!Physics.Raycast(pointAbove, Vector3.down, mantleHeight, climbableLayer))
            {
                topPosition = hit.point + Vector3.up * (mantleHeight + 0.1f);
                return true;
            }
        }

        return false;
    }

    IEnumerator Mantle(Vector3 destination)
    {
        isMantling = true;
        movementScript.enabled = false;
        characterController.enabled = false;

        if (animator) animator.SetTrigger("Mantle");

        float t = 0f;
        Vector3 startPos = transform.position;

        while (t < 1f)
        {
            t += Time.deltaTime * mantleSpeed;
            transform.position = Vector3.Lerp(startPos, destination, t);
            yield return null;
        }

        characterController.enabled = true;
        movementScript.enabled = true;
        isMantling = false;
    }
}
