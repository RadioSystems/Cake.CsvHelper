using Cake.Core;
using Cake.Core.IO;
using Cake.CsvHelper.Tests.Properties;
using Cake.CsvHelpers;
using Cake.Testing;
using NSubstitute;

namespace Cake.CsvHelper.Tests.Fixtures {
    internal sealed class CsvHelpersFixture {
        public IFileSystem FileSystem { get; set; }
        public ICakeContext Context { get; set; }
        public FilePath CsvFilePath { get; set; }
        public CsvHelperSettings Settings { get; set; }

        public CsvHelpersFixture(bool csvFileExists = true) {
            Settings = new CsvHelperSettings();

            var environment = FakeEnvironment.CreateUnixEnvironment();
            var fileSystem = new FakeFileSystem(environment);
            fileSystem.CreateDirectory("/Working");

            if (csvFileExists) {
                var content = Resources.CsvHelper_CsvFile;
                var csvFile = fileSystem.CreateFile("/Working/people.csv").SetContent(content);
                CsvFilePath = csvFile.Path;
            }

            FileSystem = fileSystem;

            Context = Substitute.For<ICakeContext>();
            Context.FileSystem.Returns(FileSystem);
            Context.Environment.Returns(environment);
        }

        public void Read() {
            var csvHelper = new CsvHelpers.CsvHelpers(Context.FileSystem, Context.Environment);
            csvHelper.ReadRecords<Person>(CsvFilePath, Settings);
        }
    }
}
