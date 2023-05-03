using CsvHelper.Configuration.Attributes;
using System;

namespace Proyecto_ED1.Models.clss
{
    public class Pacientes
    {
        [Name("Nombre")]
        public String Name { get; set; } 

        [Name("ID")]
        public String ID { get; set; }

        [Name("Edad")]
        public String age { get; set; }
        [Name("Ultima consulta")]
        public int last_consult { get; set; }
        [Name("Proxima consulta")]
        public int next_consult { get; set; }
        [Name("Descripcion")]
        public String description { get; set; }
        

        public Pacientes() { }
        public Pacientes(String name, String id, String age, int last, int next, String description)
        {
            this.Name = name;
            this.ID = id;
            this.age = age;
            this.last_consult = last;
            this.next_consult = next;
            this.description = description;
            
        }
    }
}
