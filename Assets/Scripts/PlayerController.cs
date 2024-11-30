using UnityEngine;

public class PlayerController : MonoBehaviour
{
        [SerializeField] float moveSpeed = 5f; // Karakterin hareket hızı
        [SerializeField] private float RunningSpeed = 10f;
        private float currentSpeed ;
        private Vector3 moveDirection;
        private Animator animator;
    
        private void Start()
        {
            animator = GetComponent<Animator>(); // Animator bileşenini al
            currentSpeed = moveSpeed;
        }
    
        private void Update()
        {
            // Inputları al (Horizontal = A/D veya Sol/Sağ, Vertical = W/S veya Yukarı/Aşağı)
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
    
            // Hareket yönünü belirle
            moveDirection = new Vector3(horizontal, 0, vertical).normalized;
    
            if (moveDirection.magnitude > 0.1f)
            {
                // Karakteri hareket ettir
                transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    
                // Karakterin baktığı yönü değiştir
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    
                // Yürüme animasyonunu oynat
                animator.SetBool("Walking", true);
                animator.SetBool("Running",false);
            }
            else
            {
                // Durma animasyonunu oynat
                animator.SetBool("Walking", false);
                animator.SetBool("Running",false);
            }

            if (Input.GetKey(KeyCode.LeftShift) && moveDirection.magnitude > 0.1f)
            {
                currentSpeed = RunningSpeed;
                animator.SetBool("Walking", false);
                animator.SetBool("Running",true);
            }
            else if (!Input.GetKey(KeyCode.LeftShift) && moveDirection.magnitude > 0.1f)
            {
                currentSpeed = moveSpeed;
                animator.SetBool("Walking", true);
                animator.SetBool("Running",false);
            }
        }
}
