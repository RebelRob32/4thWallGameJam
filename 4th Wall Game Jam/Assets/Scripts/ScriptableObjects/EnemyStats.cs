using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FWGJ.Enemy
{
    [CreateAssetMenu(menuName ="Enemy")]
    public class EnemyStats : ScriptableObject
    {
        public float moveSpeed;
        public float range;
        public bool isScaring;
        
    }
}

