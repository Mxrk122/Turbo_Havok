using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int enemiesRemaining;

    void Update()
    {
        // Inicializar el conteo de enemigos
        int enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;

        int player = GameObject.FindGameObjectsWithTag("Player").Length;

        if (enemiesRemaining == 0){
            SceneManager.LoadScene("Win");
        }

        if (player == 0){
            SceneManager.LoadScene("Loose");
        }
    }

    // Método para llamar cuando el jugador muere
    public void PlayerDied()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    // Método para llamar cuando el jugador gana
    private void PlayerWins()
    {
        SceneManager.LoadScene("WinScene");
    }
}
