
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update

    public TrapperDialogue trapperDialogue;
    public TMP_Text dialogueText;


    void OnEnable()
    {
        SetRandomInteractionDialogue();
    }

    public void SetRandomInteractionDialogue()
    {
        print(trapperDialogue.interactDialogue.Count);
        if(trapperDialogue.interactDialogue.Count > 0)
        {
            dialogueText.text = trapperDialogue.interactDialogue[Random.Range(0, trapperDialogue.interactDialogue.Count)];
        }   
    }
}
