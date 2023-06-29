using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView _playerView;
        [SerializeField] private CannonView _cannonView;

        //
        [SerializeField] private QuestObjectView _singleQuestItem;
        private QuestController _questController;
        //

        private PlayerController _playerController;
        private CannonController _cannonController;
        private EmitterController _emitterController;
        private CameraController _cameraController;

        [SerializeField] private AIConfig _config;
        [SerializeField] private EnemyView _enemyView;

        private SimplePatrolAI _simplePatrolAI;
        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _cannonController = new CannonController(_cannonView._muzzleT, _playerView._transform);
            _emitterController = new EmitterController(_cannonView._bullets, _cannonView._emitterT);
            _simplePatrolAI = new SimplePatrolAI(_enemyView, new SimplePatrolAIModel(_config));
            _cameraController = new CameraController(_playerView, Camera.main.transform);

            //
            _questController = new QuestController(_playerView, new QuestCoinModel(), _singleQuestItem);
            _questController.Reset();
            //
        }
        void Update()
        {
            _playerController.Update();
            _cannonController.Update();
            _emitterController.Update();
            _cameraController.Update();

            _simplePatrolAI.FixedUpdate();
        }
    }
}
