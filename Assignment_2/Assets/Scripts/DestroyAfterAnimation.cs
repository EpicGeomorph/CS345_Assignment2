using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

            Destroy(gameObject, info.length);
        } else
        {
            Destroy(gameObject);
        }
    }
}
