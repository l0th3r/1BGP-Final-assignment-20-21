using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] private float boundarie;
    [SerializeField] private float spawnRange;
    [SerializeField] private float timeToNextPos;
    [SerializeField] private GameObject collectablePrefab;

    private GameObject collectable;
    private float zoneXSize;
    private float zoneZSize;

    private Vector3 nextPos;
    private Vector3 oldPos;

    private void Start()
    {
        defineZone();
    }

    public void collectableHit()
    {
        StartCoroutine(FindNextPosAfterTime(0));
    }

    private void findNextPos()
    {
        oldPos = nextPos;
        nextPos = getRandomPosOutOfRangeOfPos(oldPos, spawnRange);
        StartCoroutine(MoveToNextPosInTime());
    }

    private IEnumerator MoveToNextPosInTime()
    {
        float timePercent = 0f;

        //START EVENT

        while (timePercent < 1)
        {
            timePercent += Time.deltaTime / timeToNextPos;
            collectable.transform.position = Vector3.Lerp(oldPos, nextPos, timePercent);
            yield return null;
        }

        //END EVENT
    }

    public void defineStartPos()
    {
        Vector3 spawnPos = getRandomPosOutOfRangeOfPos(Vector3.zero, 5f);

        if (collectable)
            Destroy(collectable);

        collectable = Instantiate(collectablePrefab, spawnPos, Quaternion.identity);
        collectable.GetComponent<collectibleController>().endMovingEvent();
        oldPos = spawnPos;
        nextPos = spawnPos;
    }

    // return a position outside of an area around a position
    private Vector3 getRandomPosOutOfRangeOfPos(Vector3 position, float areaSize)
    {
        Vector3 result = Vector3.zero;
        bool isGood = false;

        while (!isGood)
        {
            result = getRandomPos();
            if (!isCoordInRange(position, result, areaSize))
                isGood = true;
        }
        return result;
    }

    private Vector3 getRandomPos()
    {
        Vector3 goPos = transform.position;
        Vector3 result;

        result.x = Random.Range(goPos.x - zoneXSize + boundarie, goPos.x + zoneXSize - boundarie);
        result.y = goPos.y + 1f;
        result.z = Random.Range(goPos.z - zoneZSize + boundarie, goPos.z + zoneZSize - boundarie);

        //Debug.Log("x= " + result.x + " / y= " + result.y + " / z= " + result.z);

        return result;
    }

    bool isCoordInRange(Vector3 position, Vector3 coords, float areaSize)
    {
        if ((coords.x >= position.x - areaSize && coords.x <= position.x + areaSize) &&
            (coords.z >= position.z - areaSize && coords.z <= position.z + areaSize))
            return true;
        else
            return false;
    }

    private void defineZone()
    {
        zoneXSize = transform.localScale.x / 2;
        zoneZSize = transform.localScale.z / 2;
    }

    IEnumerator FindNextPosAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        findNextPos();
    }
}
