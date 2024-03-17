using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
        // Start is called before the first frame update
        Player _player;
        public Animator _animator;
        AnimState _animState = AnimState.Idle;
        AnimState _prevAnimState = AnimState.Idle;

        Vector3 _moveDir = Vector3.zero;
        Vector3 _lookDir = Vector3.zero;

        private int _upperBodyLayerIndex = -1;
        private int _additiveLayerIndex = -1;

        public bool _isGetHit = false;
	public bool _isPunching = false;
	public bool _isDeath = false;
        Transform handTransform;

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
                Death = 9,
                Spawn = 10,
        }

        enum MoveDir
        {
                idel = 0, 
        } 

    void Start()
    {
                 _animator = GetComponent<Animator>();
                _player = gameObject.GetComponent<Player>(); 
                _animState = AnimState.RunBackward;

		_upperBodyLayerIndex = _animator.GetLayerIndex("UpperBody Layer");
                _additiveLayerIndex = _animator.GetLayerIndex("Additive Layer");
		handTransform = _animator.GetBoneTransform(HumanBodyBones.RightHand);

		//_animState = AnimState.Death;
	}

	// Update is called once per frame
	void Update() 
    {
                UpdateAnimation();
		if (_animState != _prevAnimState)
                        SetAnimation();  
        }

        public void SetDir(Vector3 moveDir, Vector3 lookDir)
        {
                _moveDir = moveDir;
                _lookDir = lookDir;
        }
          
        void Event_OnDiePose()
        { 
                _animator.speed = 0.0f;
                 
	} 



	void Event_OnAttack()
	{
		GameObject fireObj = _player.GetFireObject();
		FireManager fireM = fireObj.GetComponent<FireManager>();
                Vector3 fireDir = (_player._lookPoint - handTransform.position);
                fireDir.y = 0;
                fireDir.Normalize();

		fireM.Fire(_player.PlayerId, handTransform.position, fireDir);
                //_animator.SetLayerWeight(_upperBodyLayerIndex, 0.0f);
	}  
         
	void UpdateAnimation()
        {
                if (_animator == null) 
                        return;
		// State Check
                  
		_moveDir.Normalize(); 
		_lookDir.Normalize();
		Vector3 rightDir = Vector3.Cross(new Vector3(0.0f, 1.0f, 0.0f), _lookDir);

		float forwardRate = Vector3.Dot(_moveDir, _lookDir);
		float rightStrafeRate = Vector3.Dot(_moveDir, rightDir);

                 

                if (_isDeath)
                {
                        _animState = AnimState.Death;
                        return;
                }

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

	}
         void PlayAnimation(string animName, float blendSpeed)
        {
		_animator.CrossFade(animName, blendSpeed);
	//	_armorAnimator.CrossFade(animName, blendSpeed);
	}
         
        public void PlayAttackAnimatio()
        {
               _animator.SetLayerWeight(_upperBodyLayerIndex, 1.0f); 
		_animator.Play("PunchLeft"); 
                 
	}

	void SetAnimation()
        {

                float blendSpeed = 0.2f;
		switch (_animState)
                { 
                        case AnimState.Idle:
				PlayAnimation("Idle", blendSpeed);
                                break;
			case AnimState.RunForward:
				PlayAnimation("RunForward", blendSpeed);
				break;
			case AnimState.RunLeft:
				PlayAnimation("RunLeft", blendSpeed);
				break;
			case AnimState.RunRight:
				PlayAnimation("RunRight", blendSpeed);
				break;
			case AnimState.RunBackward:
				PlayAnimation("RunBackward", blendSpeed);
				break;
			case AnimState.RunBackwardLeft:
				PlayAnimation("RunBackwardLeft", blendSpeed);
				break;
			case AnimState.RunBackwardRight:
				PlayAnimation("RunBackwardRight", blendSpeed);
				break;
			case AnimState.StrafeLeft:
                                PlayAnimation("StrafeLeft", blendSpeed);
				break;
			case AnimState.StrafeRight:
                                PlayAnimation("StrafeRight", blendSpeed);
				break;
                        case AnimState.Death:
                                PlayAnimation("Death", blendSpeed);
                                break; 
			case AnimState.Spawn:
				PlayAnimation("Spawn", blendSpeed);
				break;

		}
                _prevAnimState = _animState;

	}
}
