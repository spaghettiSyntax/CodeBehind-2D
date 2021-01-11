// (~˘▾˘)~ spaghettiSyntax
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour
{
    public static NumberWizard instance;

    [SerializeField] private Image sorcererDialogue = null;
    [SerializeField] private TMP_Text sorcererText = null;
    [SerializeField] private TMP_Text guessText = null;

    [SerializeField] private Image warriorDialogue = null;
    [SerializeField] private TMP_Text warriorText = null;

    [SerializeField] private int min;
    [SerializeField] private int max;
    
    private int guess;
    public int guessCounter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        guessCounter = 0;
        sorcererDialogue.enabled = true;
        warriorDialogue.enabled = false;
        warriorText.enabled = false;
        NextGuess();
    }

    public void OnPressHigher()
    {
        min = guess;
        NextGuess();
    }

    public void OnPressLower()
    {
        max = guess;
        NextGuess();
    }

    public void OnPressSuccess()
    {
        SceneLoader.instance.LoadNextScene();
    }

    void NextGuess()
    {
        guessCounter++;
        int rand = Random.Range(1, 4);
        guess = Random.Range(min, max + 1);
        switch (rand)
        {
            case 1:
                sorcererText.text = "It has to be";
                guessText.text = guess.ToString() + "!";
                break;
            case 2:
                sorcererText.text = "Is it";
                guessText.text = guess.ToString() + "...";
                break;
            case 3:
                sorcererText.text = "What about";
                guessText.text = guess.ToString() + "?";
                break;
            default:
                break;
        }
        
        if (guessCounter == 5)
        {
            warriorDialogue.enabled = true;
            warriorText.enabled = true;

            warriorText.text = "Can we eat, yet?";
        }
        else
        {
            warriorDialogue.enabled = false;
            warriorText.enabled = false;
        }
    }
}
