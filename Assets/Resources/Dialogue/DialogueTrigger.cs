using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueController _dialogueController;
    [SerializeField]List<Dialogue> d;
    private void OnTriggerEnter2D(Collider2D other)
    {

        Queue<Dialogue> dialogues = new Queue<Dialogue>();
        for (int i = 0; i < d.Count; i++)
        { dialogues.Enqueue(new Dialogue(d[i].Name, d[i].Sentence, d[i].PhotoPath, d[i].PhotoPathR)); }
        
        _dialogueController.Talk(dialogues);

        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _dialogueController.CloseDialogue();
    }
    void Start()
    {
        _dialogueController = GameObject.FindWithTag("GameController")
            .GetComponent<DialogueController>();
    }
}
