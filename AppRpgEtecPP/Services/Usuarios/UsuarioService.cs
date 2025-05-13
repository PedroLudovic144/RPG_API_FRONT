﻿using AppRpgEtecPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppRpgEtecPP.Services.Usuarios
{
    public class UsuarioService: Request
    {
        private readonly Request _request;

        private const string apiUrlBase = "https://xyz.azurewebsites.net/Usuarios";

        // Ctrl + TAB: Atalho para criar um construtor
        public UsuarioService()
        {
            _request = new Request();
        }

        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Registrar";
            u.Id = await _request.PostReturnIntAsync(apiUrlBase + urlComplementar, u, string.Empty);
            return u;
        }

        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Autenticar";
            u = await _request.PostAsync(apiUrlBase + urlComplementar, u, string.Empty);
            return u;
        }
    }
}
