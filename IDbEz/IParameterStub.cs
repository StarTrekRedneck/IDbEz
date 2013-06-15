using System.Data;
using System;


namespace IDbEz
{
    public interface IParameterStub : IDbDataParameter
    {
        Boolean PrecisionManuallySet { get; }
        Boolean ScaleManuallySet { get; }
        Boolean SizeManuallySet { get; }
        Boolean DbTypeManuallySet { get; }
        Boolean DirectionManuallySet { get; }
        Boolean SourceColumnManuallySet { get; }
        Boolean SourceVersionManuallySet { get; }
    }
}