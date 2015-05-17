using AlumnoEjemplos.BATTLE_SHIP.Naves;
using AlumnoEjemplos.BATTLE_SHIP.Elementos;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.Input;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.BATTLE_SHIP.Usuario
{
    public class ControlUsuario
    {
        public static void CampurarInteraccionDeUsuario(TgcD3dInput d3dInput, TgcPickingRay pickingRay, ElementosManager manager)
        {

            //Adelante
            if (d3dInput.keyDown(Key.Space))
            {
                manager.NavePrincipal.Avanzar();
            }

            //Atras
            if (d3dInput.keyDown(Key.LeftShift))
            {
                manager.NavePrincipal.Retroceder();
            }

            //Abajo
            if (d3dInput.keyDown(Key.W))
            {
                manager.NavePrincipal.Abajo();
            }

            //Arriba
            if (d3dInput.keyDown(Key.S))
            {
                manager.NavePrincipal.Arriba();
            }

            //Derecha
            if (d3dInput.keyDown(Key.D))
            {
                manager.NavePrincipal.GirarDerecha();
            }

            //Izquierda
            if (d3dInput.keyDown(Key.A))
            {
                manager.NavePrincipal.GirarIzquierda();
            }

            //Disparar Laser
            if (d3dInput.buttonPressed(TgcViewer.Utils.Input.TgcD3dInput.MouseButtons.BUTTON_LEFT))
            {
                //Actualizar Ray de colisión en base a posición del mouse
                pickingRay.updateRay();


                //Testear Ray contra el AABB de todos los meshes
                foreach (Nave enemigo in manager.NavesEnemigas)
                {
                    TgcBoundingBox aabb = enemigo.BoundingBox;
                    Vector3 collisionPoint;
                    //Ejecutar test, si devuelve true se carga el punto de colision collisionPoint
                    var selected = TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, aabb, out collisionPoint);
                    if (selected)
                    {
                        manager.NavePrincipal.DispararLaser(enemigo, collisionPoint);
                        break;
                    }
                }
            }

            //Disparar Fazer
            if (d3dInput.buttonPressed(TgcViewer.Utils.Input.TgcD3dInput.MouseButtons.BUTTON_RIGHT))
            {
                //Actualizar Ray de colisión en base a posición del mouse
                pickingRay.updateRay();


                //Testear Ray contra el AABB de todos los meshes
                foreach (Nave enemigo in manager.NavesEnemigas)
                {
                    TgcBoundingBox aabb = enemigo.BoundingBox;
                    Vector3 collisionPoint;
                    //Ejecutar test, si devuelve true se carga el punto de colision collisionPoint
                    var selected = TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, aabb, out collisionPoint);
                    if (selected)
                    {
                        manager.NavePrincipal.DispararFazer(enemigo, collisionPoint);
                        break;
                    }
                }
            }
        }
    }
}
