using UnityEngine;
using BlueStellar.Cor.Helpers;

namespace BlueStellar.Cor
{
    public class SpawnedBarrel
    {
        CollectableBarrel _collectableBall;
        CharacterColorType _type;
        Vector3 _spawnPosition;

        public CollectableBarrel GetCollectableBall()
        {
            return _collectableBall;
        }

        public CharacterColorType GetSpawnedBallType()
        {
            return _type;
        }

        public Vector3 SpawnPosition()
        {
            return _spawnPosition;
        }

        public Vector3 AddSpawnPosition(Vector3 pos)
        {
            return _spawnPosition = pos;
        }

        public void SetSpawnedBall(CollectableBarrel ball, Vector3 pos)
        {
            _collectableBall = ball;
            _type = _collectableBall.Type();
            _spawnPosition = pos;
        }

        public void SetNewSpawnedBall(CollectableBarrel collectableBall)
        {
            _collectableBall = collectableBall;
            _type = _collectableBall.Type();
        }

        public void ClearSpawnedBall()
        {
            _collectableBall = null;
        }
    }
}
