using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    public bool isPlayer = false;

    public event Action OnEnemyDeath;
    public GameObject gameOverPanel;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;   
        yield return new WaitForSeconds(0.1f); 
        spriteRenderer.color = Color.white; 
    }
    private void FixedUpdate()
    {
        if (isPlayer)
            healthText.text = "Health: " + currentHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            if (isPlayer)
            {
                FindObjectOfType<ScoreManager>().CheckHighScore();
                PlayerPrefs.Save();

                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
            }
            else
            {
                Die();
            }
        }
    }

    void Die()
    {

        OnEnemyDeath?.Invoke();

        Destroy(gameObject);
    }
}


