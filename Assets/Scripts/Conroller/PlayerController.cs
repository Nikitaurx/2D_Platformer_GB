using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private ContactPooler _contactPooler;
        private SpriteAnimatorController _playerAnimator;
        private AnimationConfig _config;
        private LevelObjectView _playerView;
        private Transform _playerT;
        private Rigidbody2D _rb;

        private bool _isJump;
        private bool _isMoving = false;

        private float _walkSpeed = 5f;
        private float _animationSpeed = 14f;
        private float _movingTreshold = 0.1f;
        private float _jumpForce = 8f;
        private float _jumpTreshold = 1f;
        private float _xAxisInput;
        private float _yVelocity = 0;
        private float _xVelocity = 0;
        private int _health = 100;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        public PlayerController(InteractiveObjectView playerView)
        {
            _playerView = playerView;
            _playerT = playerView._transform;
            _rb = _playerView._rb;

            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_config);
            _playerAnimator.StartAnimation(playerView._spriteRenderer, AnimState.Run, true, _animationSpeed);
            _contactPooler = new ContactPooler(_playerView._collider);

            playerView.TakeDamage += TakeBullet;
            playerView.FinishCheck += FinishRenderer;

        }
        private void TakeBullet(BulletView bullet)
        {
            _health -= bullet.DamagePoint;
        }
        private void FinishRenderer(FinishView finish)
        {
            _playerView._spriteRenderer.enabled = false;
        }

        private void MoveTowards()
        {
            _xVelocity += Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
            _rb.velocity = new Vector2(_xVelocity, _yVelocity);
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;

        }
        public void Update()
        {
            if (_health <= 0)
            {
                _health = 0;
                _playerView._spriteRenderer.enabled = false;
            }
            _playerAnimator.Update();
            _contactPooler.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _yVelocity = _rb.velocity.y;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;

            _playerAnimator.StartAnimation(_playerView._spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

            if (_isMoving)
            {
                Debug.Log("Moving");
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
                _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
            }

            if (_contactPooler.IsGrounded)
            {
                Debug.Log("Jump");
                if (_isJump && _yVelocity <= _jumpTreshold)
                {

                    _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpTreshold)
                {
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
            }


        }
    }

}