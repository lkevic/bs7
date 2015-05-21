using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.BATTLE_SHIP.Utils
{
    public static class TgcMath
    {
        public static float Distancia(Vector3 origen, Vector3 destino)
        {
            return (destino - origen).Length();
        }

        public static Vector3 VectorDistancia(Vector3 origen, Vector3 destino)
        {
            return (destino - origen);
        }

        public static float DistanciaXZ(Vector3 origen, Vector3 destino)
        {
            var vec = new Vector2(destino.X - origen.X, destino.Z - origen.Z);
            return vec.Length();
        }

        public static float DistanciaDeProyeccionSobreXZ(Vector3 vec)
        {
            var vecP = new Vector2(vec.X, vec.Z);
            return vecP.Length();
        }

    }
}
