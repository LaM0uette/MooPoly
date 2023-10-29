using System;
using Game.Scripts._Data.TurretData;
using Game.Scripts.BaseStateMachine;
using Game.Scripts.Turrets.TurretStateMachine.TurretStates;
using Game.Scripts.Turrets.Variants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Turrets.TurretStateMachine
{
    [RequireComponent(typeof(SphereCollider))]
    public class TurretStateMachine : StateMachine
    {
        #region Statements
        
        public Action OnUpdateRepeating { get; set; }
        public Turret Turret { get; private set; }
        
        [Space, Title("Turret")]
        public LayerMask EnemyLayer;
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
            GetComponent<SphereCollider>().radius = turretData.Range;
            InvokeRepeating(nameof(UpdateRepeating), 0, 0.1f);
            SwitchState(new TurretBuildState(this));
        }

        #endregion

        #region Functions

        private void UpdateRepeating() => OnUpdateRepeating?.Invoke();

        #endregion
        
        #region Debug

        public static void DebugLine(Vector3 from, Vector3 to, Color color)
        {
            Debug.DrawLine(from, to, color);
        }
        
        private void OnDrawGizmosSelected()
        {
            if (turretData is null) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, turretData.Range);
        }

        #endregion
    }
}
