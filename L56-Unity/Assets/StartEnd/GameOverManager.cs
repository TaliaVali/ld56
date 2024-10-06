using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public GameObject player; // Referenz zum Spieler
    public TextMeshProUGUI gameOverText; // TextMeshPro-Text für die Game-Over-Nachricht
    public GameObject gameOverUI; // UI-Element, das beim Game Over angezeigt wird

   

    // Start is called before the first frame update
    void Start()
    {
        // Sicherstellen, dass die Game Over UI anfangs deaktiviert ist
        gameOverUI.SetActive(false);

       // Sicherstellen, dass das GameOver UI deaktiviert ist
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void OnPlayerDeath()
    {
        // Spieler deaktivieren
        player.SetActive(false);

      
        
        // Game Over UI anzeigen
        gameOverUI.SetActive(true);

        gameOverText.gameObject.SetActive(true);

    }

    void Update()
    {
        // Neustart der Szene mit der Leertaste
        if (gameOverUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
