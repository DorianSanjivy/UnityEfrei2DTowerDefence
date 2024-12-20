using UnityEngine;
using UnityEngine.UI;

public class UpdateButtonHandler : MonoBehaviour
{
    public GameObject towerGameObject;  // Référence à l'objet de la tour (le GameObject)
    public GameObject newTowerPrefab;   // Le prefab à afficher à la place de la tour actuelle

    private Camera mainCamera; // Référence à la caméra principale

    void Start()
    {
        // Référencer la caméra principale
        mainCamera = Camera.main;

        // Ajouter un listener au bouton pour appeler la méthode OnUpdateClick lorsqu'il est cliqué
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnUpdateClick);
    }

    // Méthode appelée lors du clic sur le bouton
    public void OnUpdateClick()
    {
        print   ("ttt");
        if (towerGameObject != null && newTowerPrefab != null)
        {
            // Obtenir la position de la tour actuelle
            Vector3 towerPosition = towerGameObject.transform.position;

            // Détruire l'ancienne tour
            Destroy(towerGameObject);

            // Instancier le nouveau prefab de la tour à la position de l'ancienne
            Instantiate(newTowerPrefab, towerPosition, Quaternion.identity);

            // Optionnel : Si tu veux aussi cacher le bouton après le clic
            gameObject.SetActive(false);
        }
    }
}
