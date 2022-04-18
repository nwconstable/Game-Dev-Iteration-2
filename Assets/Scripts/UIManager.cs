using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button showButtonsButton;
    [SerializeField] private Button[] buttons;
    private bool areButtonsShown;
    private TMP_Text buttonText;
    // Start is called before the first frame update

    void Awake() {
        areButtonsShown = false;
        foreach(Button button in buttons) {
            button.gameObject.SetActive(areButtonsShown);
        }
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
}
