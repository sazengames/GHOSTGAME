using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace GameCreator.Runtime.Localization
{
    [Title("Has Locale")]
    [Description("Returns true if the Localization Settings contains the chosen Locale")]

    [Category("Localization/Has Locale")]
    
    [Parameter("Locale", "The Locale to check its existence. Case insensitive")]

    [Keywords("Language", "Localization", "Idiom", "English")]
    
    [Image(typeof(IconLocalization), ColorTheme.Type.Yellow, typeof(OverlayTick))]
    [Serializable]
    public class ConditionLocalizationHasLocale : Condition
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private PropertyGetString m_Locale = new PropertyGetString("en");

        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override string Summary => $"Locale {this.m_Locale} exists";
        
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            string locale = this.m_Locale.Get(args).ToLowerInvariant();
            LocaleIdentifier id = new LocaleIdentifier(locale);

            return LocalizationSettings.AvailableLocales.GetLocale(id) != null;
        }
    }
}
