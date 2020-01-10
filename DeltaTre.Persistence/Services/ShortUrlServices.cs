using DeltaTre.Core;
using DeltaTre.Core.DomainModels;
using DeltaTre.Core.IHelpers;
using DeltaTre.Core.IServices;
using DeltaTre.Core.VM;
using System;
using System.Collections.Generic;

namespace DeltaTre.Persistence.Services
{
    public  class ShortUrlServices : IShortUrlServices
  {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilities _utilities;
        public ShortUrlServices(IUnitOfWork unitOfWork  , IUtilities utilities)
        {
            _unitOfWork = unitOfWork;
            _utilities = utilities;



        }

        public string SaveUrl(string originalUrl)
        {
            if (!_utilities.IsValidUri(originalUrl).Result)
            {
                return "Sorry the URL you entered is not live";
            }

            var supposedShortUrl = _utilities.GetRandomUrl();
            while (_unitOfWork.ShortUrlRepository.IsShortUrlExists(supposedShortUrl))
            {
                supposedShortUrl = _utilities.GetRandomUrl();
            }

            var shortUrl = new ShortUrl
            {
                OriginalUrl = originalUrl,
                ShortenUrl = supposedShortUrl,
                InsertDate = DateTime.Now.Date
            };
            _unitOfWork.Add(shortUrl);
            _unitOfWork.Complete();



            return "";
        }
        
   

        public List<ShortUrl> GetValidUrls(int numberOfValidMonths)
        {
            return _unitOfWork.ShortUrlRepository.GetValidUrls(3);
        }


        public ResponseResultVm GetTheResponseUrl(string shortenUrl , int numberOfMonthsValid)
        {
            var responseResult = new ResponseResultVm();


            var originalUrl = _unitOfWork.ShortUrlRepository.GetByShortKey(shortenUrl) ;

            if (originalUrl != null)
            {

                if (_utilities.MonthsDifferenceBetweenTwoDates(DateTime.Now.Date, originalUrl.InsertDate.Date) >
                    numberOfMonthsValid)
                {
                    responseResult.ErrorMessage = "Sorry the URL you entered is not valid anymore";
                    return responseResult;
                }
                   


                var responseUrl = _utilities.GetTheResponseFromTheRequestedUrl(originalUrl.OriginalUrl).Result;


                if (responseUrl == null)
                {
                    responseResult.ErrorMessage = $"Sorry the URL {originalUrl} is not live";
                    return responseResult;
                }

                responseResult.ResponseUrl = responseUrl;
                return responseResult;

            }

            responseResult.ErrorMessage = "The Url you entered is not exist";
            return responseResult;
        }

    

    }
}
