using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button showButtonsButton;
    [SerializeField] private Button[] buttons;
    private bool areButtonsShown = false;
    private TMP_Text buttonText;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Button button in buttons) {
            button.gameObject.SetActive(false);
        }
        buttonText = showButtonsButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = "Add New Turret";
        showButtonsButton.onClick.AddListener(() => {
            if(areButtonsShown) {
                foreach(Button button in buttons) {
                    button.gameObject.SetActive(false);
                }
                areButtonsShown = false;
                buttonText.text = "Add New Turret";
            } else {
                foreach(Button button in buttons) {
                    button.gameObject.SetActive(true);
                }
                areButtonsShown = true;
                buttonText.text = "Hide Turret Buttons";
            }
        });
    }
}
