using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Vertical : MonoBehaviour {
    // ------------------------- Private attributes ------------------------- //

    /// <value> To check if the box has to be moved up or down. </value>
    private bool _goUp;

    /// <value> Starting position of the box. </value>
    private Vector3 _topEdgePosition;

    /// <value> Farthest position where the box can reach. </value>
    private Vector3 _downEdgePosition;

    /// <value> The speed of the box. </value>
    private float _boxSpeed;

    // ----------------------------- Private Method ---------------------------- //

    /// <summary>
        /// Check if the given position is between the edges.
    /// </summary>
    /// <param name="yPosition"> The position to evaluate. </param>
    /// <returns>If the position is in y range. </returns>
    private bool isInYRange(float yPosition) {
        return (yPosition > _downEdgePosition.y) && (yPosition < _topEdgePosition.y);
    }

    // ----------------------------- Unity methods ----------------------------- //

    /// <summary>
        /// Set the edges positions and the speed of the box.
    /// </summary>
    void Start() {
        _goUp = true;
        _downEdgePosition = new Vector3(4.46f, 0.06f, 2.76f);
        _topEdgePosition = new Vector3(_downEdgePosition.x, _downEdgePosition.y + 2, _downEdgePosition.z);
        _boxSpeed = 0.03f;
    }

    /// <summary>
        /// Keep the basket moving up and down.
    /// </summary>
    void Update() {
        float currentYPosition = transform.position.y;

        if (!isInYRange(currentYPosition)) _goUp = !_goUp;

        float finalSpeed = _boxSpeed * (_goUp ? 1 : -1);
        transform.Translate(Vector3.up * finalSpeed);
    }
}