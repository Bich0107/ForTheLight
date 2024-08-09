using UnityEngine;

[ExecuteAlways]
public class BossBeamAttack : MonoBehaviour
{
    [SerializeField] PlayerCollisionHandler player;
    [SerializeField] float damage = 999999f;

    void Start() {
        player = FindObjectOfType<PlayerCollisionHandler>();    
    }

    void OnParticleTrigger() {
        player?.Hit(damage);
    }
}
