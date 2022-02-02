using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeMatrix
{
    public class Utility
    {
        public class Time
        {
            // Update
            // Start
            // Resume 
            // Pause
            // Record
            // Reset

            public float max { get; set; }
            public float elapsed { get; set; }
            public float scale { get; set; }
            public float remaining { get; set; }

            public List<float> recorded { get; set; }

            public bool isActive { get; set; }
            public bool isDone { get; set; }

            TYPE type { get; set; }

            /// <summary>
            /// Initializes the timer (constructor), then assign necessary values depending on the timer type
            /// </summary>
            /// <param name="type">The type of timer to be run</param>
            /// <param name="max">The maximum elapsed time to reset the timer, ignored if timer type is STOPWATCH</param>
            /// <param name="scale">Determines how fast the timer is</param>
            public Time(TYPE type = TYPE.STOPWATCH, float max = 60, float scale = 1)
            {
                this.type = type;
                this.max = max;
                this.scale = scale;

                recorded = new List<float>();

                if (type == TYPE.COUNTUP)
                {
                    elapsed = 0;
                    remaining = max;
                }

                else if (type == TYPE.COUNTDOWN)
                {
                    elapsed = max;
                    remaining = max;
                }

                else
                {
                    remaining = 0;
                }

                isActive = true;
            }

            /// <summary>
            /// Run The timer
            /// </summary>
            /// <returns></returns>
            public bool Update()
            {

                if (isActive)
                {
                    isDone = false;

                    if (type == TYPE.COUNTUP && type == TYPE.STOPWATCH)
                    {
                        elapsed += scale * UnityEngine.Time.deltaTime;
                        remaining -= elapsed;

                        if (type == TYPE.COUNTUP)
                        {
                            if (elapsed >= max)
                            {
                                isDone = true;
                                Reset();
                                return true;
                            }
                        }
                    }

                    if (type == TYPE.COUNTDOWN)
                    {
                        elapsed -= scale * UnityEngine.Time.deltaTime;
                        remaining = elapsed;

                        if (elapsed <= 0)
                        {
                            isDone = true;
                            Reset();
                            return true;
                        }
                    }
                }

                return false;
            }

            public void Reset()
            {
                switch (type)
                {
                    default:
                        break;

                    case TYPE.COUNTUP:
                        elapsed = 0;
                        break;

                    case TYPE.COUNTDOWN:
                        elapsed = max;
                        break;
                }
            }

            public void Pause()
            {
                isActive = false;
            }

            public void Resume()
            {
                isActive = true;
            }

            public void Record()
            {
                recorded.Add(elapsed);
            }
            public enum TYPE
            {
                COUNTUP,
                COUNTDOWN,
                STOPWATCH
            }
        }

        public class Convert
        {
            public static Vector2 ToLocal(Vector2 reference, Vector2 target)
            {
                return (target / reference) / 100;
            }

            public static float ToLocal(float reference, float target)
            {
                return (target / reference) / 100;
            }


            public static Vector2 ToWorld(Vector2 reference, Vector2 target)
            {
                return (target * reference) * 10;
            }

            public static float ToWorld(float reference, float target)
            {
                return (target * reference);
            }
        }

        public class Tween
        {
            public static Vector3 PointTo2D(Vector3 target, Vector3 current, Quaternion currentRotation)
            {
                Vector3 dir = target - current;
                Vector3 angle;
                angle.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                angle.x = currentRotation.x;
                angle.y = currentRotation.y;

                return angle;
            }
        }
    }
}
