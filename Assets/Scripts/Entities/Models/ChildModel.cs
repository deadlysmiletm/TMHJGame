using TMHJ.Interfaces;
using UnityEngine;

namespace TMHJ.Core.Models
{
    public class ChildModel : MonoBehaviour
    {
        public Transform NextChildPivot;

        public SnakeComponent NextChild { get; set; }
        public SnakeComponent PreviousChild { get; set; }
    }
}