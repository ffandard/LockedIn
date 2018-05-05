using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridCollisionResolver : MonoBehaviour {
    public abstract bool CanMoveInDirection( Vector3 moveDirection );
}
