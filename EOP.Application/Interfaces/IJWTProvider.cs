using EOP.Domain.Entities;

namespace EOP.Application.Interfaces
{
    public interface IJWTProvider
    {
        Task<string> Generate(User user);
    }
}
