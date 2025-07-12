using UnityEngine;

namespace spaceExplorer.Asteroid
{
    public class AsteroidSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject asteroidPrefab;
        [SerializeField] private GameObject[] spawnableLand;   //[0] and [1] will create a rectangle
        private readonly Vector2 randomScaleRange = new Vector2(0.5f, 2f); // Min and Max scale values

        private void Start()
        {
            InvokeRepeating(nameof(SpawnAsteroid), 0f, 2f); // Spawn every 2 seconds
        }
        private void SpawnAsteroid()
        {
            GameObject asteroid = Instantiate(asteroidPrefab, RandomSpawnPosition(), Quaternion.identity);
            asteroid.transform.localScale = new Vector3(
                RandomFloat(randomScaleRange.x, randomScaleRange.y), 
                RandomFloat(randomScaleRange.x, randomScaleRange.y), 
                1f);
        }
        private Vector2 RandomSpawnPosition()
        {
            float x = RandomFloat(spawnableLand[0].transform.position.x, spawnableLand[1].transform.position.x);
            float y = RandomFloat(spawnableLand[0].transform.position.y, spawnableLand[1].transform.position.y);
            return new Vector2(x, y);
        }
        private float RandomFloat(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
}

