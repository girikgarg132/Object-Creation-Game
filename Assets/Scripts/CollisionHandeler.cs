using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionHandeler : MonoBehaviour
{
    GameManager gameManager;
    RandomObjectSpawner objectSpawner;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        objectSpawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RandomObjectSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObjectComponent"))
        {
            Destroy(gameObject.GetComponent<BoxCollider>());
            if (gameObject.name == other.gameObject.name + "(Clone)")
            {
                gameManager.RightAnswer();
                Destroy(gameObject.GetComponent<XRGrabInteractable>());
                gameObject.transform.parent = other.gameObject.transform.parent;
                Destroy(other.gameObject);
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
                gameObject.transform.localScale = Vector3.one;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                gameManager.WrongAnswer();
                objectSpawner.RespawnObject(gameObject);
            }
        }
    }
}
