using System.Collections;
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
            Debug.Log("Start");
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
            if (!_levelManager.CanStartNextWave) return;
            _levelManager.CanStartNextWave = false;
            
            StartCoroutine(StartNextWaveWithDelay(5f));
        }
        
        private IEnumerator StartNextWaveWithDelay(float delay)
        {
            var remainingTime = delay;
            
            while (remainingTime > 0)
            {
                Debug.Log("Start in " + remainingTime);
                yield return new WaitForSeconds(1);
                remainingTime--;
            }

            StartNextWave();
        }
        
        private void StartNextWave()
        {
            var waveData = _levelManager.GetCurrentWave(true);
            
            if (_levelManager.CurrentWaveIndex >= _levelManager.GetWavesCount()) return;
            
            var enemyPrefab = waveData.EnemyPrefab;
            var enemyPath = _levelManager.EnemyPaths[waveData.PathIndex];
            var enemyCount = waveData.EnemyCount;
            
            StartCoroutine(_levelManager.SpawnEnemies(enemyPrefab, enemyPath, enemyCount, _levelManager.CurrentWaveIndex));
        }
        
        #endregion
    }
}
