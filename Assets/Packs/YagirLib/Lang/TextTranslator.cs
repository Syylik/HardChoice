using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTranslator : MonoBehaviour
{
    public string key;

    private void Start()
    {
        LangsList.langs.activatedTexts.Add(this);
        ReTranslate();
    }

    private void OnDestroy()
    {
        LangsList.langs.activatedTexts.Remove(this);
    }
    public void ReTranslate()
    {
        var tmpT = GetComponent<TMP_Text>();
        var T = GetComponent<Text>();

        if (tmpT)
        {
            tmpT.text = LangsList.GetWord(key);
        }
        if (T)
        {
            T.text = LangsList.GetWord(key);
        }
    }
}
