﻿using System;
using System.Threading.Tasks;
using DaimahouGames.Runtime.Core;
using DaimahouGames.Runtime.Core.Common;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace DaimahouGames.Runtime.Abilities
{
    [Title("Activator")]
    [Image(typeof(IconChip), ColorTheme.Type.Gray)]
    
    [Serializable]
    public abstract class AbilityActivator : AbilityStrategy
    {
        //============================================================================================================||
        // ※  Variables: -------------------------------------------------------------------------------------------|※
        // ---|　Exposed State ----------------------------------------------------------------------------------->|
        
        [SerializeField] private bool m_FaceTarget;
        
        // ---|　Internal State ---------------------------------------------------------------------------------->|
        // ---|　Dependencies ------------------------------------------------------------------------------------>|
        // ---|　Properties -------------------------------------------------------------------------------------->|
        
        public bool FaceTarget => m_FaceTarget;
        
        // ---|　Events ------------------------------------------------------------------------------------------>|
        //============================================================================================================||
        // ※  Constructors: ----------------------------------------------------------------------------------------|※
        // ※  Initialization Methods: ------------------------------------------------------------------------------|※
        // ※  Public Methods: --------------------------------------------------------------------------------------|※
        
        public abstract Task Activate(ExtendedArgs args);
        
        // ※  Virtual Methods: -------------------------------------------------------------------------------------|※
        // ※  Protected Methods: -----------------------------------------------------------------------------------|※
        // ※  Private Methods: -------------------------------------------------------------------------------------|※
        //============================================================================================================||
    }
}