using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurEnemyControllerScriptManager : MonoBehaviour
{
    [SerializeField] GameObject enemiesParent;
    List<Enemy> enemies;

    private void Start() {
        DeactivateOurController();
        StartCoroutine( ActivateOurControllerRandomlyAfterSomeTime());
        DetectEnemies();   
    }

    void DeactivateOurController() {
        foreach (Enemy enemy in enemies)
        {
            enemy.GetComponent<OurEnemyController>().enabled = false;
        }
    }

    void DetectEnemies() {
        int childCount = enemiesParent.transform.childCount;
        for (int i = 0; i < childCount; i++) {
            Transform child = enemiesParent.transform.GetChild(i);
            Enemy enemy = child.GetComponent<Enemy>();
            if (enemy != null) {
                enemies.Add(enemy);
            }
        }
    }

    // This code block below is taken from DalgaKıran project for being an example in child gameobject management.

    //private void DeactivateAllChildrenExceptRequired() {
    //    int childCount = transform.childCount;
    //    for (int i = childCount - 1; i >= 0; i--) {
    //        Transform child = transform.GetChild(i);
    //        bool isChildGameObjectHasToBeActiveInitially = child.GetComponent<InitiallyActive>() != null;
    //        Debug.Log("Checked");
    //        if (isChildGameObjectHasToBeActiveInitially) {
    //            child.gameObject.SetActive(true);
    //            return;
    //        }
    //        child.gameObject.SetActive(false);
    //    }
    //}

    IEnumerator ActivateOurControllerRandomlyAfterSomeTime() {
        float timeToWait = 3f;
        yield return new WaitForSeconds(timeToWait);
        System.Random randomSelector = new System.Random();
        int randomlySelectedIndex = randomSelector.Next(0, enemies.Count);
        enemies[randomlySelectedIndex].GetComponent<OurEnemyController>().enabled = true;
    }


}
