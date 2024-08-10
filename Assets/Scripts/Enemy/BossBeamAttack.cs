using UnityEngine;

[ExecuteAlways]
public class BossBeamAttack : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] PlayerCollisionHandler player;
    [SerializeField] float damage = 999999f;

    void Start() {
        ps = GetComponent<ParticleSystem>();
        player = FindObjectOfType<PlayerCollisionHandler>();    

        ParticleSystem.TriggerModule triggerModule = ps.trigger;
        triggerModule.AddCollider(player.GetComponent<Collider2D>());
    }

    void OnParticleTrigger() {
        player?.Hit(damage);
    }
}
