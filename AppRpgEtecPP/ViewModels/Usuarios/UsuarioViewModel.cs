using AppRpgEtecPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRpgEtecPP.Services.Usuarios;


namespace AppRpgEtecPP.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService uService;
        private Usuario usuario;

        public ICommand AutenticarCommand { get; set; }

        public UsuarioViewModel()
        {
            uService = new UsuarioService();
            usuario = new Usuario();

            InicializarCommands();
        }

        private void InicializarCommands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
        }

        public string Login
        {
            get => usuario.Login;
            set
            {
                if (usuario.Login == value) return;
                usuario.Login = value;
                OnPropertyChanged();
            }
        }

        public string Senha
        {
            get => usuario.Senha;
            set
            {
                if (usuario.Senha == value) return;
                usuario.Senha = value;
                OnPropertyChanged();
            }
        }

        private async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = await uService.PostAutenticarUsuarioAsync(usuario);

                if (u != null && !string.IsNullOrEmpty(u.Login))
                {
                    Preferences.Set("UsuarioId", u.Id);
                    Preferences.Set("UsuarioLogin", u.Login);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Erro", ex.Message, "OK");
            }
        }
        #region Atributos/Propriedades
        // As propriedades serão chamadas na View futuramente

        private string login = string.Empty;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        private string senha = string.Empty;
        public string Senha
        {
            get => senha;
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }

        #endregion
        public async Task AutenticarUsuario() 
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;

                Usuario uAutenticado = await uService.PostAutenticarUsuarioAsync(u);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem-vindo(a) {uAutenticado.Username}.";

                    // Guardando dados do usuário para uso futuro
                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);
                    Preferences.Set("UsuarioToken", uAutenticado.Token);

                    await Application.Current.MainPage
                        .DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", "Dados incorretos :(", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

    }
}
