
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using System.Collections;
using Unity.VisualScripting;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update

    public TrapperDialogue trapperDialogue;
    public TMP_Text dialogueText;
    public AudioMixerGroup dialogueAMG;
    private bool isCoroutineRunning;
    public int randomDialogueCooldown;
    private AudioClip currentInteractionDialogue;
    private AudioClip currentRandomDialogue;
    

    void Update()
    {
        if(!isCoroutineRunning)
        {
            StartCoroutine(RandomDialogueCountdown());
        }
    }

    void OnEnable()
    {
        SetRandomInteractionDialogue();
    }

    public void SetRandomInteractionDialogue()
    {
        print(trapperDialogue.interactDialogue.Count);
        //declare int for index
        int randQuote;
        //if we have any dialogues
        if(trapperDialogue.interactDialogue.Count > 0)
        {
            //generate an index for dialogue
            randQuote = Random.Range(0, trapperDialogue.interactDialogue.Count);
            
            //check if the randomly selected dialogue is the same as what we just played, avoids playing same quote twice in a row
            if(trapperDialogue.interactDialogueAudio[randQuote] != currentInteractionDialogue)
            {
                //if it's a new quote, set the text and play the quote
                dialogueText.text = trapperDialogue.interactDialogue[randQuote];
                currentInteractionDialogue = trapperDialogue.interactDialogueAudio[randQuote];
                AudioManager.Instance.PlayOneShot(trapperDialogue.interactDialogueAudio[randQuote], 1.5f, dialogueAMG, AudioManager.Instance.trapperAudioSource);
                //print("Playing " + trapperDialogue.interactDialogueAudio[5].name);
            }
            else //if it *is* the same quote, pick a different one randomly again
            {
                SetRandomInteractionDialogue();
            }
           
        }   
       
        
    }

    public IEnumerator RandomDialogueCountdown()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(randomDialogueCooldown);
        AudioManager.Instance.PlayOneShot(trapperDialogue.randomDialogueAudio[Random.Range(0, trapperDialogue.randomDialogueAudio.Count)], 1f, dialogueAMG, AudioManager.Instance.trapperAudioSource);
        isCoroutineRunning = false;
    }
}
