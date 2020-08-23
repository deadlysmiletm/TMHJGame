using UnityEngine;
using TMHJ.Core.Models;
using UniRx;
using UniRx.Triggers;
using TMHJ.Interfaces;

namespace TMHJ.Core.Controllers
{
    public class ChildController : SnakeComponent
    {
        public ChildModel Model { get; set; }
        public override SnakeComponent PreviousSnake => Model.PreviousChild;
        public override SnakeComponent NextSnake => Model.NextChild;
        public override Vector3 Position => Model.NextChildPivot.position;
        public override Transform Pivot => Model.NextChildPivot;

        private Transform _trm;

        private void Awake()
        {
            
            this.UpdateAsObservable()
                .Subscribe(_ => RotateToPrevious());
        }

        private void RotateToPrevious()
        {
            _trm.forward = Vector3.Slerp(_trm.forward, Model.PreviousChild.Position - _trm.position, Time.fixedDeltaTime/.25f);
        }

        private void UpdatePosition(Vector3 position)
        {
            _trm.position = Vector3.Lerp(_trm.position, position, Time.fixedDeltaTime/.45f);
        }

        public ChildController Init(SnakeComponent previous)
        {
            _trm = this.transform;
            _trm.position = previous.Position;
            previous.AddNextSnakeComponent(this);
            this.UpdateAsObservable()
                .Subscribe(_ => UpdatePosition(PreviousSnake.Position));

            Model = this.GetComponent<ChildModel>();
            Model.PreviousChild = previous;

            return this;
        }

        public override void AddNextSnakeComponent(SnakeComponent next)
        {
            Model.NextChild = next;
        }
    }
}