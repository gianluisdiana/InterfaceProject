using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMove : MonoBehaviour {
    // ------------------------- Private attributes ------------------------- //

    /// <value> To check if the basket has to be moved to the right or not. </value>
    private bool _goRight;

    /// <value> Starting position of the basket. </value>
    private Vector3 _leftEdgePosition;

    /// <value> Farthest position where the basket can reach. </value>
    private Vector3 _rightEdgePosition;

    /// <value> The speed of the basket. </value>
    private float _basketSpeed;

    // ----------------------------- Private Method ---------------------------- //

    /// <summary>
        /// Check if the given position is between the edges.
    /// </summary>
    /// <param name="xPosition"> The position to evaluate. </param>
    /// <returns>If the position is in x range. </returns>
    private bool isInXRange(float xPosition) {
        return (xPosition > _leftEdgePosition.x) && (xPosition < _rightEdgePosition.x);
    }

    // ----------------------------- Unity methods ----------------------------- //

    /// <summary>
        /// Set the edges positions and the speed of the basket.
    /// </summary>
    private void Start() {
        _goRight = true;
        _leftEdgePosition = new Vector3(-3.5f, 0, 2);
        _rightEdgePosition = new Vector3(_leftEdgePosition.x + 4, _leftEdgePosition.y, _leftEdgePosition.z);
        _basketSpeed = 0.003f;
    }

    /// <summary>
        /// Keep the basket moving side by side.
    /// </summary>
    void Update() {
        float currentXPosition = transform.position.x;

        if (!isInXRange(currentXPosition)) _goRight = !_goRight;

        float finalSpeed = _basketSpeed * (_goRight ? 1 : -1);
        transform.Translate(Vector3.right * finalSpeed);
    }
}