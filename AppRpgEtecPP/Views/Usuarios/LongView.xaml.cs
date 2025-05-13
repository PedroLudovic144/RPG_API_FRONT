namespace AppRpgEtecPP.Views.Usuarios;
using AppRpgEtecPP.ViewModels.Usuarios;

public partial class LongView : ContentPage
{
    UsuarioViewModel usuarioViewModel;

    public LoginView()
    {
        InitializeComponent();

        usuarioViewModel = new UsuarioViewModel();
        BindingContext = usuarioViewModel;
    }
}