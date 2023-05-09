using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public enum AnimState
    {
        idle = 0,
        run = 1,
        jump = 2
    }

    [CreateAssetMenu(fileName = "SpriteAnimatorCfg", menuName = "Configs / Animation", order = 1)]
    public class AnimationConfig : ScriptableObject
    {
        [Serializable]
        public class SpriteSequence
        {
            public AnimState track;
            public List<Sprite> sprites = new List<Sprite>();
        }

        public List<SpriteSequence> sequences = new List<SpriteSequence>();
    }
}

