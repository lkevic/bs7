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
        private Matrix matrizDeEscala;
        private Matrix matrizDeRotacionDelMesh;
        #region Atributos

        protected float velocidad { get; set; }
        protected float velocidadRotacionY { get; set; }
        protected float velocidadRotacionX { get; set; }
        protected float velocidadRotacionZ { get; set; }

        protected float moverAdelante { get; set; }
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

        public Nave(string name, TgcMesh parentInstance, 
            Vector3 posicionInicial, Vector3 rotacionInicialSobreEjeY, 
            Vector3 escalaDelMesh, Vector3 rotacionDelMesh,
            float velocidadMaxima, float velocidadDeRotacion) : 
            base(name, parentInstance, posicionInicial, rotacionInicialSobreEjeY, escalaDelMesh)
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

            // Se crean las matriz de rotación para alinear el mesh a los ejes de coordenadas.
            matrizDeRotacionDelMesh = Matrix.RotationYawPitchRoll(rotacionDelMesh.Y, rotacionDelMesh.X, rotacionDelMesh.Z);
            // Se crean las matriz de rotación para escalar el mesh
            matrizDeEscala = Matrix.Scaling(scale);

            // Se rota la nave solo sobre el eje Y, ya que la nave no debe iniciar rotada sobre otro eje.
            rotateY(rotacionInicialSobreEjeY.Y);

            this.autoTransformEnable = false;
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
            moverAdelante = velocidad;
            moviendo = true;
        }

        public void Retroceder()
        {
            moverAdelante = -velocidad;
            moviendo = true;
        }

        public void GirarDerecha()
        {
            rotarY = velocidadRotacionY;
            rotandoY = true;
            rotarZ = -velocidadRotacionZ;
        }

        public void GirarIzquierda()
        {
            rotarY = -velocidadRotacionY;
            rotandoY = true;
            rotarZ = velocidadRotacionZ;
        }

        public void Arriba()
        {
            rotarX = -velocidadRotacionX;
            rotandoX = true;
        }

        public void Abajo()
        {
            rotarX = +velocidadRotacionX;
            rotandoX = true;
        }

        /// <summary>
        /// Aplicar transformaciones del mesh
        /// </summary>
        protected void updateMeshTransformNave()
        {
            //Aplicar transformacion de malla
            this.transform = matrizDeRotacionDelMesh
                * matrizDeEscala
                * Matrix.RotationYawPitchRoll(rotation.Y, rotation.X, rotation.Z)
                * Matrix.Translation(translation);
        }

        protected void ActualizarPosicion(float elapsedTime)
        {
            // En movimiento
            if (moviendo)
            {
                moveOrientedXYZ(moverAdelante * elapsedTime); 
            }

            // Rotacion lateral (izq o der)
            if (rotandoY)
            {
                float rotAngle = Geometry.DegreeToRadian(rotarY * elapsedTime);
                rotateY(rotAngle);

                if (rotation.Z <= anguloMaximoDeRotZ && rotation.Z >= anguloMinimoDeRotZ)
                {
                    rotateZ(Geometry.DegreeToRadian(rotarZ * elapsedTime));
                }
                else
                {
                    if (rotation.Z >= anguloMaximoDeRotZ)
                    {
                        rotation.Z = anguloMaximoDeRotZ;
                    }
                    else
                    {
                        rotation.Z = anguloMinimoDeRotZ;
                    }
                }
            }
            else
            {
                // Volver a colocar la nave paralela al plano XZ
                float acomodar = - rotation.Z;
                if (acomodar != 0f)
                {
                    if (acomodar > 0)
                    {
                        float correccion = Geometry.DegreeToRadian(velocidadRotacionZ * elapsedTime);
                        if (acomodar > correccion)
                            rotateZ(Geometry.DegreeToRadian(velocidadRotacionZ * elapsedTime));
                        else
                            rotation.Z = 0f;
                    }
                    else
                    {
                        float correccion = Geometry.DegreeToRadian(velocidadRotacionZ * elapsedTime);
                        if (acomodar < correccion)
                            rotateZ(Geometry.DegreeToRadian(-velocidadRotacionZ * elapsedTime));
                        else
                            rotation.Z = 0f;
                    }
                }
            }

            // Rotacion para arriba o para abajo
            if (rotandoX)
            {
                if (rotation.X <= anguloMaximoDeRotX && rotation.X >= anguloMinimoDeRotX)
                {
                    rotateX(Geometry.DegreeToRadian(rotarX * elapsedTime));
                }
                else 
                {
                    if (rotation.X >= anguloMaximoDeRotX)
                    {
                        rotation.X = anguloMaximoDeRotX;
                    }
                    else 
                    {
                        rotation.X = anguloMinimoDeRotX;
                    }
                }
            }
            else
            {
                // Volver a colocar la nave paralela al plano XZ
                float acomodar = 0f - rotation.X;
                if (acomodar != 0f)
                {
                    if(acomodar > 0)
                    {
                        float correccion = Geometry.DegreeToRadian(velocidadRotacionX * elapsedTime);
                        if(acomodar > correccion)
                            rotateX(Geometry.DegreeToRadian(velocidadRotacionX * elapsedTime));
                        else
                            rotation.X = 0f;
                    }
                    else
                    {
                        float correccion = Geometry.DegreeToRadian(velocidadRotacionX * elapsedTime);
                        if (acomodar < correccion)
                            rotateX(Geometry.DegreeToRadian(-velocidadRotacionX * elapsedTime));
                        else
                            rotation.X = 0f;
                    }
                }
            }

            updateBoundingBox();
            rotandoY = false;
            rotandoX = false;
            moviendo = false;
            updateMeshTransformNave();
        }

        protected void moveOrientedXYZ(float movement)
        {
            float z = FastMath.Cos(rotation.Y) * FastMath.Cos(-rotation.X) * movement;
            float x = FastMath.Sin(rotation.Y) * FastMath.Cos(-rotation.X) * movement;
            float y = FastMath.Sin(-rotation.X) * movement;

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
