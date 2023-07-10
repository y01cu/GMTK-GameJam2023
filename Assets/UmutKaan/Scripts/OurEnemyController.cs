using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class OurEnemyController : MonoBehaviour {
    [SerializeField] float movementSpeed;
    float horizontalInput;
    float verticalInput;
    void Update() {
        AvoidCharacterFallingManager.instance.AvoidFallingOfTransform(this.transform);
        GetInputsFromBothAxis();
        UpdateRotationAndMovementBasedOnInput();
    }
    void GetInputsFromBothAxis() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    // The duration of the rotation transition
    float transitionDuration = 2f;

    // The timer to keep track of the elapsed time during the transition
    float transitionTimer = 0f;

    Quaternion newRotation;
    void UpdateRotationAndMovementBasedOnInput() {

        if (horizontalInput > 0)
        {
            GetComponent<Animator>().SetTrigger("Walk");
            newRotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            Vector3 movementVector = new Vector3(verticalInput, 0, horizontalInput) * movementSpeed * Time.deltaTime;
            transform.Translate(movementVector);
        }
        if (horizontalInput < 0) {
            GetComponent<Animator>().SetTrigger("Walk");
            newRotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            Vector3 movementVector = new Vector3(verticalInput, 0, horizontalInput) * -movementSpeed * Time.deltaTime;
            transform.Translate(movementVector);
        }
        if (verticalInput > 0) {
            GetComponent<Animator>().SetTrigger("Walk");
            newRotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
            Vector3 movementVector = new Vector3(horizontalInput, 0, verticalInput) * movementSpeed * Time.deltaTime;
            transform.Translate(movementVector);
        }
        if (verticalInput < 0) {
            GetComponent<Animator>().SetTrigger("Walk");
            newRotation = Quaternion.Euler(transform.rotation.x, 270, transform.rotation.z);
            Vector3 movementVector = new Vector3(horizontalInput, 0, verticalInput) * -movementSpeed * Time.deltaTime;
            transform.Translate(movementVector);
        }
        if(horizontalInput == 0 && verticalInput == 0) {
            GetComponent<Animator>().SetTrigger("Idle");
        }
        float lerpSpeed = 10f;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * lerpSpeed);

    }
    void FixedUpdate() {
        Move();
    }
    void Move() {
        
    }
}
