using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LedgerCore.Data.Entities;
using LedgerCore.ViewModels;

namespace LedgerCore.Helpers
{
    /// <summary>
    /// Add all AutoMapper mapping configuration in this class
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<AccountDTO, Account>().ReverseMap();
            CreateMap<TransactionDTO, Transaction>().ReverseMap();
            Mapper.Initialize(cfg => cfg.AddProfile(this));
        }
    }
}
