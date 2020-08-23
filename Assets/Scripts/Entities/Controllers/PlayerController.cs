using UnityEngine;
using TMHJ.Core.Models;
using UniRx;
using UniRx.Triggers;
using TMHJ.Interfaces;

namespace TMHJ.Core.Controllers
{
    public class PlayerController : SnakeComponent
    {
        private PlayerModel Model { get; set; }
        public override SnakeComponent PreviousSnake => null;
        public override SnakeComponent NextSnake => Model.FirstChild;
        public override Vector3 Position => Model.NextPivot.position;
        public override Transform Pivot => Model.NextPivot;

        private Transform _trm;

        private void Awake()
        {
            _trm = this.transform;
            Model = this.GetComponent<PlayerModel>();
            Model.FirstChild = this;
            Model.LastChild = this;

            var inputStream = this.UpdateAsObservable();

            inputStream
                        .Subscribe(_ => MoveForward());

            inputStream.Where(_ => Input.GetAxisRaw("Horizontal") != 0)
                        .Subscribe(_ => TurnAround(Input.GetAxis("Horizontal")));
        }

        /// <summary>
        /// Control the rotations of the PlayerHead.
        /// </summary>
        /// <param name="input"></param>
        private void TurnAround(float input)
        {
            _trm.Rotate(_trm.up * input * Model.RotationAngle * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Advance the PlayerHead forward forever.
        /// </summary>
        private void MoveForward()
        {
            _trm.position += transform.forward * Model.MovmentSpeed * Time.fixedDeltaTime;
        }

        /// <summary>
        /// Create a new child in the Snake.
        /// </summary>
        public void AddChild()
        {
            Debug.Log("Creando child");
            var snake = GameObject.Instantiate(Model.ChildPrefab)
                                  .GetComponent<ChildController>()
                                  .Init(Model.LastChild);
            Model.LastChild = snake;
            Model.ChildsCount++;
        }

        public override void AddNextSnakeComponent(SnakeComponent next)
        {
            if (next.GetInstanceID() == this.GetInstanceID())
            {
                Model.FirstChild = next;
            }

            Model.LastChild = next;
        }
    }
}