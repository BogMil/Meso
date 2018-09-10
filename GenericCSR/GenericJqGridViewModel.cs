using System.Collections.Generic;

namespace GenericCSR
{
    public class GenericJqGridViewModel<TQueryDto> where TQueryDto:class
    {
        public List<TQueryDto> Records;
        public int CurrentPageNumber;
        public int TotalNumberOfPages;
        public int TotalNumberOfRecords;
    }
}