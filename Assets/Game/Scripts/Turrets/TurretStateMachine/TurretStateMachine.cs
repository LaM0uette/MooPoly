using System;
using Game.Scripts._Data.TurretData;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Turrets.TurretStateMachine.TurretStates;
using Game.Scripts.Turrets.Variants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Turrets.TurretStateMachine
{
    public class TurretStateMachine : StateMachine
    {
        #region Statements
        
        public Action OnUpdateRepeating { get; set; }
        
        // Components
        public Turret Turret { get; private set; }
        
        [Space, Title("Turret GameObject")]
        public Transform TurretMobileTransform;
        public Transform TurretHeadTransform;
        public Transform InitialPos;
        public Transform FinalPos;
        public Transform FirePoint;
        
        [Space, Title("Data")]
        [SerializeField] private TurretData turretData;

        private void Awake()
        {
            Turret = gameObject.AddComponent<BasicTurret>();
            Turret.Init(turretData);
        }

        private void Start()
        {
            InvokeRepeat(0, turretData.RepeatRate);
            
            SwitchState(new TurretBuildState(this));
        }

        #endregion

        #region Functions

        private void InvokeRepeat(float startTime, float repeatRate) => 
            InvokeRepeating(nameof(UpdateRepeating), startTime, repeatRate);
        
        private void UpdateRepeating() => OnUpdateRepeating?.Invoke();

        #endregion
        
        #region Debug

        public static void DebugLine(Vector3 from, Vector3 to, Color color)
        {
            Debug.DrawLine(from, to, color);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, turretData.Range);
        }

        #endregion
    }
}
