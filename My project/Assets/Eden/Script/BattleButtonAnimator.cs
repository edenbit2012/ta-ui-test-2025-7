using UnityEngine;
using UnityEngine.EventSystems;

public class BattleButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;
    private bool isHolding = false;
    private float holdTime = 0f;

    [SerializeField]
    private float holdThreshold = 1.0f; // כמה זמן צריך להחזיק כדי לעבור ל-AutoSpin

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;

            if (holdTime >= holdThreshold)
            {
                if (!animator.GetBool("AutoSpin"))
                {
                    Debug.Log("🚀 עבר הזמן הדרוש — הפעלת AutoSpin");
                    animator.SetBool("AutoSpin", true);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdTime = 0f;

        Debug.Log("👆 נלחץ על הכפתור (PointerDown)");
        animator.SetTrigger("Pressed"); // ✅ רק Trigger – אין צורך בבול
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;

        Debug.Log("👆 שחרור הלחיצה (PointerUp)");

        if (holdTime < holdThreshold)
        {
            Debug.Log("💥 שחרור מוקדם – טריגר רגיל של Pressed");
            animator.ResetTrigger("Pressed"); // מוודא שהטריגר לא תקוע
        }

        animator.SetBool("AutoSpin", false); // תמיד מבטל AutoSpin בסיום

        Debug.Log("🔁 חזרה ל־IDLE – ביטול מצב AutoSpin");
    }
}
