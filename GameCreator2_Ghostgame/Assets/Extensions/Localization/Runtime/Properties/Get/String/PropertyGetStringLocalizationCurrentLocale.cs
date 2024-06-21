using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace GameCreator.Runtime.Localization
{
    [Title("Current Locale")]
    [Category("Localization/Current Locale")]
    
    [Image(typeof(IconLocalization), ColorTheme.Type.Blue)]
    [Description("Returns the current locale value")]
    
    [Example(
        "Locales are two-letter values that indicate the language. " +
        "For example, 'en' means 'English', 'es' refers to 'Spanish', etc..."
    )]
    
    [Serializable]
    public class PropertyGetStringLocalizationCurrentLocale : PropertyTypeGetString
    {
        public override string Get(Args args) => LocalizationSettings.SelectedLocale.Identifier.Code;

        public override string Get(GameObject gameObject) => LocalizationSettings.SelectedLocale.Identifier.Code;

        public static PropertyGetString Create => new PropertyGetString(
            new PropertyGetStringLocalizationCurrentLocale()
        );

        public override string String => "Current Locale";
    }
}