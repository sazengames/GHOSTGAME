using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Localization;

namespace GameCreator.Runtime.Localization
{
    [Title("Localized Game Object")]
    [Category("Localization/Localized Game Object")]
    
    [Image(typeof(IconLocalization), ColorTheme.Type.Blue)]
    [Description("Returns the localized value of a Game Object")]
    
    [Serializable]
    public class PropertyGetSpriteLocalizationGameObject : PropertyTypeGetGameObject
    {
        [SerializeField] private LocalizedAsset<GameObject> m_Asset = new LocalizedAsset<GameObject>();

        public override GameObject Get(Args args) => this.m_Asset.LoadAsset();

        public override GameObject Get(GameObject gameObject) => this.m_Asset.LoadAsset();

        public static PropertyGetGameObject Create => new PropertyGetGameObject(
            new PropertyGetSpriteLocalizationGameObject()
        );

        public override string String => LocalizationUtils.ToString(
            this.m_Asset.TableReference,
            this.m_Asset.TableEntryReference
        );
    }
}