using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button openCredits;
    [SerializeField] private Button closeCreditsButton;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private Button openInstructions;
    [SerializeField] private Button closeInstructions;
    [SerializeField] private GameObject instructionsPanel;

    void Awake() {
        Cursor.lockState = CursorLockMode.None;
    }

    // Start is called before the first frame update
    void Start() 
    {
        startButton.onClick.AddListener(() => { LoadingScreen.LoadScene("GameScreen"); } );

        openCredits.onClick.AddListener(() => { creditsPanel.SetActive(true); } );
        closeCreditsButton.onClick.AddListener(() => { creditsPanel.SetActive(false); } );

        openInstructions.onClick.AddListener(() => { instructionsPanel.SetActive(true); } );
        closeInstructions.onClick.AddListener(() => { instructionsPanel.SetActive(false); } );
    }
}
