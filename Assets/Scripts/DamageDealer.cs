using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    public int GetDamage() => damage;

    public void Hit()
    {
        Destroy(gameObject);
    }
}