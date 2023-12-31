using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts._Data.EnemyData;
using Game.Scripts._Data.LevelData;
using Game.Scripts._Data.Observer;
using Game.Scripts._Data.SceneData;
using Game.Scripts.Enemies.EnemyFactory;
using Game.Scripts.Enemies.EnemyStateMachine;
using Game.Scripts.Generic.Managers;
using Game.Scripts.Levels.GameMode;
using Game.Scripts.StaticUtilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Scripts.Levels
{
    public class LevelManager : MonoBehaviour
    {
        #region Statements
        
        public static Action OnLevelStart;
        public static Action OnLevelEnd;
        public static Action<string> OnLevelUpdate;
        
        public bool CanStartNextWave { get; set; }
        public int CurrentWaveIndex { get; set; }
        public int EnemiesAlive { get; private set; }
        public int CowAlive { get; set; }
        public List<SplineContainer> EnemyPaths { get; } = new();
        
        [Space, Title("Observers"), SerializeField] private ObserverEvent _mooCoinsObserver;
        [Space, Title("Observers"), SerializeField] private ObserverEvent _cowObserver;
        [Space, Title("LevelData"), SerializeField] private LevelData _levelData;
        [Space, Title("SceneData"), SerializeField] private SceneData _sceneData;
        
        private GameObject _enemiesParent;
        private IGameMode _currentGameMode;

        private void Awake()
        {
            _enemiesParent = GameObject.FindGameObjectWithTag(TagRef.EnemiesParent);
            _currentGameMode = gameObject.AddComponent<WaveGameMode>();
        }

        private void Start()
        {
            _currentGameMode.StartMode();
            
            OnLevelStart?.Invoke();
            _mooCoinsObserver.Notify(_levelData.MooCoinsStart);
            
            InvokeRepeat(5f, 1f);
        }

        #endregion

        #region Events

        private void OnDisable()
        {
            GameManager.Instance.ResetLevelMooCoins();
        }

        #endregion
        
        #region Game

        private void Repeat()
        {
            _currentGameMode.RepeatMode();
        }
        
        private void InvokeRepeat(float startTime, float repeatRate) => InvokeRepeating(nameof(Repeat), startTime, repeatRate);

        private void StopRepeat() => CancelInvoke(nameof(Repeat));

        #endregion

        #region Functions

        public IEnumerator SpawnEnemies(EnemyData enemyData, SplineContainer enemyPath, int enemyCount, int lifeIncrement)
        {
            for (var i = 0; i < enemyCount; i++)
            {
                var enemyGameObject = Instantiate(enemyData.Prefab, enemyPath.transform.position, Quaternion.identity, _enemiesParent.transform);
                var enemyStateMachine = enemyGameObject.GetComponent<EnemyStateMachine>();
                
                var newHealth = enemyData.BaseLife + enemyData.BaseLife / 3 * lifeIncrement;
                var enemy = EnemyFactory.Create(enemyData, newHealth);
        
                enemyStateMachine.Enemy = enemy;
                enemyStateMachine.EnemyPath = enemyPath;

                yield return new WaitForSeconds(enemy.SpawnRate);
            }
        }
        
        public void WinGame()
        {
            StopRepeat();
            OnLevelEnd?.Invoke();
            LoadScene();
        }
        
        public void LoseGame()
        {
            StopRepeat();
            OnLevelEnd?.Invoke();
            LoadScene();
        }
        
        private void LoadScene()
        {
            ScenesManager.LoadScene(_sceneData);
        }
        
        #endregion

        #region Waves

        public Wave GetCurrentWave(bool increment = false) => increment ? _levelData.Waves[++CurrentWaveIndex] : _levelData.Waves[CurrentWaveIndex];
        public bool IsLastWave() => CurrentWaveIndex + 1 >= _levelData.Waves.Length;

        #endregion

        #region Enemy

        public void EnemySpawned() => EnemiesAlive++;
        public void EnemyDied() 
        {
            EnemiesAlive--;
        }
        public void EnemyReachedEnd()
        {
            EnemyDied();
            KillCow();
        }
        public void CheckEnemiesAlive()
        {
            if (IsLastWave()) return;
            CanStartNextWave = EnemiesAlive <= 0;
        }
        
        private void KillCow()
        {
            CowAlive--;
            _cowObserver.Notify();
            
            CheckCowAlive();
        }
        private void CheckCowAlive()
        {
            if (CowAlive > 0) return;
            LoseGame();
        }

        #endregion
    }
}
