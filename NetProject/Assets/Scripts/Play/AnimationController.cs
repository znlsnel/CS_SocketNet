using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
        // Start is called before the first frame update
        Animator _animator;
        [SerializeField] Animator _armorAnimator;
        AnimState _animState = AnimState.Idle;
        AnimState _prevAnimState = AnimState.Idle;

        Vector3 _moveDir = Vector3.zero;
        Vector3 _lookDir = Vector3.zero;
          

        enum AnimState
        {
                Idle = 0,
                RunForward = 1,
                RunLeft = 2,
                RunRight = 3,
                StrafeLeft = 4,
                StrafeRight = 5,
                RunBackward = 6,
                RunBackwardLeft = 7,
                RunBackwardRight = 8, 
        }

        enum MoveDir
        {
                idel = 0, 
        } 

    void Start()
    {
        _animator = GetComponent<Animator>();
                _animState = AnimState.RunBackward;
    }
         
    // Update is called once per frame
    void Update() 
    {
                if (UpdateAnimation())
                        SetAnimation(); 
        }

        public void SetDir(Vector3 moveDir, Vector3 lookDir)
        {
                _moveDir = moveDir;
                _lookDir = lookDir;
        }
         
        bool UpdateAnimation()
        {
                if (_animator == null) 
                        return false;
		// State Check
                  
		_moveDir.Normalize();
		_lookDir.Normalize();
		Vector3 rightDir = Vector3.Cross(new Vector3(0.0f, 1.0f, 0.0f), _lookDir);

		float forwardRate = Vector3.Dot(_moveDir, _lookDir);
		float rightStrafeRate = Vector3.Dot(_moveDir, rightDir);
                 
		#region SetAnimState
                 
                int forward = forwardRate > 0.5 ? 1 : forwardRate < -0.5 ? -1 : 0; 
                int right = rightStrafeRate > 0.5 ? 1 : rightStrafeRate < -0.5 ? -1 : 0; 
                  
		if (forward == 1)
                {
                        if (right == 0)
                                _animState = AnimState.RunForward; 
                        else if (right == 1)
                                _animState = AnimState.RunLeft;
                        else
                                _animState = AnimState.RunRight;
                }
                else if (forward == -1) 
                {
			if (right == 0)
				_animState = AnimState.RunBackward;
			else if (right == 1)
				_animState = AnimState.RunBackwardRight;
			else
				_animState = AnimState.RunBackwardLeft;
		}
                else
                {
			if (right == 0) 
				_animState = AnimState.Idle;
			else if (right == 1)
				_animState = AnimState.StrafeRight;
			else 
				_animState = AnimState.StrafeLeft;
		}

        #endregion

                if (_animState == _prevAnimState)
                        return false;
                   
                return true;

	}
         
        void SetAnimation()
        {
                float blendSpeed = 0.2f;
		switch (_animState)
                { 
                        case AnimState.Idle:
                                _animator.CrossFade("Idle", blendSpeed);
				_armorAnimator.CrossFade("Idle", blendSpeed); 
                                break;
			case AnimState.RunForward:
                                _animator.CrossFade("RunForward", blendSpeed);
				_armorAnimator.CrossFade("RunForward", blendSpeed); 
				break;
			case AnimState.RunLeft:
                                _animator.CrossFade("RunLeft", blendSpeed);
				_armorAnimator.CrossFade("RunLeft", blendSpeed); 
				break;
			case AnimState.RunRight:
                                _animator.CrossFade("RunRight", blendSpeed);
				_armorAnimator.CrossFade("RunRight", blendSpeed); 
				break;
			case AnimState.RunBackward:
                                _animator.CrossFade("RunBackward", blendSpeed);
				_armorAnimator.CrossFade("RunBackward", blendSpeed); 
				break;
			case AnimState.RunBackwardLeft:
                                _animator.CrossFade("RunBackwardLeft", blendSpeed);
				_armorAnimator.CrossFade("RunBackwardLeft", blendSpeed); 
				break;
			case AnimState.RunBackwardRight:
                                _animator.CrossFade("RunBackwardRight", blendSpeed);
				_armorAnimator.CrossFade("RunBackwardRight", blendSpeed); 
				break;
			case AnimState.StrafeLeft:
                                _animator.CrossFade("StrafeLeft", blendSpeed);
				_armorAnimator.CrossFade("StrafeLeft", blendSpeed); 
				break;
			case AnimState.StrafeRight:
                                _animator.CrossFade("StrafeRight", blendSpeed);
				_armorAnimator.CrossFade("StrafeRight", blendSpeed); 
				break;
                                 
		}
                _prevAnimState = _animState;

	}
}
