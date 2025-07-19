using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BattleButtonJuice : MonoBehaviour
{
    [Header("Target")]
    public RectTransform targetToShake; // האובייקט שירעד (בד"כ זה הכפתור)

    [Header("Animation")]
    public Animator animator; // ה־Animator של הכפתור

    [Header("Shake Settings")]
    public float shakeDuration = 0.2f;
    public float shakeStrength = 0.05f; // גודל השייק – נשתמש ב־Scale, אז ערך קטן
    public int vibrato = 10; // כמה פעמים ירטוט

    private Button button;

    void Start()
    {
        // קישור לכפתור
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(HandleClick);
        }
    }

    void HandleClick()
    {
        if (targetToShake != null)
        {
            targetToShake.DOKill(); // מבטל Shake קודם אם יש
            targetToShake.DOShakeScale(shakeDuration, shakeStrength, vibrato);
        }

        if (animator != null)
        {
            animator.SetTrigger("Pressed");
        }
    }
}
