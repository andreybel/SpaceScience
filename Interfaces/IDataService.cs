using AskSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceScience.Interfaces
{
    public interface IDataService
    {
        Task<MarsDto> GetMarsDataByCameraType(string cameraType);
        Task<MarsDto> GetMarsDataByDate(string date);
        Task<ApodDto> GetNasaApod();
        Task<List<EpicDto>> GetNasaEpic();
    }
}
