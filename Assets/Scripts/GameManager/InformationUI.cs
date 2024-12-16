using UnityEngine;
using UnityEngine.UI;

public class InformationUI : MonoBehaviour {
    public Text NameText;
    public Text HealthText;
    public Text ArmotText;
    public Text AttackDamageText;
    public Text KillText;
    public Text HighestKillText;
    public Text HighestTimeText;
    public Text TimerText;
    public Transform Player;

    private static bool IsPlayerAlive;
    private float PlayTime;
    private Player playerScript;

    void Start() {
        IsPlayerAlive = true;
        PlayTime = 0f;
        
        if (Player != null) {
            playerScript = Player.GetComponent<Player>();
        } else {
            Debug.LogError("Player Transform not assigned in InformationUI.");
        }
    }

    void Update()
    {
        UpdateName();
        UpdateTime();
        UpdateHealth();
        UpdateArmor();
        UpdateAttackDamage();
        UpdateKill();
        UpdateHighestKill();
        UpdateHighestTime();
    }

    public void UpdateTime() {
        if (IsPlayerAlive)
        {
            PlayTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(PlayTime / 60F);
            int seconds = Mathf.FloorToInt(PlayTime % 60F);
            TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        } else {
            Statistics.SaveStats((int)PlayTime);
            Statistics.ResetStats();
        }
    }

    public void UpdateName() {
        NameText.text = "Name: " + playerScript.Name;
    }

    public void UpdateHealth() {
        HealthText.text = "Health: " + playerScript.HealthPoint.ToString();
    }
    
    public void UpdateArmor() {
        ArmotText.text = "Armor: " + playerScript.Armor.ToString();
    }

    public void UpdateAttackDamage() {
        AttackDamageText.text = "Attack Damage: " + playerScript.AttackDamage.ToString();
    }

    public void UpdateKill() {
        KillText.text = "Kill: " + Statistics.KillRecord.ToString();
    }

    public void UpdateHighestKill() {
        HighestKillText.text = "Highest Kill: " + Statistics.LoadKillRecord().ToString();
    }

    public void UpdateHighestTime() {
        HighestTimeText.text = "Highest Time: " + Statistics.HighestTimeRecord;
    }

    public static void PlayerDied()
    {
        IsPlayerAlive = false;
    }
}