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
            SwitchState(new TurretIdleState(this));
        }

        #endregion
    }
}
