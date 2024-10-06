using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartManager : MonoBehaviour
{

    public GameObject player; // Der Player-Character
    public TextMeshProUGUI startText; // Der Startbildschirm-Text
   
    

    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialer Zustand: Player deaktiviert, Startbildschirm-Text aktiviert
        player.SetActive(false);
        startText.gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Überprüfen, ob die Leertaste gedrückt wird und das Spiel noch nicht gestartet wurde
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        // Player aktivieren und Startbildschirm-Text deaktivieren
        player.SetActive(true);
        startText.gameObject.SetActive(false);
        gameStarted = true;
    }
}
