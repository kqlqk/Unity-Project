using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public float speed = 3.0f;
    public float detectionDistance = 70.0f;
    public float avoidanceDistance = 5.0f;
    public float avoidanceSpeed = 3.0f;
    private Animator animator;
    
    public bool isTraining = false;
    public int epochs = 10000000;
    public float learningRate = 0.01f;

    private NeuralNetwork nn;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        nn = new NeuralNetwork(2, 2, 10);
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();

        if (isTraining)
        {
            Train();
        }
        else
        {
            nn.LoadModel("Assets/GameScripts/Learning/EnemyModel.txt");
        }
    }

    private void FixedUpdate()
    {
        float playerDistance = Vector3.Distance(player.position, transform.position);

        float[] inputs = new float[2];
        inputs[0] = player.position.x - transform.position.x; // distance player enemy (X)
        inputs[1] = player.position.z - transform.position.z; // distance player enemy (Z)

        float[] outputs = nn.FeedForward(inputs);

        Vector3 movementDirection = Vector3.zero;

        // move to player if he is within detectionDistance
        if (playerDistance < detectionDistance)
        {
            // Use the outputs from the neural network to determine the movement direction
            movementDirection.x = outputs[0] > 0.5f ? 1f : -1f;
            movementDirection.z = outputs[1] > 0.5f ? 1f : -1f;
        }

        if (movementDirection != Vector3.zero)
        {
            Ray ray = new Ray(transform.position, movementDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, avoidanceDistance))
            {
                if (hit.collider.tag.Equals("Obtains"))
                {
                    movementDirection += hit.normal * avoidanceSpeed;
                }
            }
        }

        Vector3 newPosition = transform.position + movementDirection.normalized * speed * Time.deltaTime;
        newPosition.y = transform.position.y;

        rb.MovePosition(newPosition);
        
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            rb.MoveRotation(targetRotation);
            
            animator.SetFloat("speed", 1f);
            
        }
        else
        {
           animator.SetFloat("speed", 0f);
        }
    }



    private void Train()
    {
        for (int i = 0; i < epochs; i++)
        {
            // generate random data for training
            float playerX = Random.Range(-detectionDistance, detectionDistance);
            float playerZ = Random.Range(-detectionDistance, detectionDistance);

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

        nn.SaveModel("Assets/GameScripts/Learning/EnemyModel.txt");
        Debug.Log("Model was saved");
    }
}
