using Core.Services;

namespace Core.Interfaces;

public interface ICustomAuthenticationService
{
    string Autenticar( string UserName, string Password);
}