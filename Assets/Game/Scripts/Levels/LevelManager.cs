using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enemies.EnemyStateMachine;
using Game.Scripts.Levels.GameMode;
using Game.Scripts.ScriptableObjects.LevelData;
using Game.Scripts.StaticUtilities;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Scripts.Levels
{
    public class LevelManager : MonoBehaviour
    {
        #region Statements
        
        public bool CanStartNextWave { get; set; }
        public int CurrentWaveIndex { get; set; }
        public int EnemiesAlive { get; set; }
        public List<SplineContainer> EnemyPaths { get; set; } = new();
        
        public LevelData LevelData;
        
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
            
            InvokeRepeating(nameof(Repeat), 5f, 1f);
        }

        #endregion
        
        #region GameMode

        private void Repeat()
        {
            _currentGameMode.RepeatMode();
        }

        #endregion

        #region Functions

        public Wave GetCurrentWave(bool increment = false)
        {
            if (increment) CurrentWaveIndex++;
            return LevelData.Waves[CurrentWaveIndex];
        }
        
        public int GetWavesCount() => LevelData.Waves.Length;

        public IEnumerator SpawnEnemies(GameObject enemyPrefab, SplineContainer enemyPath, int enemyCount, int lifeIncrement)
        {
            for (var i = 0; i < enemyCount; i++)
            {
                var enemyGameObject = Instantiate(enemyPrefab, enemyPath.transform.position, Quaternion.identity, _enemiesParent.transform);
                var enemyStateMachine = enemyGameObject.GetComponent<EnemyStateMachine>();
                var enemyData = enemyStateMachine.EnemyData;
        
                enemyStateMachine.Health = enemyData.BaseLife + enemyData.BaseLife / 2 * lifeIncrement;
                enemyStateMachine.EnemyPath = enemyPath;

                yield return new WaitForSeconds(enemyData.SpawnRate);
            }
        }
        
        #endregion

        #region Enemy

        public void EnemySpawned() => EnemiesAlive++;
        public void EnemyDied() => EnemiesAlive--;
        public void CheckEnemiesAlive() => CanStartNextWave = EnemiesAlive <= 0;

        #endregion
    }
}
