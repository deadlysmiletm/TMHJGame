using TMHJ.Interfaces;
using UnityEngine;

namespace TMHJ.Core.Models
{
    public class PlayerModel : MonoBehaviour
    {
        public SnakeComponent FirstChild { get; set; }
        public uint ChildsCount { get; set; }
        public SnakeComponent LastChild { get; set; }

        [Header("Pivot")]
        public Transform NextPivot;

        [Header("Movement")]
        public float RotationAngle;
        public float MovmentSpeed;

        [Header("Prefabs")]
        public GameObject ChildPrefab;
    }
}