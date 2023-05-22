using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float detectionDistance = 40.0f;
    public float avoidanceDistance = 10.0f;
    public float avoidanceSpeed = 3.0f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public bool isTraining = true;
    public int epochs = 1000;
    public float learningRate = 0.1f;

    private NeuralNetwork nn;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        
        nn = new NeuralNetwork(2, 2, 10);

        if (isTraining)
        {
            Train();
        }
        else
        {
            nn.LoadModel("Assets/GameScripts/Learning/EnemyModel" + GlobalScript.currentLvl + ".txt");
        }
    }

    private void FixedUpdate()
    {
        float playerDistance = Vector3.Distance(player.position, transform.position);

        float[] inputs = new float[2];
        inputs[0] = player.position.x - transform.position.x; // distance player enemy (X)
        inputs[1] = player.position.z - transform.position.z; // distance player enemy (Z)

        // output data
        float[] outputs = nn.FeedForward(inputs);

        Vector3 movementDirection = Vector3.zero;

        // move to player if he is within detectionDistance
        if (playerDistance < detectionDistance)
        {
            // Use the outputs from the neural network to determine the movement direction
            movementDirection.x = outputs[0] > 0.5f ? 1f : -1f;
            movementDirection.z = outputs[1] > 0.5f ? 1f : -1f;
        }

        // Avoid Inspection Collisions
        if (movementDirection != Vector3.zero)
        {
            Ray ray = new Ray(transform.position, movementDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, avoidanceDistance))
            {
                if (hit.collider.tag.Equals("Obtain"))
                {
                    movementDirection += hit.normal * avoidanceSpeed;
                }
            }
        }

        transform.position += movementDirection.normalized * speed * Time.deltaTime;

        // check if enemy touches the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    }

    private void Train()
    {
        for (int i = 0; i < epochs; i++)
        {
            // generate random data for training
            float playerX = Random.Range(-40f, 40f);
            float playerZ = Random.Range(-40f, 40f);

            // input data
            float[] inputs = new float[2];
            inputs[0] = playerX - transform.position.x;
            inputs[1] = playerZ - transform.position.z;

            // output data
            float[] targets = new float[2];
            targets[0] = (playerX > transform.position.x) ? 1f : 0f;
            targets[1] = (playerZ > transform.position.z) ? 1f : 0f;

            nn.Train(inputs, targets, learningRate);
        }

        nn.SaveModel("Assets/GameScripts/Learning/EnemyModel" + GlobalScript.currentLvl + ".txt");
        Debug.Log("Model was saved");
    }
}
