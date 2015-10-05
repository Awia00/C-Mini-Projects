using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCommon.Models
{
    public class Vector3D
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Length { get; private set; }

        public Vector3D(Point2D start, Point2D end)
        {
            X = end.X - start.X;
            Y = end.Y - start.Y;
            CalculateLength();
        }

        public Vector3D(float x, float y)
        {
            X = x;
            Y = y;
            CalculateLength();
        }

        public Vector3D()
        {
            X = 0;
            Y = 0;
            Length = 0;
        }

        private void CalculateLength()
        {
            Length = (float)Math.Sqrt(X*X + Y*Y);
        }

        private Vector3D Normalize()
        {
            return new Vector3D(X/Length, Y/Length);
        }

        public Vector3D VectorTimesFactor(float factor)
        {
            return new Vector3D(X*factor, Y*factor);
        }

        private static float DotProduct(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y*v2.Y;
        }

        private static Vector3D VectorSubtraction(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector3D ReflectionVector3D(Vector3D incident, Vector3D normal)
        {
            var incidentLenght = incident.Length;
            var normalizedNormal = normal.Normalize();
            var normalizedIncident = incident.Normalize();
            var returnValue = VectorSubtraction(normalizedIncident, normalizedNormal.VectorTimesFactor(2 * DotProduct(normalizedNormal, normalizedIncident)));
            return returnValue.VectorTimesFactor(incidentLenght);
        }
    }
}
