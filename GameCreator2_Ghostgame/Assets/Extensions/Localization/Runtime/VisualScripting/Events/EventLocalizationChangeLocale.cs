using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace GameCreator.Runtime.Localization
{
    [Title("On Change Language")]
    [Category("Localization/On Change Language")]
    [Description("Executed when the locale of the Unity Localization changes")]

    [Image(typeof(IconLocalization), ColorTheme.Type.Yellow)]
    
    [Keywords("Locale", "Localization", "Idiom", "English")]
    
    [Serializable]
    public class EventLocalizationChangeLocale : Event
    {
        protected override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);
            LocalizationSettings.SelectedLocaleChanged += this.OnChangeLocale;
        }

        protected override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);
            LocalizationSettings.SelectedLocaleChanged -= this.OnChangeLocale;
        }

        private void OnChangeLocale(Locale newLocale)
        {
            _ = this.m_Trigger.Execute(this.Self);
        }
    }
}
