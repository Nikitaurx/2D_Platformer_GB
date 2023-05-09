using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class SpriteAnimatorController : IDisposable
    {
        private sealed class Animation
        {
            public AnimState track;
            public List<Sprite> sprites;
            public bool loop;
            public float speed = 10;
            public float counter = 0;
            public bool sleep;

            public void Update()
            {
                if (sleep) return;

                counter += Time.deltaTime * speed;

                if (loop)
                {
                    while (counter > sprites.Count)
                    {
                        counter -= sprites.Count;
                    }
                }
                else if (counter > sprites.Count)
                {
                    counter = sprites.Count;
                    sleep = true;
                }
            }
        }

        private AnimationConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimatorController(AnimationConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState Track, bool Loop, float Speed)
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.loop = Loop;
                animation.speed = Speed;
                animation.counter = 0;
                animation.sleep = false;

                if (animation.track != Track)
                {
                    animation.track = Track;
                    animation.sprites = _config.sequences.Find(sequence => sequence.track == Track).sprites;
                    animation.counter = 0;
                }
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new Animation()
                {
                    track = Track,
                    sprites = _config.sequences.Find(sequence => sequence.track == Track).sprites,
                    loop = Loop,
                    speed = Speed
                });
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
            {
                _activeAnimations.Remove(sprite);
            }
        }

        public void Update()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Update();
                if (animation.Value.counter < animation.Value.sprites.Count)
                {
                    animation.Key.sprite = animation.Value.sprites[(int)animation.Value.counter];
                }
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}

