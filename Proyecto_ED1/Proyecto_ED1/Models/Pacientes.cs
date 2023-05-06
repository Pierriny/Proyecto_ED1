using CsvHelper.Configuration.Attributes;
using System;

namespace Proyecto_ED1.Models
{
    public class Pacientes
    {


        [Name("Nombre1")]
        public String Name1 { get; set; }
        [Name("Nombre2")]
        public String Name2 { get; set; }
        [Name("Apellido1")]
        public String LName1 { get; set; }
        [Name("Apellido2")]
        public String LName2 { get; set; }
        [Name("ID")]
        public long ID { get; set; }
        [Name("Edad")]
        public int age { get; set; }
        [Name("Telefono")]
        public int Phone { get; set; }
        [Name("Ultima consulta")]
        public int last_consult { get; set; }
        [Name("Proxima consulta")]
        public int next_consult { get; set; }
        [Name("Descripcion")]
        public String description { get; set; }
        [Name("Asistencia")]
        public String asistencia { get; set; }

        public Pacientes() { }
        public Pacientes(String name1, String name2, String Lname1, String Lname2, long id, int age, int phone, int last, int next, String description, String asistencia)
        {
            this.Name1 = name1;
            this.Name2 = name2;
            this.LName1 = Lname1;
            this.LName2 = Lname2;
            this.ID = id;
            this.age = age;
            this.Phone = phone;
            this.last_consult = last;
            this.next_consult = next;
            this.description = description;
            this.asistencia = asistencia;
        }
    }
}