using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLaser : Projectile
{
    [SerializeField] RotateObject rotateObject;
    [SerializeField] List<float> rotateSpeedList;
    [SerializeField] Vector2 finalBeamScale;
    [SerializeField] Vector2 baseBeamScale;
    [SerializeField] float selfDestructDelay;
    [SerializeField] List<GameObject> beams;

    private void OnEnable()
    {
        rotateObject = GetComponent<RotateObject>();

        float rotateSpeed = rotateSpeedList[Random.Range(0, rotateSpeedList.Count)];
        rotateObject?.SetRotateSpeed(rotateSpeed);
    }

    public override void Fire(Vector2 _direction)
    {
        attackCoroutine = StartCoroutine(CR_Fire(_direction));
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
       IHitByEnemy hit = other.GetComponent<IHitByEnemy>();
        if (hit != null) {
            hit.Hit(projectileScript.GetDamage);
        } 
    }

    protected override IEnumerator CR_Fire(Vector2 _direction)
    {
        yield return new WaitForSeconds(projectileScript.GetAttackDelay);
        rotateObject.Stop();

        foreach (GameObject beam in beams)
        {
            beam.transform.localScale = finalBeamScale;
        }

        yield return new WaitForSeconds(selfDestructDelay);
        Die();
    }

    protected override void Reset()
    {
        base.Reset();
        foreach (GameObject beam in beams)
        {
            beam.transform.localScale = baseBeamScale;
        }
    }
}
