using System.Collections;
using System.Collections.Generic;
using Game.Scripts._Data.EnemyData;
using Game.Scripts._Data.LevelData;
using Game.Scripts.Enemies.EnemyFactory;
using Game.Scripts.Enemies.EnemyStateMachine;
using Game.Scripts.Levels.GameMode;
using Game.Scripts.MooCoins;
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
        private GameObject _mooCoinsParent;
        private IGameMode _currentGameMode;

        private void Awake()
        {
            _enemiesParent = GameObject.FindGameObjectWithTag(TagRef.EnemiesParent);
            _mooCoinsParent = GameObject.FindGameObjectWithTag(TagRef.MooCoinsParent);
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
        
        #endregion

        #region Waves

        public Wave GetCurrentWave(bool increment = false) => increment ? LevelData.Waves[++CurrentWaveIndex] : LevelData.Waves[CurrentWaveIndex];
        public bool IsLastWave() => CurrentWaveIndex + 1 >= LevelData.Waves.Length;

        #endregion

        #region Enemy

        public void EnemySpawned() => EnemiesAlive++;

        public void EnemyDied(Enemy enemy)
        {
            EarnMooCoins(enemy.MooCoins, enemy.DeadPosition);
            EnemiesAlive--;
        }

        public void CheckEnemiesAlive()
        {
            if (IsLastWave()) return;
            CanStartNextWave = EnemiesAlive <= 0;
        }

        #endregion

        #region MooCoins

        private void EarnMooCoins(IEnumerable<MooCoinProbability> mooCoins, Vector3 pos)
        {
            pos.y += 1f;
            
            foreach (var mooCoin in mooCoins)
            {
                var random = Random.Range(0, 1f);
                if (random > mooCoin.Probability) continue;
                
                var mooCoinGameObject = Instantiate(mooCoin.MooCoinData.Prefab, pos, Quaternion.Euler(0, 0, 90), _mooCoinsParent.transform);
                var mooCoinBehaviour = mooCoinGameObject.GetComponent<MooCoinBehaviour>();
                
                var randomForce = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(1, 3), Random.Range(-0.3f, 0.3f));
                mooCoinBehaviour.Rigidbody.AddForce(randomForce * 5f, ForceMode.Impulse);
                mooCoinBehaviour.MooCoin = MooCoinFactory.Create(mooCoin.MooCoinData);
            }
        }

        #endregion
    }
}
