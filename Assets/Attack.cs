using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attack : MonoBehaviour
{
    private int attackForce = 0;
    private SpriteRenderer spriteRenderer;
    bool haveKnightPower = false;
    bool haveWizardPower = false;

    public Sprite knightPic;
    public Sprite wizardPic;
    public Sprite defaultPic;

    public GameObject knightSword;
    public GameObject wizardWand;
    public GameObject MagicBullet;

    public float offsetDistance = 0.5f;
    public LayerMask enemyLayer;
    float attackRange = .5f;

    private float powerDuration = 10f;
    private Coroutine powerTimerCoroutine;
    [SerializeField] private TextMeshProUGUI powerCountdownText;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyLayer = LayerMask.GetMask("Enemy");
        if (powerCountdownText == null)
            Debug.LogWarning("Power text boþ!");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnWeapon();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("knightPotion"))
        {
            Destroy(collision.gameObject);
            haveKnightPower = true;
            haveWizardPower = false;
            BecomeKnight();

            StartPowerTimer();
        }
        else if (collision.gameObject.CompareTag("wizardPotion"))
        {
            Destroy(collision.gameObject);
            haveWizardPower = true;
            haveKnightPower = false;
            BecomeWizard();

            StartPowerTimer();
        }
    }

    void BecomeKnight()
    {
        spriteRenderer.sprite = knightPic;
        attackForce = 30;
    }

    void BecomeWizard()
    {
        spriteRenderer.sprite = wizardPic;
    }

    public void ResetToDefault()
    {
        haveKnightPower = false;
        haveWizardPower = false;
        spriteRenderer.sprite = defaultPic;
        attackForce = 0;
        powerTimerCoroutine = null;
    }

    void StartPowerTimer()
    {
        if (powerTimerCoroutine != null)
            StopCoroutine(powerTimerCoroutine);
        powerTimerCoroutine = StartCoroutine(PowerCountdown());
    }

    IEnumerator PowerCountdown()
    {
        float remainingTime = powerDuration;

        while (remainingTime > 0)
        {
            powerCountdownText.text = "Strong: " + Mathf.FloorToInt(remainingTime).ToString();
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        powerCountdownText.text = "";
        ResetToDefault();
    }

    void SpawnWeapon()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        Vector3 spawnPos = transform.position + direction * offsetDistance;

        GameObject instantiatedWeapon = null;

        if (haveKnightPower)
        {
            instantiatedWeapon = Instantiate(knightSword, spawnPos, Quaternion.identity);
            instantiatedWeapon.transform.parent = transform;
            Destroy(instantiatedWeapon, 0.2f);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                CharacterStats enemyStats = enemy.GetComponent<CharacterStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(attackForce);
                }
            }
        }
        else if (haveWizardPower)
        {
            instantiatedWeapon = Instantiate(wizardWand, spawnPos, Quaternion.identity);

            Transform firePoint = instantiatedWeapon.transform.Find("firePoint");
            if (firePoint != null && MagicBullet != null)
            {
                GameObject bullet = Instantiate(MagicBullet, firePoint.position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.SetDirection(direction);
                }
            }
            Destroy(instantiatedWeapon, 0.2f);
        }
    }
        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
