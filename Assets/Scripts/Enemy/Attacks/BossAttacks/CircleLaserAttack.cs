using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLaserAttack : Attack
{
    [Header("GObject and component")]
    [SerializeField] GameObject laser;
    [SerializeField] List<GameObject> beams;
    [SerializeField] RotateObject rotateObject;
    [SerializeField] DangerouseObject laserHit;
    [Header("Settings")]
    [SerializeField] float damage;
    [SerializeField] float attackDelay;
    [SerializeField] List<float> rotateSpeedList;
    [SerializeField] Vector2 finalBeamScale;
    [SerializeField] Vector2 baseBeamScale;
    [SerializeField] float selfDestructDelay;

    public override void Initialize()
    {
        rotateObject = GetComponent<RotateObject>();

        float rotateSpeed = rotateSpeedList[Random.Range(0, rotateSpeedList.Count)];
        rotateObject?.SetRotateSpeed(rotateSpeed);

        laserHit.Initialize(damage);
    }

    public override IEnumerator Start()
    {
        yield return StartCoroutine(CR_Start());
    }

    IEnumerator CR_Start()
    {
        yield return new WaitForSeconds(attackDelay);
        rotateObject.Stop();

        foreach (GameObject beam in beams)
        {
            beam.transform.localScale = finalBeamScale;
        }

        yield return new WaitForSeconds(selfDestructDelay);
        
        gameObject.SetActive(false);
    }

    public override void Reset()
    {
        foreach (GameObject beam in beams)
        {
            beam.transform.localScale = baseBeamScale;
        }
    }
}
