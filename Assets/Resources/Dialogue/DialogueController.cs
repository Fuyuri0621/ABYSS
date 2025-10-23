using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private Coroutine _typing;


    [SerializeField] private GameObject _continueButton;

    [SerializeField] private GameObject _dialoguePanel;

    [SerializeField] private Image _characterImage;

    [SerializeField] private Image _characterImageR;

    private Queue<Dialogue> _dialogues;

    [SerializeField]private TextMeshProUGUI _dialogueText;

    [SerializeField]private TextMeshProUGUI _nameText;

    private float _wordSpeed=0.05f;

    private IEnumerator Type()
    {
        Dialogue dialogue = _dialogues.Dequeue();
      //  _nameText.text = dialogue.Name;
        _characterImage.sprite = Resources.Load<Sprite>(dialogue.PhotoPath);
        _characterImageR.sprite = Resources.Load<Sprite>(dialogue.PhotoPathR);

        _characterImage.color = Color.white;
        _characterImageR.color = Color.white;

       // if ("UI/CharacterPhotos/"+dialogue.Name!= dialogue.PhotoPath) _characterImage.color = Color.gray; else _characterImageR.color = Color.gray;


        _dialogueText.text = string.Empty;

        foreach (char letter in dialogue.Sentence.ToCharArray())
        {
            _dialogueText.text += letter;

            yield return new WaitForSeconds(_wordSpeed);
        }

        

            _continueButton.SetActive(true);


        
    }

    public void CloseDialogue()
    {
        StopCoroutine(_typing);
        _dialoguePanel.SetActive(false);
    }

    public void SpeakNextSentence()
    {
        _continueButton.SetActive(false);

        if (_dialogues.Count > 0)
        {
            _typing = StartCoroutine(Type());
        }
        else
        {
            CloseDialogue();


        }
    }

    public void Talk(Queue<Dialogue> dialogues)
    {
        _dialogues = dialogues;
        _dialoguePanel.SetActive(true);
        _typing = StartCoroutine(Type());
    }
}
