using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerController : MonoBehaviour
{
        public AnimationController _animCtrl;
        public Rigidbody _rigidBody;
        protected virtual void InitCtrl()
        {
		_animCtrl = GetComponent<AnimationController>();
		_rigidBody = GetComponent<Rigidbody>();
	}
	// Start is called before the first frame update
	void Start()
        {
                InitCtrl();
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
