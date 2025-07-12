using UnityEngine;

namespace spaceExplorer.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private InputSystem_Actions action;
        public Transform PlayerTransform { get; set; }
        private Vector3 moveDir;
        private readonly float moveSpeed = 5f;
        public bool IsMoving {  get; private set; }
        private void Start()
        {
            moveDir = Vector3.zero;
            IsMoving = false;
            action = new InputSystem_Actions();
            action.Enable();
        }

        /*private void OnEnable()
        {
            moveDir = Vector3.zero;
            IsMoving = false;
            action = new InputSystem_Actions();
            action.Enable();
        }*/

        private void Update()
        {
            HandleMove();
        }
        public void HandleMove()
        {
            Vector2 inputMoveDir = action.Player.Move.ReadValue<Vector2>();
            moveDir = new Vector3(inputMoveDir.x, inputMoveDir.y, 0);
            if (moveDir != Vector3.zero)
            {
                IsMoving = true;
                moveDir = moveDir.normalized;
                PlayerTransform.position += moveDir * moveSpeed * Time.deltaTime;
            }
            else
            {
                IsMoving = false;
            }
        }
        private void OnDisable()
        {
            action.Disable();
        }
        private void OnDestroy()
        {
            if (action != null)
            {
                action.Disable();
                action.Dispose(); // <Efinal cleanup
            }
        }

    }
}

