using System.Collections;
using Game.Scripts.Generic.Functions;
using UnityEngine;

namespace Game.Scripts.Levels.GameMode
{
    public class WaveGameMode : MonoBehaviour, IGameMode
    {
        #region Statements

        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = GetComponent<LevelManager>();
        }

        #endregion

        #region GameMode

        public void StartMode()
        {
            LevelManager.OnLevelUpdate?.Invoke("Start");
            
            _levelManager.CanStartNextWave = true;
            _levelManager.CurrentWaveIndex = -1;
        }

        public void RepeatMode()
        {
            CheckNextWave();
        }

        #endregion

        #region Functions

        private void CheckNextWave()
        {
            if (_levelManager.IsLastWave() && _levelManager.EnemiesAlive <= 0)
            {
                LevelManager.OnLevelUpdate?.Invoke("Finish");
                _levelManager.WinGame();
                return;
            }
            
            if (!_levelManager.CanStartNextWave) return;
            _levelManager.CanStartNextWave = false;
            
            StartCoroutine(StartNextWaveWithDelay(5f));
        }
        
        private IEnumerator StartNextWaveWithDelay(float delay)
        {
            var remainingTime = delay;
            
            while (remainingTime > 0)
            {
                LevelManager.OnLevelUpdate?.Invoke("Start in " + remainingTime);
                yield return new WaitForSeconds(1);
                remainingTime--;
            }

            StartNextWave();
        }
        
        private void StartNextWave()
        {
            LevelManager.OnLevelUpdate?.Invoke($"Wave {(_levelManager.CurrentWaveIndex + 2):00}");
            var waveData = _levelManager.GetCurrentWave(true);
            var enemyData = waveData.EnemyData;
            var pathIndex = waveData.PathIndex % _levelManager.EnemyPaths.Count;
            var enemyPath = _levelManager.EnemyPaths[pathIndex];
            var enemyCount = waveData.EnemyCount;
            
            StartCoroutine(_levelManager.SpawnEnemies(enemyData, enemyPath, enemyCount, _levelManager.CurrentWaveIndex));
        }
        
        #endregion
    }
}
