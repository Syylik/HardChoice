using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Translates", menuName = "YagirLib/Translates", order = 1)]
public class LangListObjects : ScriptableObject
{
    public List<string> languages = new List<string>();
    public List<WordKey> words = new List<WordKey>();
}
