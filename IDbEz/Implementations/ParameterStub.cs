using System;
using System.Data;


namespace IDbEz.Implementations
{
    internal class ParameterStub : IParameterStub
    {
        private Byte _precision;
        public Byte Precision
        {
            get { return _precision; }
            set { _precision = value; PrecisionManuallySet = true; }
        }


        private Byte _scale;
        public Byte Scale
        {
            get { return _scale; }
            set { _scale = value; ScaleManuallySet = true; }
        }

        
        private Int32 _size;
        public Int32 Size
        {
            get { return _size; }
            set { _size = value; SizeManuallySet = true; }
        }

        
        private DbType _dbType;
        public DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; DbTypeManuallySet = true; }
        }


        private ParameterDirection _direction;
        public ParameterDirection Direction
        {
            get { return _direction; }
            set { _direction = value; DirectionManuallySet = true; }
        }


        public Boolean IsNullable { get; private set; }


        public String ParameterName { get; set; }


        private String _sourceColumn;
        public String SourceColumn
        {
            get { return _sourceColumn; }
            set { _sourceColumn = value; SourceColumnManuallySet = true; }
        }


        private DataRowVersion _sourceVersion;
        public DataRowVersion SourceVersion
        {
            get { return _sourceVersion; }
            set { _sourceVersion = value; SourceVersionManuallySet = true; }
        }


        public Object Value { get; set; }


        public Boolean PrecisionManuallySet { get; private set; }
        public Boolean ScaleManuallySet { get; private set; }
        public Boolean SizeManuallySet { get; private set; }
        public Boolean DbTypeManuallySet { get; private set; }
        public Boolean DirectionManuallySet { get; private set; }
        public Boolean IsNullableManuallySet { get; private set; }
        public Boolean ParameterNameManuallySet { get; private set; }
        public Boolean SourceColumnManuallySet { get; private set; }
        public Boolean SourceVersionManuallySet { get; private set; }
    }
}