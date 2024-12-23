/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class ButtonHoverInfo : MonoBehaviour
{
    [Header("UI Settings")]
    public List<Button> buttons = new List<Button>(); // Liste des boutons
    public List<GameObject> prefabs = new List<GameObject>(); // Liste des prefabs associ�s
    public TextMeshProUGUI infoText; // Texte du panneau Info_panel (TMP)

    void Start()
    {
        // V�rifie la coh�rence des listes
        if (buttons.Count != prefabs.Count)
        {
            Debug.LogError("Le nombre de boutons ne correspond pas au nombre de prefabs !");
            return;
        }

        // Associer les �v�nements de survol � chaque bouton
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // N�cessaire pour capturer la bonne valeur dans une closure
            EventTrigger trigger = AddEventTrigger(buttons[i]);

            // Ajouter les �v�nements
            AddEvent(trigger, EventTriggerType.PointerEnter, () => ShowDescription(index));
            AddEvent(trigger, EventTriggerType.PointerExit, HideDescription);
        }
    }

    EventTrigger AddEventTrigger(Button button)
    {
        // Ajoute un EventTrigger au bouton s'il n'existe pas d�j�
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();
        return trigger;
    }

    void AddEvent(EventTrigger trigger, EventTriggerType eventType, System.Action callback)
    {
        // Cr�e une entr�e pour un �v�nement sp�cifique
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((_) => callback());
        trigger.triggers.Add(entry);
    }

    void ShowDescription(int index)
    {
        // R�cup�re les informations du prefab associ�
        GameObject prefab = prefabs[index];
        Tower prefabInfo = prefab.GetComponent<Tower>();

        if (prefabInfo != null && infoText != null)
        {
            // G�n�re une description � partir des donn�es du prefab
            string description = $"{prefabInfo.name}\n\nCoût: {prefabInfo.cost}\n\nDégâts: {prefabInfo.damage}\n\nDescription: \n{prefabInfo.description}";
            infoText.text = description;
        }
        else
        {
            Debug.LogError($"Prefab {prefab.name} n'a pas de script 'PrefabInfo' attach� !");
        }
    }

    void HideDescription()
    {
        // Vide la description
        if (infoText != null)
        {
            infoText.text = string.Empty;
        }
    }
}
*/
///*
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class ButtonHoverInfo : MonoBehaviour
{
    [Header("UI Settings")]
    public List<Button> buttons = new List<Button>(); // Liste des boutons
    public List<GameObject> prefabs = new List<GameObject>(); // Liste des prefabs associ�s
    public TextMeshProUGUI infoText; // Texte du panneau Info_panel (TMP)

    private Tower currentlyHoveredTower = null;

    void Start()
    {
        // Vérifie la cohérence des listes
        if (buttons.Count != prefabs.Count)
        {
            Debug.LogError("Le nombre de boutons ne correspond pas au nombre de prefabs !");
            return;
        }

        // Associer les événements de survol à chaque bouton
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // Nécessaire pour capturer la bonne valeur dans une closure
            EventTrigger trigger = AddEventTrigger(buttons[i]);

            // Ajouter les événements
            AddEvent(trigger, EventTriggerType.PointerEnter, () => ShowDescription(index));
            AddEvent(trigger, EventTriggerType.PointerExit, HideDescription);
        }
    }

    void Update()
    {
        // Vérifier si la souris survole un objet avec le script Tower
        CheckHoverOverTower();
    }

    EventTrigger AddEventTrigger(Button button)
    {
        // Ajoute un EventTrigger au bouton s'il n'existe pas déjà
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();
        return trigger;
    }

    void AddEvent(EventTrigger trigger, EventTriggerType eventType, System.Action callback)
    {
        // Crée une entrée pour un événement spécifique
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((_) => callback());
        trigger.triggers.Add(entry);
    }

    void ShowDescription(int index)
    {
        // Récupère les informations du prefab associé
        GameObject prefab = prefabs[index];
        Tower prefabInfo = prefab.GetComponent<Tower>();

        if (prefabInfo != null && infoText != null)
        {
            // Génère une description à partir des données du prefab
            string description = $"{prefabInfo.name}\n\nCoût: {prefabInfo.cost}\n\nDégâts: {prefabInfo.damage}\n\nDescription: \n{prefabInfo.description}";
            infoText.text = description;
        }
        else
        {
            Debug.LogError($"Prefab {prefab.name} n'a pas de script 'Tower' attaché !");
        }
    }

    void HideDescription()
    {
        // Vide la description
        if (infoText != null)
        {
            infoText.text = string.Empty;
        }
    }

    void CheckHoverOverTower()
    {
        // Récupère la position de la souris dans le monde 2D
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Vérifie s'il y a un collider à cet endroit
        Collider2D hit = Physics2D.OverlapPoint(mousePosition);

        if (hit != null)
        {
            Tower towerInfo = hit.GetComponent<Tower>();
            if (towerInfo != null)
            {
                if (currentlyHoveredTower != towerInfo)
                {
                    // Deactivate the range of the previously hovered tower
                    if (currentlyHoveredTower != null)
                    {
                        ToggleRange(currentlyHoveredTower, false);
                    }

                    // Update the currently hovered tower
                    currentlyHoveredTower = towerInfo;

                    // Activate the range of the new tower
                    ToggleRange(currentlyHoveredTower, true);
                }

                // Update the description text
                if (infoText != null)
                {
                    string description = $"{towerInfo.name}\n\nCoût: {towerInfo.cost}\n\nDégâts: {towerInfo.damage}\n\nDescription: \n{towerInfo.description}";
                    infoText.text = description;
                }
            }
        }
        else
        {
            // If no tower is hovered, deactivate the range of the last hovered tower
            if (currentlyHoveredTower != null)
            {
                ToggleRange(currentlyHoveredTower, false);
                currentlyHoveredTower = null;
            }

            // Clear the description
            HideDescription();
        }
    }

    void ToggleRange(Tower tower, bool isActive)
    {
        Transform range = tower.transform.Find("Range");
        if (range != null)
        {
            range.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning($"Tower {tower.name} does not have a child object named 'Range'!");
        }
    }
}
