using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewTrapperDialogue", menuName = "Data/TrapperDialogue")]
public class TrapperDialogue : ScriptableObject
{
    [TextArea] public List<string> interactDialogue;
    [TextArea] public List<string> randomDialogue;

    public List<AudioClip> interactDialogueAudio;
    public List<AudioClip> randomDialogueAudio;
}
