using System;

namespace DataLayer
{
    public class DL_Personal
    {
        public int ID_personal { get; set; }
        public string Nombre { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public int Edad { get; set; }
        public bool IsActive { get; set; }
    } 
}
