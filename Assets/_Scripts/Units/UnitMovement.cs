using System;
using _Scripts.Levels;
using _Scripts.Tower_Logic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Scripts.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        [Inject] private Tower _tower;
        
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.SetDestination(_tower.transform.position);
        }
    }
}