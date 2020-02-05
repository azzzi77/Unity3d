using UnityEngine;
using UnityEngine.Profiling;

namespace Geekbrains
{
    public sealed class GameController : MonoBehaviour
    {
        private Controllers _controllers;
        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }

        private void Update()
        {
            Profiler.BeginSample("CheckControllers");

            for (var i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
            Profiler.EndSample();

        }
    }
}
