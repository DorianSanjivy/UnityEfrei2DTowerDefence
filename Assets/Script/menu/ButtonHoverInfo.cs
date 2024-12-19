<<<<<<< Updated upstream
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class ButtonHoverInfo : MonoBehaviour
{
    [Header("UI Settings")]
    public List<Button> buttons = new List<Button>(); // Liste des boutons
    public List<GameObject> prefabs = new List<GameObject>(); // Liste des prefabs associés
    public TextMeshProUGUI infoText; // Texte du panneau Info_panel (TMP)
    
    [Header("Prefab Descriptions")]
    public List<string> prefabDescriptions = new List<string>(); // Descriptions des prefabs

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
            string description = $"\n\nCost : {prefabInfo.cost}\n\nDamage : {prefabInfo.damage}\n\nDescription : {prefabDescriptions[index]}";
            infoText.text = description;
        }
        else
        {
            Debug.LogError($"Prefab n'a pas de script 'PrefabInfo' attaché !");
        }
    }

    void HideDescription()
    {
        // Vide la description
        if (infoText != null)
        {
            infoText.text = string.Empty;
        }
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
>>>>>>> Stashed changes
    }
}
