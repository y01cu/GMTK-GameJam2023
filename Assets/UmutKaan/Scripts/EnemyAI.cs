using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    PlayerAI player;
    [SerializeField] LayerMask whatIsPlayer;
    float detectionRange = 17;
    float attackRange = 3;
    private float movementSpeed = 5f;

    void Update() {
        CheckDetectionRange();
        if (player != null) {
            MoveToPlayer();
        } else {
            MoveRandomly();
        }
    }

    void CheckDetectionRange() {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, detectionRange, whatIsPlayer);
        player = playerInRange[0].GetComponent<PlayerAI>();
    }

    void MoveToPlayer() {
        PlayerAI player = null;

        float distanceToEnemy = Vector3.Distance(transform.position, player.transform.position);

        // Move to the closest enemy if it's not in attack range
        if (distanceToEnemy > attackRange) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        } else if (distanceToEnemy <= attackRange) {
            Attack(player);
        }
    }

    bool isAttackMessageLoggedRecently = false;
    void Attack(PlayerAI enemy) {
        // Placeholder for attack logic
        // Consider creating an interface or a base class for enemies to standardize how you deal damage to them
        PlayerAI enemyScript = enemy.GetComponent<PlayerAI>();
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
