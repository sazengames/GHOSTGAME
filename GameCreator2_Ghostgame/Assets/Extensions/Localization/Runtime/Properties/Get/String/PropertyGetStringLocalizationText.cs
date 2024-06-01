using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Localization;

namespace GameCreator.Runtime.Localization
{
    [Title("Localized Text")]
    [Category("Localization/Localized Text")]
    
    [Image(typeof(IconLocalization), ColorTheme.Type.Yellow)]
    [Description("Returns the localized value of a text")]
    
    [Serializable]
    public class PropertyGetStringLocalizationText : PropertyTypeGetString
    {
        [SerializeField] private LocalizedString m_Text = new LocalizedString();

        public override string Get(Args args) => this.m_Text.GetLocalizedString();

        public override string Get(GameObject gameObject) => this.m_Text.GetLocalizedString();

        public static PropertyGetString Create => new PropertyGetString(
            new PropertyGetStringLocalizationText()
        );

        public override string String => LocalizationUtils.ToString(
            this.m_Text.TableReference,
            this.m_Text.TableEntryReference
        );
    }
}