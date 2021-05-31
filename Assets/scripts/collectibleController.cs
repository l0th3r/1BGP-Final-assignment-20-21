using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleController : MonoBehaviour
{
    [SerializeField] private GameObject particules;
    [SerializeField] private GameObject model;
    private bool canBeHit;

    private void Update()
    {
        model.transform.Rotate(0, 100 * Time.deltaTime, 0);
    }

    public void startMovingEvent()
    {
        canBeHit = false;
    }

    public void endMovingEvent()
    {
        canBeHit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && canBeHit)
        {
            FindObjectOfType<GroundController>().collectableHit();
            FindObjectOfType<gameModeManager>().addToScore(1);
            particules.GetComponent<ParticleSystem>().Play();
        }
    }
}
