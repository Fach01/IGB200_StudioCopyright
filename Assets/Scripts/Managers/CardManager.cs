using System;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card m_card;
    public GameObject m_picture;
    public GameObject m_background;
    public GameObject m_cost;
    public GameObject m_name;
    public GameObject m_type;
    public GameObject m_resource;
    public GameObject m_description;

    public bool m_active = false;

    private Image m_image;
    private Image m_Tbackground;
    private TMP_Text m_Tcost;
    private TMP_Text m_Tname;
    private TMP_Text m_Ttype;
    private TMP_Text m_TResource;
    private TMP_Text m_Tdescription;

    private GameObject player;
    private PlayerManager playerManager;
    private AbilityManager abilityManager;

    private int originalIndex;
    public Animator cardanimator;
    private Transform Orientation;
    public bool inanimation = false;

    public bool sick;
    public bool locked = false;

    private float scalar = 0.3f;
    public bool descaled = false;

    private void Awake()
    {
        m_image = m_picture.GetComponent<Image>();
        m_Tbackground = m_background.GetComponent<Image>();
        m_Tcost = m_cost.GetComponent<TMP_Text>();
        m_Tname = m_name.GetComponent<TMP_Text>();
        m_Ttype = m_type.GetComponent<TMP_Text>();
        m_TResource = m_resource.GetComponent<TMP_Text>();
        m_Tdescription = m_description.GetComponent<TMP_Text>();
        player = GameObject.FindWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();

        cardanimator = this.GetComponent<Animator>();
        abilityManager = GameObject.Find("Ability Manager").GetComponent<AbilityManager>();

        Orientation = this.transform;
        sick = false;
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
            m_Tdescription.text = $"{card.abilityName} {card.abilityCost.ToString("N0")} \n Level: {card.abilityLevel}";
        }

        locked = true;
    }
    public void SetActiveCard()
    {
        if (locked) { return; }
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
        if (locked) {
            Debug.Log("doing nothing!");
            return; }
        originalIndex = transform.GetSiblingIndex();
        if (playerManager.phase != Phase.Setup)
        {
            return;
        }

        if (playerManager.selectedCard == gameObject)
        {
            return;
        }
        AudioManager.instance.PlaySFX("Hover Card");
        transform.Translate(0, 10f, 0f);
        transform.SetAsLastSibling();
        m_active = true;


    }

    public void Descale()
    {
        cardanimator.SetBool("Highlighted", false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (locked) { return; }
        if (playerManager.phase != Phase.Setup)
        {
            return;
        }

        if (playerManager.selectedCard == gameObject)
        {
            return;
        }
        transform.Translate(0, -10f, 0);
        transform.SetSiblingIndex(originalIndex);
        m_active = false;

    }
    public void EnterAnimation()
    {
        inanimation = true;
    }
    public void EndAnimation(string parameter)
    {
        cardanimator.SetBool(parameter, false);
        inanimation = false;
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    public void ResetOrientation()
    {
        this.transform.rotation = Orientation.rotation;
    }
    private void OnDestroy()
    {
        // discard animation
    }

    public void ScaleCard()
    {
        Vector3 currentScale = transform.localScale;
        if (descaled)
        {
            transform.localScale = new Vector3(currentScale.x + scalar, currentScale.y + scalar, currentScale.z);
            descaled = false;
        }
        else
        {
            transform.localScale = new Vector3(currentScale.x - scalar, currentScale.y - scalar, currentScale.z);
            descaled = true;
        }
    }

    private void Update()
    {
        if (sick)
        {
            m_image.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            m_image.color = new Color(1f, 1f, 1f, 1f);
            GetComponent<Button>().interactable = true;
        }
    }

    public void Unlock()
    {
        Debug.Log("Unlocking");
        if (playerManager.selectedCard != gameObject && m_active)
        {
            transform.Translate(0, -10f, 0);
            
            m_active = false;
        }
        transform.SetSiblingIndex(originalIndex);
        locked = false;
    }
    public void Deselect()
    {
        transform.Translate(0, -20f, 0);
    }
}
