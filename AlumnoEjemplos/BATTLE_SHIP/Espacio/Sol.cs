using AlumnoEjemplos.BATTLE_SHIP.Elementos;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.BATTLE_SHIP.Espacio
{
    public class Sol : TgcMesh, IInteractivo
    {
        protected Vector3 sol_escala = new Vector3(12, 12, 12);
        const float AXIS_ROTATION_SPEED = 0.5f;
        float axisRotation = 0f;
        private Matrix pos;

        public ElementosManager ManagerTGC { get; set; }

        public Sol(string name, TgcMesh parentInstance, Vector3 translation, Vector3 rotation, Vector3 scale) : 
            base(name, parentInstance, translation, rotation, scale)
        {
            sol_escala = scale;
            enabled = true;
            this.AutoTransformEnable = false;
            pos = Matrix.Translation(translation);
        }
        
        public void Actualizar(float elapsedTime) 
        {
            axisRotation += AXIS_ROTATION_SPEED * elapsedTime;
            this.Transform = getSunTransform(elapsedTime);
        }

        private Matrix getSunTransform(float elapsedTime)
        {
            Matrix scale = Matrix.Scaling(sol_escala);
            Matrix yRot = Matrix.RotationY(axisRotation);

            return scale * yRot * pos;
        }
    }
}
