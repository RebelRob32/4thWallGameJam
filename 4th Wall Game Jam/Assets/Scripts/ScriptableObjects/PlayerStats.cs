using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FWGJ.Player
{
    [CreateAssetMenu(menuName = "Player")]
    public class PlayerStats : ScriptableObject
{
        [Header("Movement Stats")]
        public float moveSpeed;
        public float walkSpeed;
        public float runSpeed;
        public float rotationSpeed;
        public float jumpHeight;
        public float gravity = -9.81f;

        [Header("Gameplay Stats")]
        public float fearLevel;
        public int asthmaCharges = 3;
}

}

