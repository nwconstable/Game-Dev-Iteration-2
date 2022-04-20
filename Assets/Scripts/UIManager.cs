using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button showButtonsButton;
    [SerializeField] private Button[] buttons;
    [SerializeField] private TMP_Text treasureCount;
    [SerializeField] private int startingTreasure;
    private bool areButtonsShown;
    private TMP_Text buttonText;
    private int treasure;
    public static UIManager instance;

    private int Treasure{
        get{
            return treasure;
        }
        set{
            treasure = value;
            if(treasure <= 0){
                LoadingScreen.LoadScene("MainMenu");
            }
            UpdateTreasureText();
        }
    }

    void Awake() {
        areButtonsShown = false;
        foreach(Button button in buttons) {
            button.gameObject.SetActive(areButtonsShown);
        }
        instance = this;
        treasure = startingTreasure;
        UpdateTreasureText();
    }


    void Start()
    {
        buttonText = showButtonsButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = "Add New Turret";
        showButtonsButton.onClick.AddListener(() => {
            if(areButtonsShown) {
                areButtonsShown = false;
                foreach(Button button in buttons) {
                    button.gameObject.SetActive(areButtonsShown);
                }
                buttonText.text = "Add New Turret";
            } else {
                areButtonsShown = true;
                foreach(Button button in buttons) {
                    button.gameObject.SetActive(areButtonsShown);
                }
                buttonText.text = "Hide Turret Buttons";
            }
        });
    }

    private void UpdateTreasureText() {
        treasureCount.text = Treasure.ToString();
    }

    public void IncrementTreasure() {
        Treasure += 100;
    }

    public void DecrementTreasure() {
        Treasure -= 100;
    }
}
