using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace GameCreator.Runtime.Localization
{
    [Title("Change Language")]
    [Description("Changes the current language of the project")]

    [Category("Localization/Change Language")]

    [Keywords("Locale", "Idiom", "English")]

    [Image(typeof(IconLocalization), ColorTheme.Type.Yellow)]
    
    [Serializable]
    public class InstructionLocalizationChangeLocale : Instruction
    {
        [SerializeField] private PropertyGetString m_Locale = new PropertyGetString("en");
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Change Locale to {this.m_Locale}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            LocaleIdentifier id = new LocaleIdentifier(this.m_Locale.Get(args));
            Locale locale = LocalizationSettings.AvailableLocales.GetLocale(id);

            LocalizationSettings.SelectedLocale = locale;
            return DefaultResult;
        }
    }
}