using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;

public class HighlightCard : MonoBehaviour
{
    public Card m_card;
    public GameObject m_picture;
    public GameObject m_background;
    public GameObject m_cost;
    public GameObject m_name;
    public GameObject m_type;
    public GameObject m_resource;
    public GameObject m_description;
    public AbilityManager abilityManager;

    private Image m_image;
    private Image m_Tbackground;
    private TMP_Text m_Tcost;
    private TMP_Text m_Tname;
    private TMP_Text m_Ttype;
    private TMP_Text m_TResource;
    private TMP_Text m_Tdescription;
    private void Awake()
    {
        
        m_image = m_picture.GetComponent<Image>();
        m_Tbackground = m_background.GetComponent<Image>();
        m_Tcost = m_cost.GetComponent<TMP_Text>();
        m_Tname = m_name.GetComponent<TMP_Text>();
        m_Ttype = m_type.GetComponent<TMP_Text>();
        m_TResource = m_resource.GetComponent<TMP_Text>();
        m_Tdescription = m_description.GetComponent<TMP_Text>();

        abilityManager = GameObject.Find("Ability Manager").GetComponent<AbilityManager>();
    }
    public void SetCard(Card card)
    {
        m_card = card;
        m_image.sprite = card.image;
        m_Tbackground.sprite = card.background;
        m_Tcost.text = card.cost.ToString("N0");
        m_Tname.text = card.name;
        m_Ttype.text = card.type.ToString();
        m_TResource.text = card.resource.ToString();
        m_Tdescription.text = card.description;

        card.ability = abilityManager.AssignAbility(card.abilityName);
        // TODO make description what its meant 2 be
        if (card.ability != null)
        {
            m_Tdescription.text = $"{card.abilityName} {card.abilityCost.ToString("N0")} - Level: {card.abilityLevel}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
