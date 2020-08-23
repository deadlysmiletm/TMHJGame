using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace TMHJ.Core.Controllers
{
    public class DotController : MonoBehaviour
    {
        private void Awake()
        {
            this.OnTriggerEnterAsObservable()
                .Where(collider => collider.gameObject.layer == 8)
                .Select(collider => collider.GetComponentInParent<PlayerController>())
                .Subscribe(p => p.AddChild());
        }
    }
}