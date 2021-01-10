// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class SO_State : ScriptableObject
{
    [TextArea(10, 14)][SerializeField] private string storyText = null;

    [SerializeField] private SO_State[] nextState = null; 

    public string GetStoryState()
    {
        return storyText;
    }

    public SO_State[] GetNextState()
    {
        return nextState;
    }
}