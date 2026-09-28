using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRange = 1.5f;

    private void Update()
    {
        // Movimiento
        Vector3 movementVector = Vector3.zero;

        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = 0;
        movementVector.z = Input.GetAxisRaw("Vertical");

        characterController.Move(movementVector * Time.deltaTime * speed);

        // Mirar hacia el mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Vector3 position = hitInfo.point;
            position.y = transform.position.y;

            transform.LookAt(position);
        }

        // Ataque melee
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hitEnemies = Physics.OverlapSphere(
                transform.position,
                attackRange,
                enemyLayer
            );

            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("Golpeaste a: " + enemy.gameObject.name);
            }
        }
    }
}