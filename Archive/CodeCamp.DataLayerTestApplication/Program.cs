using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeCamp.CoreClasses;
using CodeCamp.DataServerEF4;
using System.Data.Entity.Infrastructure;
using System.Configuration;
using CodeCamp.Utilities.MEF;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;

namespace DataLayerTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();

        }

        public void Run()
        {
            CompositionContainer _CompositionContainer = new CompositionContainer(new ConfigExportProvider());
            _CompositionContainer.ComposeParts(this);
            CodeCamp.DataServerInterface.IDataServer _DataServer = DataServer;// new DataServer();
            Person _Person = new Person();
            Sponsor _Sponsor = new Sponsor();
            _DataServer.Add<Sponsor>(_Sponsor);
            _DataServer.Add<Person>(_Person);
            _DataServer.Commit();
            IQueryable<Sponsor> _Sponsors = _DataServer.GetTable<Sponsor>();
            Console.WriteLine(_Sponsors.ToList().Count().ToString());
            //
            IQueryable<Person> _Persons = _DataServer.GetTable<Person>();
            Console.WriteLine(_Persons.ToList().Count().ToString());
            //modify a persom
            string _NewName = "Changed:" + DateTime.Now.ToString();
            _Persons.First().Name = _NewName;
            _DataServer.Commit();
            _Person = _DataServer.GetTable<Person>().Where(x => x.Name == _NewName).FirstOrDefault();
            if (_Person == null)
                throw new Exception("Person name not changed");
            Console.WriteLine(_Person.Name);
            Console.ReadLine();
        }

        [System.ComponentModel.Composition.Import]
        public CodeCamp.DataServerInterface.IDataServer DataServer { get; set; }
    }
}
