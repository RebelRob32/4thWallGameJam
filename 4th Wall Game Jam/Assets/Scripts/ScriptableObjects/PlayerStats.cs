using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FWGJ.Player
{
    [CreateAssetMenu(menuName = "Player")]
    public class PlayerStats : ScriptableObject
{
        public float moveSpeed;
        public float walkSpeed;
        public float runSpeed;
        public float jumpHeight;
        public float gravity = -9.81f;
}

}

