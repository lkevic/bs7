using AlumnoEjemplos.BATTLE_SHIP.Naves;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.BATTLE_SHIP.Usuario
{
    public class CamaraUsuario
    {
        private const float adelantar = 300f;
        private Nave objetivo;

        public CamaraUsuario(Nave naveASeguir)
        {
            objetivo = naveASeguir;
            //Posicion inicial de la camara
            GuiController.Instance.ThirdPersonCamera.Enable = true;
            GuiController.Instance.ThirdPersonCamera.setCamera(naveASeguir.Position, 200, -800);
        }

        public void Actualizar()
        {
            GuiController.Instance.ThirdPersonCamera.RotationY = objetivo.Rotation.Y;
            GuiController.Instance.ThirdPersonCamera.Target = CalcularPosition();
        }

        private Vector3 CalcularPosition()
        {
            float z = FastMath.Cos(objetivo.Rotation.Y) * adelantar;
            float x = FastMath.Sin(objetivo.Rotation.Y) * adelantar;

            return new Vector3(objetivo.Position.X + x, objetivo.Position.Y, objetivo.Position.Z + z);
        }

        public Vector3 GetPosition()
        {
            return GuiController.Instance.ThirdPersonCamera.Position;
        }
    }
}
