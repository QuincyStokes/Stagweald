
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update

    public TrapperDialogue trapperDialogue;
    public TMP_Text dialogueText;
    public AudioMixerGroup dialogueAMG;


    void OnEnable()
    {
        SetRandomInteractionDialogue();
    }

    public void SetRandomInteractionDialogue()
    {
        print(trapperDialogue.interactDialogue.Count);
        int randQuote;
        if(trapperDialogue.interactDialogue.Count > 0)
        {
            randQuote = Random.Range(0, trapperDialogue.interactDialogue.Count);
            dialogueText.text = trapperDialogue.interactDialogue[randQuote];
            AudioManager.Instance.PlayOneShot(trapperDialogue.interactDialogueAudio[randQuote], 1.5f, dialogueAMG, AudioManager.Instance.trapperAudioSource);
        }   
        print("Playing " + trapperDialogue.interactDialogueAudio[5].name);
        
    }
}
