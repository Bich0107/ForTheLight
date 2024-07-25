
using System.Collections;
using UnityEngine;

public class JumpAttack : Projectile
{
    [SerializeField] Collider2D footCollider;
    float jumpDelay;
    float dropDelay;
    float maxHeight;
    int jumpCount;
    bool isJumping;

    public override void Fire(GameObject target) {
        StartCoroutine(CR_JumpAttack());
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Ground) && isJumping)
        {
            movingObject.Stop();
            ToggleFoot(); // turn off foot after touching the ground
        }
    }

    IEnumerator CR_JumpAttack()
    {
        isJumping = true;
        for (int i = 0; i < jumpCount; i++)
        {
            movingObject.Move(Vector3.up); // jump up

            while (transform.position.y < maxHeight) yield return null; // wait until that max height

            // stop and drop down after some delay
            movingObject.Stop();
            transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(dropDelay);
            movingObject.Move(Vector3.down);

            // turn on foot to detect the ground
            ToggleFoot();
            yield return new WaitForSeconds(jumpDelay);
        }
        isJumping = false;
    }

    void ToggleFoot() {
        if (footCollider == null) return;
        footCollider.enabled = !footCollider.enabled;
    }
}
