using System;
using Game.Scripts._Data.TurretData;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Turrets.TurretFactory;
using Game.Scripts.Turrets.TurretFactory.Variants;
using Game.Scripts.Turrets.TurretStateMachine.TurretStates;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Turrets.TurretStateMachine
{
    public class TurretStateMachine : StateMachine
    {
        #region Statements
        
        public Action OnUpdateRepeating { get; set; }
        
        public Turret Turret { get; private set; }
        public float FireRateCountdown { get; set; }
        
        [Space, Title("Turret")]
        public LayerMask EnemyLayer;
        public Transform TurretMobileTransform;
        public Transform TurretHeadTransform;
        public Transform InitialPos;
        public Transform FirePoint;
        
        [Space, Title("Data")]
        [SerializeField] private TurretData _turretData;

        private void Awake()
        {
            Turret = gameObject.AddComponent<BasicTurret>();
            Turret.Init(_turretData, FirePoint);
        }

        private void Start()
        {
            InvokeRepeating(nameof(UpdateRepeating), 0, 0.1f);
            SwitchState(new TurretBuildState(this));
        }

        #endregion

        #region Events

        private void UpdateRepeating() => OnUpdateRepeating?.Invoke();

        #endregion
        
        #region Debug

        public static void DebugLine(Vector3 from, Vector3 to, Color color)
        {
            Debug.DrawLine(from, to, color);
        }
        
        private void OnDrawGizmosSelected()
        {
            if (_turretData is null) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _turretData.Range);
        }

        #endregion
    }
}
