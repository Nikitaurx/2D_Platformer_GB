using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _playerView;
        private AnimationConfig _config;
        private SpriteAnimatorController _playerAnimator;

        private void Awake()
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_config);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.idle, true, 12f);
        }
        void Update()
        {
            _playerAnimator.Update();
        }
    }
}
