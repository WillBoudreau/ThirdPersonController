using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizedTextComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string UI2;
    [SerializeField] private string localizationKey;

    private LocalizedString localizedSTR;
    private Text textComponent;
    void Start()
    {
        textComponent = GetComponent<Text>();
        localizedSTR = new LocalizedString { TableReference = UI2, TableEntryReference = localizationKey };

        LocalizationSettings.SelectedLocaleChanged += UpdateText;

        //var FrenchLocale = LocalizationSettings.AvailableLocales.GetLocale("fr");

        //LocalizationSettings.SelectedLocale = FrenchLocale;

        //UpdateText(FrenchLocale);
    }
    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= UpdateText;
    }

    void UpdateText(Locale locale)
    {
        textComponent = GetComponent<Text>();
        textComponent.text = localizedSTR.GetLocalizedString();
        textComponent = GetComponent<Text>();
    }
}
