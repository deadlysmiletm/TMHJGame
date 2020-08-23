using UnityEngine;

namespace TMHJ.Interfaces
{
    public abstract class SnakeComponent : MonoBehaviour
    {
        public abstract SnakeComponent PreviousSnake { get; }
        public abstract SnakeComponent NextSnake { get; }
        public abstract Vector3 Position { get; }
        public abstract Transform Pivot { get; }

        /// <summary>
        /// Add a new component in the snake.
        /// </summary>
        /// <param name="next"></param>
        public abstract void AddNextSnakeComponent(SnakeComponent next);
    }
}