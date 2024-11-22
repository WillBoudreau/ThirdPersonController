using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Language
{
    public string lang;
    public string title;
    public string play;
    public string quit;
    public string options;
    public string credits;
    public string OptionsPrompt;
    public string OptionsEnglish;
    public string OptionsFrench;
}

public class LanguageData
{
    public Language[] languages;
}

public class Reader : MonoBehaviour
{
    public TextAsset jsonFile;
    public string currentLanguage;
    private LanguageData languageData;

    public Text titleText;
    public Text playText;
    public Text quitText;
    public Text optionsText;
    public Text creditsText;
    public Text OptionsPrompt;
    public Text OptionsEnglish;
    public Text OptionsFrench;

    public void Start()
    {
        languageData = JsonUtility.FromJson<LanguageData>(jsonFile.text);
        SetLanguage(currentLanguage);
    }
    public void SetLanguage(string newLanguage)
    {
        foreach(Language language in languageData.languages)
        {
            if(language.lang.ToLower() == newLanguage.ToLower())
            {
                titleText.text = language.title;
                playText.text = language.play;
                quitText.text = language.quit;
                optionsText.text = language.options;
                creditsText.text = language.credits;
                OptionsPrompt.text = language.OptionsPrompt;
                OptionsEnglish.text = language.OptionsEnglish;
                OptionsFrench.text = language.OptionsFrench;
                return;
            }
        }
    }
    public void SetEnglish()
    {
        SetLanguage("en");
    }
    public void SetFrench()
    {
        SetLanguage("fr");
    }   
}
