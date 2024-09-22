using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card m_card;
    public GameObject m_picture;
    public GameObject m_cost;
    public GameObject m_name;
    public GameObject m_type;
    public GameObject m_utilities;
    public GameObject m_frameworks;
    public GameObject m_description;
    // public Ability m_ability;

    public bool m_active = false;

    private Image m_image;
    private TMP_Text m_Tcost;
    private TMP_Text m_Tname;
    private TMP_Text m_Ttype;
    private TMP_Text m_Tutilities;
    private TMP_Text m_Tframeworks;
    private TMP_Text m_Tdescription;

    private GameObject player;
    private PlayerManager playerManager;
    private AbilityManager abilityManager;

    public Animator cardanimator;

    private void Awake()
    {
        m_image = m_picture.GetComponent<Image>();
        m_Tcost = m_cost.GetComponent<TMP_Text>();
        m_Tname = m_name.GetComponent<TMP_Text>();
        m_Ttype = m_type.GetComponent<TMP_Text>();
        m_Tutilities = m_utilities.GetComponent<TMP_Text>();
        m_Tframeworks = m_frameworks.GetComponent<TMP_Text>();
        m_Tdescription = m_description.GetComponent<TMP_Text>();

        player = GameObject.FindWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();

        cardanimator = this.GetComponent<Animator>();
        abilityManager = GameObject.Find("Ability Manager").GetComponent<AbilityManager>();
    }

    public void SetCard(Card card)
    {
        m_card = card;

        m_image.sprite = card.image;
        m_Tcost.text = card.cost.ToString();
        m_Tname.text = card.name;
        m_Ttype.text = card.type.ToString();
        m_Tutilities.text = card.utilities.ToString();
        m_Tframeworks.text = card.frameworks.ToString();
        m_Tdescription.text = card.description;

        card.ability = abilityManager.AssignAbility(card.abilityName);
        // TODO make description what its meant 2 be
        if (card.ability != null)
        {
            m_Tdescription.text = $"{card.abilityName} {card.abilityCost} - {card.ability.Description}";
        }
    }
    public void SetActiveCard()
    {
        // add animation of picking it from hand?
        playerManager.SelectCard(gameObject);
        if (m_active)
        {
            transform.Translate(0, -10f, 0);
            m_active = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (playerManager.phase != Phase.Setup)
        {
            return;
        }

        if (playerManager.selectedCard == gameObject)
        {
            return;
        }
        transform.Translate(0, 10f, 0);
        m_active = true;


    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (playerManager.phase != Phase.Setup)
        {
            return;
        }

        if (playerManager.selectedCard == gameObject)
        {
            return;
        }
        transform.Translate(0, -10f, 0);
        m_active = false;

    }

    public void EndAnimation(string parameter)
    {
        cardanimator.SetBool(parameter, false);
    }
    private void OnDestroy()
    {
        // discard animation
    }
}
