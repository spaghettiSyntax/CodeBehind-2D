// (~˘▾˘)~ spaghettiSyntax
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public static IntroManager instance;

    [SerializeField] private Image sorcererDialogue = null;
    [SerializeField] private TMP_Text sorcererText = null;
    [SerializeField] private Image sorceressDialogue = null;
    [SerializeField] private TMP_Text sorceressText = null;
    [SerializeField] private Image warriorThoughtBubble = null;
    [SerializeField] private TMP_Text warriorText = null;
    [SerializeField] private TMP_Text startContinueButtonText = null;

    [HideInInspector] public int introDialogueCounter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        introDialogueCounter = -1;
        CheckStartContinueButtonText();

        sorcererDialogue.enabled = true;
        sorcererText.enabled = true;
        sorceressDialogue.enabled = false;
        sorceressText.enabled = false;
        warriorThoughtBubble.enabled = false;
        warriorText.enabled = false;
    }

    void Update()
    {
        CheckForUserInput();
    }

    private void CheckForUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextDialogue();
        }
    }

    public void StartContinueButton()
    {
        if (startContinueButtonText.text == "Continue")
        {
            LoadNextDialogue();
        }
        else
        {
            SceneLoader.instance.LoadNextScene();
        }
    }

    public void LoadNextDialogue()
    {
        introDialogueCounter++;

        CheckStartContinueButtonText();

        switch (introDialogueCounter)
        {
            case 0:
                sorcererDialogue.enabled = false;
                sorcererText.enabled = false;

                sorceressDialogue.enabled = true;
                sorceressText.enabled = true;

                sorceressText.text = "I am too...";
                break;
            case 1:
                sorceressDialogue.enabled = false;
                sorceressText.enabled = false;

                warriorThoughtBubble.enabled = true;
                warriorText.enabled = true;

                warriorText.text = "Not going to tell them I could disarm it... could go for some food.";
                warriorText.fontStyle = FontStyles.Italic;
                break;
            case 2:
                warriorThoughtBubble.enabled = false;
                warriorText.enabled = false;

                sorcererDialogue.enabled = true;
                sorcererText.enabled = true;

                sorcererText.text = "How about a game?";
                break;
            case 3:
                sorcererDialogue.enabled = false;
                sorcererText.enabled = false;

                sorceressDialogue.enabled = true;
                sorceressText.enabled = true;

                sorceressText.text = "I mean... sure?";
                break;
            case 4:
                sorceressDialogue.enabled = false;
                sorceressText.enabled = false;

                warriorThoughtBubble.enabled = true;
                warriorText.enabled = true;

                warriorText.text = "I'd much rather eat...";
                warriorText.fontStyle = FontStyles.Italic;
                break;
            case 5:
                warriorThoughtBubble.enabled = false;
                warriorText.enabled = false;

                sorcererDialogue.enabled = true;
                sorcererText.enabled = true;

                sorcererText.text = "Think of a number between 1 and 1000...";
                break;
            case 6:
                SceneLoader.instance.LoadNextScene();
                break;
            default:
                break;
        }
    }

    private void CheckStartContinueButtonText()
    {
        if (introDialogueCounter < 5)
        {
            startContinueButtonText.text = "Continue";
        }
        else
        {
            startContinueButtonText.text = "Start!";
        }
    }
}
