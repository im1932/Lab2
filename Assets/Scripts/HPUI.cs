using UnityEngine;

public class HPUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject[] hearts;

    private void Start()
    {
        UpdateHearts();
    }

    private void Update()
    {
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        int currentHP = Mathf.CeilToInt(health.CurrentHealth);

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHP);
        }
    }
}