using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalTrigger : MonoBehaviour
{
    public GameObject finalMenu; // Canvas-ul
    public GameObject player; // Playerul sau controllerul

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            finalMenu.SetActive(true);
            Time.timeScale = 0f; // Oprește timpul
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
