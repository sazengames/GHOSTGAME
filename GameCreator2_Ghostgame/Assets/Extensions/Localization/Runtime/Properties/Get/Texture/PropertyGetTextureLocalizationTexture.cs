using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Localization;

namespace GameCreator.Runtime.Localization
{
    [Title("Localized Texture")]
    [Category("Localization/Localized Texture")]
    
    [Image(typeof(IconLocalization), ColorTheme.Type.Blue)]
    [Description("Returns the localized value of a Game Object")]
    
    [Serializable]
    public class PropertyGetTextureLocalizationTexture : PropertyTypeGetTexture
    {
        [SerializeField] private LocalizedAsset<Texture> m_Asset = new LocalizedAsset<Texture>();

        public override Texture Get(Args args) => this.m_Asset.LoadAsset();

        public override Texture Get(GameObject gameObject) => this.m_Asset.LoadAsset();

        public static PropertyGetTexture Create => new PropertyGetTexture(
            new PropertyGetTextureLocalizationTexture()
        );

        public override string String => LocalizationUtils.ToString(
            this.m_Asset.TableReference,
            this.m_Asset.TableEntryReference
        );
    }
}