using System;
using System.Data;


namespace IDbEz.Implementations
{
    internal class ParameterStub : IParameterStub
    {
        public Byte Precision { get; set; }

        public Byte Scale { get; set; }

        public Int32 Size { get; set; }

        public DbType DbType { get; set; }

        public ParameterDirection Direction { get; set; }

        public Boolean IsNullable { get; private set; }

        public String ParameterName { get; set; }

        public String SourceColumn { get; set; }

        public DataRowVersion SourceVersion { get; set; }

        public Object Value { get; set; }
    }
}