using UnityEngine;
using UnityEngine.UI;

public class TowerInteraction : MonoBehaviour
{
    public GameObject updateButton;  // Le bouton à afficher
    private Collider2D towerCollider; // Le collider de la tour
    private Camera mainCamera; // Référence à la caméra principale
    private Vector3 buttonOffset = new Vector3(0, 1, 0); // Décalage du bouton au-dessus de la tour
    private bool isButtonActive = false; // Vérifier si le bouton est actuellement actif

    void Start()
    {
        // Trouver le collider attaché à la tour
        towerCollider = GetComponent<Collider2D>();

        // Assurez-vous que le bouton est caché au départ
        updateButton.SetActive(false);  // On cache le bouton au démarrage

        // Référencer la caméra principale
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Vérifier si l'utilisateur clique sur la tour
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Vérifier si le clic est dans la zone du collider de la tour
            if (towerCollider.OverlapPoint(mousePos))
            {
                ToggleButton(); // Alterner l'affichage du bouton à chaque clic
            }
        }
    }

    // Fonction pour afficher ou masquer le bouton "Update"
    private void ToggleButton()
    {
        // Si le bouton est actif, on le cache, sinon on l'affiche
        isButtonActive = !isButtonActive;
        updateButton.SetActive(isButtonActive);

        // Si le bouton est affiché, on le place au-dessus de la tour
        if (isButtonActive)
        {
            Vector3 towerPosition = transform.position; // Position de la tour
            updateButton.transform.position = mainCamera.WorldToScreenPoint(towerPosition + buttonOffset);
        }
    }
}
