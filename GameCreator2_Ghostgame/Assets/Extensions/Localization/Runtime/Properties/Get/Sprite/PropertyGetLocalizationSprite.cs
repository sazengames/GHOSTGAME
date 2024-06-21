using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Localization;

namespace GameCreator.Runtime.Localization
{
    [Title("Localized Sprite")]
    [Category("Localization/Localized Sprite")]
    
    [Image(typeof(IconLocalization), ColorTheme.Type.Purple)]
    [Description("Returns the localized value of a Game Object")]
    
    [Serializable]
    public class PropertyGetLocalizationSprite : PropertyTypeGetSprite
    {
        [SerializeField] private LocalizedAsset<Sprite> m_Asset = new LocalizedAsset<Sprite>();

        public override Sprite Get(Args args) => this.m_Asset.LoadAsset();

        public override Sprite Get(GameObject gameObject) => this.m_Asset.LoadAsset();

        public static PropertyGetSprite Create => new PropertyGetSprite(
            new PropertyGetLocalizationSprite()
        );

        public override string String => LocalizationUtils.ToString(
            this.m_Asset.TableReference,
            this.m_Asset.TableEntryReference
        );
    }
}