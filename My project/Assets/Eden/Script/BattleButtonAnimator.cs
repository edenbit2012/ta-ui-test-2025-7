using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BattleButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;
    private RectTransform rectTransform;
    private Tween autoSpinShake;
    private bool isHolding = false;
    private float holdTime = 0f;

    [Header("Threshold")]
    public float holdThreshold = 1f;

    [Header("Shake Settings")]
    public float shakeStrength = 5f;
    public float shakeDuration = 999f;
    public int shakeVibrato = 10;

    [Header("Color Settings")]
    public Image buttonImage;
    public Color defaultColor = Color.white;
    public Color autoSpinColor = Color.red;

    [Header("Text Settings")]
    public TMP_Text battleText;
    public Color defaultTextColor = Color.white;
    public Color autoSpinTextColor = Color.yellow;

    public GameObject holdTextObject;
    public GameObject releaseTextObject;

    [Header("Boss Icon")]
    public GameObject bossIcon; // 👈 האובייקט שיכיל את הספרייט של הגולגולת

    void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();

        // ודא ש־Release מוסתר בתחילת המשחק
        if (releaseTextObject != null) releaseTextObject.SetActive(false);
        if (bossIcon != null) bossIcon.SetActive(false);
    }

    void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;

            if (holdTime >= holdThreshold && !animator.GetBool("AutoSpin"))
            {
                animator.SetBool("AutoSpin", true);

                // רעידה
                autoSpinShake = rectTransform.DOShakeAnchorPos(
                    duration: shakeDuration,
                    strength: new Vector2(shakeStrength, 0f),
                    vibrato: shakeVibrato,
                    randomness: 0,
                    snapping: false,
                    fadeOut: false
                ).SetLoops(-1);

                // שינוי צבעים
                if (buttonImage != null) buttonImage.color = autoSpinColor;
                if (battleText != null) battleText.color = autoSpinTextColor;

                // החלפת טקסטים
                if (holdTextObject != null) holdTextObject.SetActive(false);
                if (releaseTextObject != null) releaseTextObject.SetActive(true);

                // הצגת בוס אייקון
                if (bossIcon != null) bossIcon.SetActive(true);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdTime = 0f;

        animator.SetTrigger("Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;

        if (holdTime < holdThreshold)
        {
            animator.ResetTrigger("Pressed");
        }

        animator.SetBool("AutoSpin", false);

        // עצירת רעידה
        if (autoSpinShake != null && autoSpinShake.IsActive())
        {
            autoSpinShake.Kill();
            autoSpinShake = null;
        }

        // חזרה לצבע רגיל
        if (buttonImage != null) buttonImage.color = defaultColor;
        if (battleText != null) battleText.color = defaultTextColor;

        // החזרת טקסטים
        if (holdTextObject != null) holdTextObject.SetActive(true);
        if (releaseTextObject != null) releaseTextObject.SetActive(false);

        // הסתרת בוס אייקון
        if (bossIcon != null) bossIcon.SetActive(false);
    }
}
