using _Scripts.Projectiles;
using UnityEngine;

namespace _Scripts.Units
{
    public class ZombieBomb : Bomb
    {
        [SerializeField] private Zombie targetZombie;

        private void Start()
        {
            targetZombie.DeadEvent += BlowUp;
        }
    }
}