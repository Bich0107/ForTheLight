using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] EnemyAttackController attackController;
    [Header("Jump attack properties")]
    [SerializeField] Collider2D footCollider;
    [SerializeField] float jumpDelay;
    [SerializeField] float dropDelay;
    [SerializeField] float maxHeight;
    [SerializeField] int jumpCount;
    [SerializeField] int maxJumpCount;
    bool isJumping = false;
    Coroutine jumpAttackCoroutine;

    protected new void OnEnable()
    {
        base.OnEnable();
        attackController = GetComponent<EnemyAttackController>();
        JumpAttack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Ground) && isJumping)
        {
            Debug.Log("stop");
            moveController.Stop();
            ToggleFoot();
        }
    }

    void JumpAttack()
    {
        jumpAttackCoroutine = StartCoroutine(CR_JumpAttack());
    }

    IEnumerator CR_JumpAttack()
    {
        isJumping = true;
        for (int i = 0; i < jumpCount; i++)
        {
            moveController.Move(Vector3.up);

            while (transform.position.y < maxHeight) yield return null;

            moveController.Stop();
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(dropDelay);
            
            moveController.Move(Vector3.down);
            ToggleFoot();
            yield return new WaitForSeconds(jumpDelay);
        }
        isJumping = false;
    }

    void ToggleFoot() {
        if (footCollider == null) return;
        footCollider.enabled = !footCollider.enabled;
    }

    void NextAttack()
    {
        if (jumpAttackCoroutine != null) StopCoroutine(jumpAttackCoroutine);
    }
}
