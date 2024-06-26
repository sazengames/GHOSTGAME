using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Characters
{
    [Title("Character Bone")]
    [Category("Characters/Character Bone")]
    
    [Image(typeof(IconBoneSolid), ColorTheme.Type.Yellow)]
    [Description("The bone references on a Character game object")]

    [Serializable]
    public class GetGameObjectCharactersBone : PropertyTypeGetGameObject
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        
        [SerializeField] private Bone m_Bone = new Bone(HumanBodyBones.RightHand);
        
        public override GameObject Get(Args args)
        {
            Character character = this.m_Character.Get<Character>(args);
            if (character == null) return null;
            
            return character.Animim?.Animator != null 
                ? this.m_Bone.Get(character.Animim?.Animator) 
                : null;
        }

        public GetGameObjectCharactersBone(PropertyGetGameObject character, Bone bone)
        {
            this.m_Character = character;
            this.m_Bone = bone;
        }

        public GetGameObjectCharactersBone()
        {
            
        }
        
        public static PropertyGetGameObject Create(PropertyGetGameObject character, Bone bone)
        {
            return new PropertyGetGameObject(
                new GetGameObjectCharactersBone(character, bone)
            );
        }
        
        public override GameObject EditorValue
        {
            get
            {
                GameObject gameObject = this.m_Character.EditorValue;
                if (gameObject == null) return null;

                Character character = gameObject.GetComponent<Character>();
                if (character == null) return null;

                Animator animator = character.Animim?.Animator;
                if (animator == null) return null;
                
                Transform bone = this.m_Bone.GetTransform(animator);
                return bone != null ? bone.gameObject : null;
            }
        }

        public override string String => $"{this.m_Character}/{this.m_Bone}";
    }
}