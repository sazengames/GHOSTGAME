using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace GameCreator.Runtime.Localization
{
    [Title("Compare Current Locale")]
    [Description("Returns true if the current Locale is equal to the specified")]

    [Category("Localization/Compare Current Locale")]
    
    [Parameter("Locale", "The Locale to compare to. Case insensitive")]

    [Keywords("Language", "Localization", "Idiom", "English")]
    
    [Image(typeof(IconLocalization), ColorTheme.Type.Blue)]
    [Serializable]
    public class ConditionLocalizationCurrentLocale : Condition
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private PropertyGetString m_Locale = new PropertyGetString("en");

        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override string Summary => $"Current Locale is {this.m_Locale}";
        
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            string current = LocalizationSettings.SelectedLocale.LocaleName.ToLowerInvariant();
            string compare = this.m_Locale.Get(args).ToLowerInvariant();

            return current == compare;
        }
    }
}
