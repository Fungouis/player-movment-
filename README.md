using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    private int currentHealth; // Current health

    public Slider healthSlider; // Reference to the UI slider element for displaying health
    public GameObject objectToDisableOnDeath; // Reference to the GameObject to disable upon player death
    public GameObject gameOverPanel; // Reference to the game over panel GameObject
    public Button resetButton; // Reference to the reset button
    public Button mainMenuButton; // Reference to the main menu button

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
        UpdateHealthSlider(); // Update UI slider element with current health

        // Add onClick listeners to the buttons
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetLevel);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
    }

    // Method to take damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce current health by the damage amount

        if (currentHealth <= 0)
        {
            currentHealth = 0; // Ensure health doesn't go below zero
            Die(); // Call Die method if health drops to or below zero
        }

        UpdateHealthSlider(); // Update UI slider element with current health
    }

    // Method to handle player death
    void Die()
    {
        // Implement player death logic here, such as respawning or game over
        Debug.Log("Player died!");

        // Activate the game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Disable object on death
        if (objectToDisableOnDeath != null)
        {
            objectToDisableOnDeath.SetActive(false);
        }

        // Freeze the game
        Time.timeScale = 0f;
    }

    // Method to update UI slider element with current health
    void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth; // Update slider value based on current health
        }
    }

    // Method to reset the level
    void ResetLevel()
    {
        // Reload the current scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    // Method to go to the main menu
    void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Main menu"); // Replace "MainMenu" with the name of your main menu scene
    }
}
