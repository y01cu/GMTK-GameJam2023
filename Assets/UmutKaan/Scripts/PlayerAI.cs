using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour {
    bool isEnemyInAttackRange;
    [Tooltip("Colored in blue")]
    [SerializeField] float detectionRange;
    [Tooltip("Colored in red")]
    [SerializeField] float attackRange;
    float movementSpeed = 2f;
    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] List<Enemy> enemies;
    // Start is called before the first frame update

    AvoidCharacterFallingManager avoidCharacterFalling;
    void Start() {
        enemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update() {
        AvoidCharacterFallingManager.instance.AvoidFallingOfTransform(this.transform);

        CheckDetectionRange();
        if (enemies.Count > 0) {
            MoveToNearestEnemy();
        } else {
            MoveRandomly();
        }
    }

    [SerializeField] Transform temporaryTransform;

    void CheckDetectionRange() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRange, whatIsEnemy);
        enemies.Clear();
        foreach (Collider enemy in enemiesInRange) {
            enemies.Add(enemy.GetComponent<Enemy>());
        }
    }

    void MoveToNearestEnemy() {
        Enemy closestEnemy = null;
        float minimumDistance = float.MaxValue;

        foreach (Enemy enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < minimumDistance) {
                minimumDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        // Move to the closest enemy if it's not in attack range
        if (minimumDistance > attackRange) {
            transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, movementSpeed * Time.deltaTime);
        } else if (minimumDistance <= attackRange) {
            Attack(closestEnemy);
        }
    }

    bool isAttackMessageLoggedRecently = false;
    void Attack(Enemy enemy) {
        // Placeholder for attack logic
        // Consider creating an interface or a base class for enemies to standardize how you deal damage to them
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null && !isAttackMessageLoggedRecently) {
            // Call take damage method for enemy
            StartCoroutine(LogAttackMessage());

        }
    }
    /// <summary>
    /// This method simulates an attack by logging a message to the console
    /// </summary>
    /// <returns></returns>
    IEnumerator LogAttackMessage() {
        isAttackMessageLoggedRecently = true;
        Debug.Log("Attacking enemy");
        float attackMessageCooldown = 2f;
        yield return new WaitForSeconds(attackMessageCooldown);
        isAttackMessageLoggedRecently = false;
    }
    bool willPlayerMoveForward = true;
    bool isMovementOnCooldown = false;
    Vector3 randomPosition;
    void MoveRandomly() {
        if (!isMovementOnCooldown) {
            isMovementOnCooldown = true;
            StartCoroutine(ChangeRandomMovementPosition());
        }
        if (willPlayerMoveForward) {
            Vector3 forwardPosititon = new Vector3(transform.position.x, transform.position.y, transform.position.z + 25);
            randomPosition = forwardPosititon;
        } else {
            Vector3 backgroundPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 25);
            randomPosition = backgroundPosition;
        }
        //System.Random positionRandomizer = new System.Random();
        //int randomIndex = positionRandomizer.Next(0, possibleDirections.Count);


        transform.position = Vector3.MoveTowards(transform.position, randomPosition, movementSpeed * Time.deltaTime);
    }

    IEnumerator ChangeRandomMovementPosition() {
        float movementCooldown = 3f;
        yield return new WaitForSeconds(movementCooldown);
        willPlayerMoveForward = !willPlayerMoveForward;
        isMovementOnCooldown = !isMovementOnCooldown;
    }

    void OnDrawGizmosSelected() {
        // Draw attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw detection range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    
}