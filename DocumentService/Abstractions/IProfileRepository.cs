using System.Collections.Generic;
using DocumentService.Models;

namespace DocumentService.Abstractions
{
    public interface IProfileRepository
    {
        IList<Profile> GetProfileForUser(int id);
    }
}
