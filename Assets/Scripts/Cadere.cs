using UnityEngine;

public class FallVoiceTrigger : MonoBehaviour
{
    public AudioClip fallVoiceClip;
    public LayerMask groundLayer;
    public float rayDistance = 1.5f;
    public float fallSpeedThreshold = -10f;

    private AudioSource audioSource;
    private StarterAssets.ThirdPersonController controller;
    private Vector3 lastPosition;
    private bool wasInAir = false;
    private float verticalSpeed = 0f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        controller = GetComponent<StarterAssets.ThirdPersonController>();
        lastPosition = transform.position;
    }

    void Update()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);

        verticalSpeed = (transform.position.y - lastPosition.y) / Time.deltaTime;

        if (!isGrounded)
        {
            wasInAir = true;
        }
        else if (wasInAir && verticalSpeed < fallSpeedThreshold)
        {
            PlayFallVoice();
            wasInAir = false;
        }
        else if (isGrounded)
        {
            wasInAir = false;
        }

        lastPosition = transform.position;
    }

    void PlayFallVoice()
    {
        if (fallVoiceClip != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(fallVoiceClip);
        }
    }
}
