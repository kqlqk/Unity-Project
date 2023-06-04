using UnityEngine;

public class PortalMove : MonoBehaviour
{
    private Transform playerTransform;
    private Transform enemyTransform;
	private GameObject spawnedObject;

    private Vector3 playerOffset;
    private Vector3 enemyOffset;
	private Vector3 enemyPortal;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        enemyTransform = GameObject.FindWithTag("Enemy").transform;
        playerOffset = new Vector3(0, -2.13f, 1.5f);
        enemyOffset = new Vector3(0, -2.13f, 7f);
		enemyPortal = new Vector3(0, -1.9f, 7f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerTransform.position = GameObject.FindWithTag("Portal2").transform.position + playerOffset;

            playerTransform.rotation = new Quaternion(0,0,0,0);
            
			spawnedObject = Instantiate(GameObject.FindWithTag("EnemyPortal"), GameObject.FindWithTag("Portal2").transform.position + enemyPortal, new Quaternion(0,-180,0,0));
			
            Invoke("SpawnEnemy", 1f);
        }
    }

    private void SpawnEnemy()
    {
        enemyTransform.position = GameObject.FindWithTag("Portal2").transform.position + enemyOffset;	
		Destroy(spawnedObject);
    }
}
