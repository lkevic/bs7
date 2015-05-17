using AlumnoEjemplos.BATTLE_SHIP.Elementos;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.BATTLE_SHIP.Naves
{
    public class Nave : TgcMesh, IInteractivo
    {
        #region Atributos

        protected float velocidad { get; set; }
        protected float velocidadRotacionY { get; set; }
        protected float velocidadRotacionX { get; set; }
        protected float velocidadRotacionZ { get; set; }

        protected Vector3 rotInicial { get; set; }

        public Vector3 RotInicial { get { return rotInicial; } }
        
        protected float moverAtras { get; set; }
        protected bool moviendo { get; set; }

        protected float rotarY { get; set; }
        protected bool rotandoY { get; set; }
        protected float rotarX { get; set; }
        protected bool rotandoX { get; set; }
        protected float rotarZ { get; set; }

        protected float anguloMaximoDeRotX { get; set; }
        protected float anguloMinimoDeRotX { get; set; }
        protected float anguloMaximoDeRotZ { get; set; }
        protected float anguloMinimoDeRotZ { get; set; }

        protected int energia { get; set; }
        protected int potenciaDeLaser { get; set; }
        protected int potenciaDeFazer { get; set; }

        protected bool cargandoLaser { get; set; }
        protected bool dispararLaser { get; set; }
        protected Nave objetivoLaser { get; set; }

        protected bool cargandoFazer { get; set; }
        protected bool dispararFazer { get; set; }
        protected Nave objetivoFazer { get; set; }

        public ElementosManager ManagerTGC { get; set; }

        #endregion

        #region General

        public Nave(string name, TgcMesh parentInstance, Vector3 translation, Vector3 rotation, Vector3 scale,
            float velocidadMaxima, float velocidadDeRotacion) : 
            base(name, parentInstance, translation, rotation, scale)
        {
            enabled = true;
            velocidad = velocidadMaxima;
            velocidadRotacionY = velocidadDeRotacion;
            velocidadRotacionX = velocidadRotacionY * 0.7f;
            velocidadRotacionZ = velocidadRotacionY * 0.7f;

            anguloMaximoDeRotX = Geometry.DegreeToRadian( 80f);
            anguloMinimoDeRotX = Geometry.DegreeToRadian(-80f);
            anguloMaximoDeRotZ = Geometry.DegreeToRadian( 25f);
            anguloMinimoDeRotZ = Geometry.DegreeToRadian(-25f);

            energia = 2000;
            potenciaDeLaser = 200;
            potenciaDeFazer = 500;

            dispararLaser = false;
            cargandoLaser = false;

            dispararFazer = false;
            cargandoFazer = false;

            AutoUpdateBoundingBox = true;
            createBoundingBox();
            updateBoundingBox();

            rotInicial = new Vector3(rotation.X, rotation.Y, rotation.Z);
        }

        public void Actualizar(float elapsedTime)
        {
            ActualizarPosicion(elapsedTime);
            ActualizarDisparos(elapsedTime);
        }
        
        #endregion

        #region Movimiento

        public void Avanzar()
        {
            moverAtras = 0f;
            moverAtras = velocidad;
            moviendo = true;
        }

        public void Retroceder()
        {
            moverAtras = 0f;
            moverAtras = -velocidad;
            moviendo = true;
        }

        public void GirarDerecha()
        {
            rotarY = 0f;
            rotarY = velocidadRotacionY;
            rotandoY = true;
            rotarZ = velocidadRotacionZ;
        }

        public void GirarIzquierda()
        {
            rotarY = 0f;
            rotarY = -velocidadRotacionY;
            rotandoY = true;
            rotarZ = -velocidadRotacionZ;
        }

        public void Arriba()
        {
            rotarX = 0f;
            rotarX = velocidadRotacionX;
            rotandoX = true;
        }

        public void Abajo()
        {
            rotarX = 0f;
            rotarX = -velocidadRotacionX;
            rotandoX = true;
        }
        
        protected void ActualizarPosicion(float elapsedTime)
        {
            // En movimiento
            if (moviendo)
            {
                moveOrientedXY(moverAtras * elapsedTime); 
            }

            // Rotacion lateral (izq o der)
            if (rotandoY)
            {
                //Rotar personaje y la camara, hay que multiplicarlo por el tiempo transcurrido para no atarse a la velocidad el hardware
                float rotAngle = Geometry.DegreeToRadian(rotarY * elapsedTime);
                rotateY(rotAngle);

                if ((rotation.Z - rotInicial.Z) <= anguloMaximoDeRotZ && (rotation.Z - rotInicial.Z) >= anguloMinimoDeRotZ)
                {
                    //Rotar personaje y la camara, hay que multiplicarlo por el tiempo transcurrido para no atarse a la velocidad el hardware
                    rotateZ(Geometry.DegreeToRadian(rotarZ * elapsedTime));
                }
                else
                {
                    if ((rotation.Z - rotInicial.Z) >= anguloMaximoDeRotZ)
                    {
                        rotation.Z = anguloMaximoDeRotZ + rotInicial.Z;
                    }
                    else
                    {
                        rotation.Z = anguloMinimoDeRotZ + rotInicial.Z;
                    }
                }
            }
            else
            {
                // Volver a colocar la nave paralela al plano XZ
                float acomodar = rotInicial.Z - rotation.Z;
                if (acomodar != 0f)
                {
                    if (acomodar > 0)
                    {
                        float correccion = Geometry.DegreeToRadian(velocidadRotacionZ * elapsedTime);
                        if (acomodar > correccion)
                            rotateZ(Geometry.DegreeToRadian(velocidadRotacionZ * elapsedTime));
                        else
                            rotation.Z = rotInicial.Z;
                    }
                    else
                    {
                        float correccion = Geometry.DegreeToRadian(velocidadRotacionZ * elapsedTime);
                        if (acomodar < correccion)
                            rotateZ(Geometry.DegreeToRadian(-velocidadRotacionZ * elapsedTime));
                        else
                            rotation.Z = rotInicial.Z;
                    }
                }
            }

            // Rotacion para arriba o para abajo
            if (rotandoX)
            {
                if ((rotation.X - rotInicial.X) <= anguloMaximoDeRotX && (rotation.X - rotInicial.X) >= anguloMinimoDeRotX)
                {
                    //Rotar personaje y la camara, hay que multiplicarlo por el tiempo transcurrido para no atarse a la velocidad el hardware
                    rotateX(Geometry.DegreeToRadian(rotarX * elapsedTime));
                }
                else 
                {
                    if ((rotation.X - rotInicial.X) >= anguloMaximoDeRotX)
                    {
                        rotation.X = anguloMaximoDeRotX + rotInicial.X;
                    }
                    else 
                    {
                        rotation.X = anguloMinimoDeRotX + rotInicial.X;
                    }
                }
            }
            else
            {
                // Volver a colocar la nave paralela al plano XZ
                float acomodar = rotInicial.X - rotation.X;
                if (acomodar != 0f)
                {
                    if(acomodar > 0)
                    {
                        float correccion = Geometry.DegreeToRadian(velocidadRotacionX * elapsedTime);
                        if(acomodar > correccion)
                            rotateX(Geometry.DegreeToRadian(velocidadRotacionX * elapsedTime));
                        else
                            rotation.X = rotInicial.X;
                    }
                    else
                    {
                        float correccion = Geometry.DegreeToRadian(velocidadRotacionX * elapsedTime);
                        if (acomodar < correccion)
                            rotateX(Geometry.DegreeToRadian(-velocidadRotacionX * elapsedTime));
                        else
                            rotation.X = rotInicial.X;
                    }
                }
            }

            updateBoundingBox();
            rotandoY = false;
            rotandoX = false;
            moviendo = false;
        }

        public void moveOrientedXY(float movement)
        {
            float z = FastMath.Cos(rotation.Y - rotInicial.Y) * FastMath.Cos(rotation.X - rotInicial.X) * movement;
            float x = FastMath.Sin(rotation.Y - rotInicial.Y) * FastMath.Cos(rotation.X - rotInicial.X) * movement;
            float y = FastMath.Sin(rotation.X - rotInicial.X) * movement;

            move(x, y, z);
        }

        #endregion

        #region Disparo

        public void DispararLaser(Nave objetivo, Vector3 puntoDeColision)
        {
            if (!cargandoLaser && !dispararLaser)
            {
                dispararLaser = true;
                objetivoLaser = objetivo;
            }
        }

        public void DispararFazer(Nave objetivo, Vector3 puntoDeColision)
        {
            if (!cargandoFazer && !dispararFazer)
            {
                dispararFazer = true;
                objetivoFazer = objetivo;
            }
        }

        public void RecibirDisparo(int potencia) 
        {
            energia -= potencia;

            if (energia <= 0)
            {
                ManagerTGC.Remove(this);
                //enabled = false;
            }
        }

        protected void ActualizarDisparos(float elapsedTime)
        { 
            if(dispararLaser)
            {
                var nuevoLaser = new Laser(this, objetivoLaser, potenciaDeLaser);
                ManagerTGC.Add(nuevoLaser);
                dispararLaser = false;
                objetivoLaser = null;
            }

            if (dispararFazer)
            {
                var nuevoFazer = new Fazer(this, objetivoFazer, potenciaDeFazer);
                ManagerTGC.Add(nuevoFazer);
                dispararFazer = false;
                objetivoFazer = null;
            }
        }

        #endregion
    }
}
