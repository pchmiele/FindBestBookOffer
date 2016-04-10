using System;
using FindBestBookOffer.Model;

namespace FindBestBookOffer.Interfaces
{
    public interface ISearchResult
    {
        Price BookPrice { get; }

        Uri BookUri { get; }

        bool IsSuccessfull { get; }
    }
}