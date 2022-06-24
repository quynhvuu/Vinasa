using System.IO;
using System.Threading.Tasks;

namespace Vinasa.Interfaces
{    
    /// <summary>
     /// Import manager interface
     /// </summary>
    public partial interface IImportManager
    {        
        /// <summary>
        /// Import data from XLSX file
        /// </summary>
        /// <param name="stream">Stream</param>
        Task ImportSeminarParticipantFromXlsx(int seminarId, Stream stream);
    }
}
