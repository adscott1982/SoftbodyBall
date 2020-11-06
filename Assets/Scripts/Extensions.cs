using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        public static void ClampXMaxSpeed(this Rigidbody2D rb, float maxSpeed)
        {
            // If not currently at max speed, return
            if (!(Math.Abs(rb.velocity.x) > maxSpeed))
            {
                return;
            }

            var velocity = rb.velocity;
            velocity.x = velocity.x > 0 ? maxSpeed : -maxSpeed;
            rb.velocity = velocity;
        }

        public static void ClampYMaxSpeed(this Rigidbody2D rb, float maxSpeed)
        {
            // If not currently at max speed, return
            if (!(Math.Abs(rb.velocity.y) > maxSpeed))
            {
                return;
            }

            var velocity = rb.velocity;
            velocity.y = velocity.y > 0 ? maxSpeed : -maxSpeed;
            rb.velocity = velocity;
        }

        public static IEnumerator CoroutineRunOnDelay(this Action action, float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            action.Invoke();
        }

        public static void DecelerateX(this Rigidbody2D rb, float deceleration)
        {
            var velocity = rb.velocity;
            deceleration *= Time.fixedDeltaTime;

            if (Mathf.Abs(velocity.x) - deceleration < 0)
            {
                velocity.x = 0f;
            }
            else
            {
                velocity.x = velocity.x > 0 ? velocity.x - deceleration : velocity.x + deceleration;
            }

            rb.velocity = velocity;
        }

        public static void FlipXScale(this Transform transform)
        {
            var currentScale = transform.localScale;
            currentScale.x = -currentScale.x;
            transform.localScale = currentScale;
        }

        public static bool IsApproxZero(this float value)
        {
            return Math.Abs(value) < AndyTools.FloatEqualityTolerance;
        }

        public static Color ChangeAlpha(this Color color, float newAlpha)
        {
            color.a = newAlpha;
            return color;
        }

        public static Color ChangeAlpha(this Color color, byte newAlpha)
        {
            color.a = newAlpha / 255f;
            return color;
        }

        public static int IndexOf<T>(this IReadOnlyList<T> list, T elementToFind)
        {
            int i = 0;
            foreach (T element in list)
            {
                if (Equals(element, elementToFind))
                    return i;
                i++;
            }
            return -1;
        }
    }
}
