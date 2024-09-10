using System.Collections;
using UnityEngine;

public class JumpAttack : Attack
{
    [Header("GO and components")]
    [SerializeField] GameObject body;
    [SerializeField] GameObject target;
    [SerializeField] MovementController moveController;
    Transform trans;

    [Header("Settings")]
    [SerializeField] float jumpHeight = 30f;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dropDelay;
    [SerializeField] float jumpDelay;
    [SerializeField] float jumpCount;
    float maxHeight;
    float minHeight;
    [SerializeField] GameObject groundHitVFX;
    [SerializeField] AudioClip groundHitSFX;

    public override void Initialize(GameObject _target)
    {
        trans = body.transform;

        minHeight = trans.position.y;
        maxHeight = minHeight + jumpHeight;

        target = _target;
    }

    public override IEnumerator Start()
    {
        for (int i = 0; i < jumpCount; i++)
        {
            groundHitVFX.SetActive(false);
            moveController.MoveSpeed = jumpSpeed;
            moveController.Move(Vector2.up);

            // wait until reaching maximum height
            while (trans.position.y <= maxHeight) yield return null;
            moveController.Stop();

            trans.position = new Vector3(target.transform.position.x, transform.position.y);
            yield return new WaitForSeconds(dropDelay);
            moveController.MoveSpeed = jumpSpeed;
            moveController.Move(Vector2.down);

            // wait until reaching minimum height
            while (trans.position.y >= minHeight) yield return null;
            groundHitVFX.SetActive(true);
            moveController.Stop();
            AudioManager.Instance.PlaySound(groundHitSFX);

            yield return new WaitForSeconds(jumpDelay);
        }
    }

    public override void Reset()
    {
        Stop();
        moveController.Reset();
    }
}
