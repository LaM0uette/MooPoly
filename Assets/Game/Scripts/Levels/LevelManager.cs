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
            
            InvokeRepeat(5f, 1f);
        }

        #endregion
        
        #region Game

        private void Repeat()
        {
            _currentGameMode.RepeatMode();
        }
        
        private void InvokeRepeat(float startTime, float repeatRate) => InvokeRepeating(nameof(Repeat), startTime, repeatRate);
        
        public void StopRepeat() => CancelInvoke(nameof(Repeat));

        #endregion

        #region Functions

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

        #region Waves

        public Wave GetCurrentWave(bool increment = false) => increment ? LevelData.Waves[++CurrentWaveIndex] : LevelData.Waves[CurrentWaveIndex];
        public bool IsLastWave() => CurrentWaveIndex + 1 >= LevelData.Waves.Length;

        #endregion

        #region Enemy

        public void EnemySpawned() => EnemiesAlive++;
        public void EnemyDied() => EnemiesAlive--;

        public void CheckEnemiesAlive()
        {
            if (IsLastWave()) return;
            CanStartNextWave = EnemiesAlive <= 0;
        }

        #endregion
    }
}