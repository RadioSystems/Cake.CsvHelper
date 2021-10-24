﻿using System.Collections.Generic;
using System.Globalization;
using Cake.Core;
using Cake.Core.IO;
using Cake.CsvHelper.Tests.Properties;
using Cake.Testing;
using CsvHelper.Configuration;
using NSubstitute;

namespace Cake.CsvHelper.Tests.Fixtures {
    internal sealed class CsvHelpersFixture {
        public IFileSystem FileSystem { get; set; }
        public ICakeContext Context { get; set; }
        public FilePath CsvFilePath { get; set; }
        public FilePath ResultPath { get; set; }
        public CsvConfiguration Configuration { get; set; }
        public List<Person> People { get; set; }
        public Dictionary<string, string> DictionaryMap { get; set; }
        public ClassMap ClassMap { get; set; }

        public CsvHelpersFixture(bool csvFileExists = true, bool peopleExists = true) {
            Configuration = new CsvConfiguration(CultureInfo.InvariantCulture);


            ClassMap = new PersonMap();
            DictionaryMap = new Dictionary<string, string>() {
                {"Id", "EmployeeId" }, {"Name", "GivenName" }
            };

            var environment = FakeEnvironment.CreateUnixEnvironment();
            var fileSystem = new FakeFileSystem(environment);
            fileSystem.CreateDirectory("/Working");

            if (csvFileExists) {
                var content = Resources.CsvHelper_CsvFile;
                var csvFile = fileSystem.CreateFile("/Working/people.csv").SetContent(content);
                CsvFilePath = csvFile.Path;
            }

            ResultPath = "/Working/people.csv";

            if (peopleExists) {
                People = new List<Person> {
                    new Person { Id = 1, Name = "Jane" },
                    new Person {Id = 2, Name="Mal" }
                };
            }

            FileSystem = fileSystem;

            Context = Substitute.For<ICakeContext>();
            Context.FileSystem.Returns(FileSystem);
            Context.Environment.Returns(environment);
        }

        public void Read() {
            var csvHelper = new CsvHelpers(Context.FileSystem, Context.Environment);
            csvHelper.ReadRecords<Person>(CsvFilePath, null, Configuration);
        }

        public void WriteNoMapping() {
            var csvHelper = new CsvHelpers(Context.FileSystem, Context.Environment);
            csvHelper.WriteRecords(ResultPath, People, Configuration);
        }

        public void WriteWithMapping(bool useDictionaryMapping = false) {
            var csvHelper = new CsvHelpers(Context.FileSystem, Context.Environment);
            if (useDictionaryMapping) {
                csvHelper.WriteRecords(ResultPath, People, DictionaryMap, Configuration);
            }
            else {
                csvHelper.WriteRecords(ResultPath, People, ClassMap, Configuration);
            }
        }
    }

    public sealed class Person {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonMap : ClassMap<Person> {
        public PersonMap() {
            Map(m => m.Id).Name("EmployeeId");
            Map(m => m.Name).Name("GivenName");
        }
    }
}
