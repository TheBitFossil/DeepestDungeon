using UnityEngine;

public class Gravity : MonoBehaviour
{
   [SerializeField] private CharacterController characterController;
   [SerializeField] private float gravity = -9.81f;
   private Vector3 _gravityVector;
   private bool _isGrounded;
   
   private void Update()
   {
      _isGrounded = characterController.isGrounded;

      if (_isGrounded && _gravityVector.y < Mathf.Epsilon)
      {
         _gravityVector.y = 0f;
      }

      _gravityVector.y += gravity * Time.deltaTime;
      
      characterController.Move(_gravityVector * Time.deltaTime);
   }
}