using AlumnoEjemplos.BATTLE_SHIP.Naves;
using AlumnoEjemplos.BATTLE_SHIP.Utils;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.BATTLE_SHIP.Usuario.Panel
{
    class Radar
    {
        private TgcSprite radarBase;
        private int margen;
        private float radarEscala;

        private float escala;
        private float radio;
        private Vector2 centro;
        private float enemigoEscala;
        private TgcSprite enemigoTemplate;
        private List<TgcSprite> enemigosMostrados;

        private int contador;
        private Vector2 vec;
        private Vector2 vecEsc;
        private float dist;
        private Vector2 centroPuntoEnemigo;
        private TgcSprite naveUsuario;
        private float usuarioEscala;
        private DateTime ultimaActualizacion;
        private float tiempoRefrescoMiliseg;

        public Radar(float escalaDeDistancias) 
        {
            escala = escalaDeDistancias;
            //Crear Sprite del radar
            radarEscala = 0.8f;
            enemigoEscala = 0.08f;
            usuarioEscala = 0.2f;
            margen = 10;
            tiempoRefrescoMiliseg = 30f;

            ultimaActualizacion = DateTime.Now;

            enemigosMostrados = new List<TgcSprite>();

            //Base del radar
            radarBase = new TgcSprite();
            radarBase.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosMediaDir + "\\Panel\\Radar\\Radar256x256.png");
            radarBase.Scaling = new Vector2(radarEscala, radarEscala);

            //Calculo El radio
            radio = radarBase.Texture.Size.Width * radarEscala / 2;

            //Ubicarlo
            Size screenSize = GuiController.Instance.Panel3d.Size;
            Size textureSize = radarBase.Texture.Size;
            radarBase.Position = new Vector2(FastMath.Max(screenSize.Width - textureSize.Width * radarEscala - margen, 0), FastMath.Max(screenSize.Height - textureSize.Height * radarEscala - margen, 0));

            centro = new Vector2(radarBase.Position.X + radio, radarBase.Position.Y + radio);

            //Template de enemigo
            for (int i = 0; i < 100; i++)
            {
                enemigoTemplate = new TgcSprite();
                enemigoTemplate.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosMediaDir + "\\Panel\\Radar\\Enemigo64x64.png");
                enemigoTemplate.Scaling = new Vector2(enemigoEscala, enemigoEscala);
                enemigoTemplate.Enabled = false;
                enemigosMostrados.Add(enemigoTemplate);
            }
            centroPuntoEnemigo = new Vector2((enemigoTemplate.Texture.Width * enemigoEscala / 2),(enemigoTemplate.Texture.Width * enemigoEscala / 2));

            // Nave de Usuario
            naveUsuario = new TgcSprite();
            naveUsuario.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosMediaDir + "\\Panel\\Radar\\Usuario51x44.png");
            naveUsuario.Scaling = new Vector2(usuarioEscala, usuarioEscala);
            naveUsuario.Position = new Vector2(centro.X - naveUsuario.Texture.Width * usuarioEscala / 2, centro.Y - naveUsuario.Texture.Height * usuarioEscala / 2);
        }

        public void render(Nave usuario, List<Nave> enemigos) 
        {
            if ((DateTime.Now - ultimaActualizacion).TotalMilliseconds >= tiempoRefrescoMiliseg)
            {
                for (int i = 0; i < enemigosMostrados.Count(); i++)
                {
                    if (!enemigosMostrados[i].Enabled)
                        break;

                    enemigosMostrados[i].Enabled = false;
                }

                contador = 0;

                foreach (var enemigo in enemigos)
                {
                    vec = new Vector2(enemigo.Position.X - usuario.Position.X, -(enemigo.Position.Z - usuario.Position.Z));
                    vecEsc = new Vector2(vec.X * escala, vec.Y * escala);
                    dist = vec.Length();
                    if (dist * escala < radio)
                    {
                        enemigosMostrados[contador].Position = centro + vecEsc - centroPuntoEnemigo;
                        enemigosMostrados[contador].RotationCenter = centroPuntoEnemigo - vecEsc;
                        enemigosMostrados[contador].Rotation = -(usuario.Rotation.Y - usuario.RotInicial.Y);
                        enemigosMostrados[contador].Enabled = true;
                        contador++;
                    }
                }

                ultimaActualizacion = DateTime.Now;
            }

            radarBase.render();

            for (int i = 0; i < enemigosMostrados.Count(); i++)
            {
                if (!enemigosMostrados[i].Enabled)
                    break;

                enemigosMostrados[i].render();
            }

            naveUsuario.render();
        }
    }
}
