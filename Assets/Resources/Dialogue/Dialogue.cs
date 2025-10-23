

using System;
using UnityEngine;
[Serializable]
public class Dialogue
{

    private string _photoPath = "UI/CharacterPhotos";


    public  string Name;
    public  string PhotoPath;
    public  string PhotoPathR;
   [TextArea] public  string Sentence;



    public Dialogue(string name, string sentence = "", string photoPath = null, string photoPathR = null)
    {
        Name = name;
        photoPath = photoPath ?? "Default";
        PhotoPath = $"{_photoPath}/{photoPath}";
        photoPathR = photoPathR ?? "Default";
        PhotoPathR = $"{_photoPath}/{photoPathR}";
        Sentence = sentence;


    }
}
