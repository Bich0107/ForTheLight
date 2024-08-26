using System.Collections;
using UnityEngine;

public class JumpAttack : Attack
{
    [Header("GO and components")]
    [SerializeField] GameObject body;
    [SerializeField] MovementController moveController;
    [SerializeField] Transform trans;
    [SerializeField] GameObject target;
    [Header("Settings")]
    Vector3 basePosition;
    float maxHeight;
    float minHeight;
    [SerializeField] float jumpHeight = 30f;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dropDelay;
    [SerializeField] float jumpDelay;
    [SerializeField] float jumpCount;
    [SerializeField] GameObject groundHitVFX;
    [SerializeField] AudioClip groundHitSFX;

    public override void Initialize()
    {
        trans = body.transform;
        moveController = body.GetComponent<MovementController>();

        basePosition = trans.position;

        target = FindObjectOfType<Player>().gameObject;
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

        trans.position = basePosition;
        moveController.Reset();
    }
}
