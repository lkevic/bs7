using AlumnoEjemplos.BATTLE_SHIP.Naves;
using AlumnoEjemplos.BATTLE_SHIP.Utils;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.BATTLE_SHIP.IA
{
    public class PilotoIA
    {
        private Nave nave;
        private IAManager miJefe;
        private Nave objetivo;
        private DateTime timeStampDeOrdenDeAtaque;
        private int tiempoDePersecucion;
        private float radioDePersecucion;
        private float distanciaDeAtaque;
        private float distanciaAceptableAlObjetivo;
        private Vector3 vecDistanciaAlObjetivo;
        private double milisegDesdeInicioDeAtaque;
        private float toleranciaDeRotacion;

        public bool Activo { get { return (nave != null && nave.Enabled); } }

        public PilotoIA(Nave naveAPilotear, IAManager manager)
        {
            nave = naveAPilotear;
            miJefe = manager;
            tiempoDePersecucion = 30000; // 30 Segundos
            radioDePersecucion = 1500f;
            distanciaDeAtaque = 1000f;
            distanciaAceptableAlObjetivo = 500f;
            toleranciaDeRotacion = FastMath.PI / 16f;
            vecDistanciaAlObjetivo = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            milisegDesdeInicioDeAtaque = 0f;
        }

        public Vector3 Position
        {
            get { return nave.Position; }
        }

        internal void PerseguirYAtacar(Nave objetivoTemporal)
        {
            objetivo = objetivoTemporal;
            timeStampDeOrdenDeAtaque = DateTime.Now;
        }

        public void Actualizar(float elapsedTime)
        {
            if (objetivo != null) 
            { 
                vecDistanciaAlObjetivo = TgcMath.VectorDistancia(nave.Position, objetivo.Position);
                milisegDesdeInicioDeAtaque = (DateTime.Now - timeStampDeOrdenDeAtaque).TotalMilliseconds;

                if (milisegDesdeInicioDeAtaque > tiempoDePersecucion && vecDistanciaAlObjetivo.Length() > radioDePersecucion)
                {
                    miJefe.NotificarFinDeAtaque(this);
                    objetivo = null;
                }
                else
                {
                    IrA(objetivo.Position, vecDistanciaAlObjetivo, distanciaAceptableAlObjetivo);
                    DispararSiSePuede(objetivo, vecDistanciaAlObjetivo, distanciaDeAtaque);
                }
            }
        }

        private void DispararSiSePuede(Nave enemigo, Vector3 vecDistanciaAlObjetivo, float distanciaDeAtaque)
        {
            if(vecDistanciaAlObjetivo.Length() < distanciaDeAtaque)
            {
                nave.DispararFazer(enemigo, enemigo.Position);
            }
        }

        private void IrA(Vector3 vector3, Vector3 vecDistanciaAlObjetivo, float distanciaAceptableAlObjetivo)
        {
            if (vecDistanciaAlObjetivo.Length() > distanciaAceptableAlObjetivo) 
            {
                nave.Avanzar();
            }

            var anguloDeDiferencia = ((vecDistanciaAlObjetivo.X > 0f) ? 1f : -1f) *
                FastMath.Acos(vecDistanciaAlObjetivo.Z / TgcMath.DistanciaDeProyeccionSobreXZ(vecDistanciaAlObjetivo)) +
                ((vecDistanciaAlObjetivo.X > 0f) ? 0f : FastMath.TWO_PI) -
                (((nave.Rotation.Y < 0f) ? FastMath.TWO_PI : 0f) +
                ( nave.Rotation.Y % FastMath.TWO_PI));
            
            if (FastMath.Abs(anguloDeDiferencia) > toleranciaDeRotacion)
            {
                if ((anguloDeDiferencia > 0f && anguloDeDiferencia < FastMath.PI)
                    || (anguloDeDiferencia > -FastMath.PI && anguloDeDiferencia < -FastMath.TWO_PI))
                {
                    nave.GirarDerecha();
                }
                else
                {
                    nave.GirarIzquierda();
                }
            }
        }

    }
}
