using UnityEngine;

public class FireworkTrigger : MonoBehaviour
{
    public GameObject fireworks;

    private void Start()
    {
        if (fireworks != null)
        {
            fireworks.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fireworks != null)
            {
                fireworks.SetActive(true);
                var ps = fireworks.GetComponent<ParticleSystem>();
                if (ps != null)
                    ps.Play();
            }
        }
    }
}
