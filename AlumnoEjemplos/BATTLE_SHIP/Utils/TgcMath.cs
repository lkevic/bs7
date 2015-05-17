using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.BATTLE_SHIP.Utils
{
    public static class TgcMath
    {
        public static float Distancia(Vector3 punto1, Vector3 punto2)
        {
            return (punto1 - punto2).Length();
        }

        public static float DistanciaXZ(Vector3 punto1, Vector3 punto2)
        {
            var vec = new Vector2(punto1.X - punto2.X, punto1.Z - punto2.Z);
            return vec.Length();
        }

    }
}
