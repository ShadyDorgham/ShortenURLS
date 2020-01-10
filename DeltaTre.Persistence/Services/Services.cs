using DeltaTre.Core;
using DeltaTre.Core.IHelpers;
using DeltaTre.Core.IServices;

namespace DeltaTre.Persistence.Services
{

    public class Services : IServices
    {
        public readonly IUnitOfWork UnitOfWork;
        public readonly IUtilities Utilities;
        public Services(IUnitOfWork unitOfWork, IUtilities utilities)
        {
            UnitOfWork = unitOfWork;
            Utilities = utilities;
            ShortUrlServices = new ShortUrlServices(UnitOfWork , Utilities);
        }

        public IShortUrlServices ShortUrlServices { get; private set; }




    }
}
