using UnityEngine;

namespace BlueStellar.Cor.Transports
{
    public class TransportMovement : MonoBehaviour
    {
        #region Variables

        [Header("MovementSettings")]
        [SerializeField] Transport _transport;
        [SerializeField] Transform _target;
        [SerializeField] private float speed;
        [SerializeField] private bool canMovement;

        #endregion

        private void FixedUpdate()
        {
            Movement();
        }

        public void ActiveMovement(bool isActive)
        {
            canMovement = isActive;
        }

        private void Movement()
        {
            if (!canMovement)
                return;

            if (transform.position == _target.position)
            {
                _transport.StopTransport();
                ActiveMovement(false);
                return;
            }

            speed += 0.007f;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, speed);
        }
    }
}
