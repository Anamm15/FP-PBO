using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Health, Damage, Armor }
    public ItemType itemType;

    public void ApplyEffect(Player player)
    {
        switch (itemType)
        {
            case ItemType.Health:
                player.IncreaseHealth();
                break;
            case ItemType.Damage:
                player.IncreaseDamage();
                break;
            case ItemType.Armor:
                player.IncreaseArmor();
                break;
        }

        Destroy(gameObject);
    }
}
