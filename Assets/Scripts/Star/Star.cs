using UnityEngine;

namespace spaceExplorer.star
{
    public class Star : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameController.Instance.AddPoints();
            Destroy(gameObject);
        }
    }
}

