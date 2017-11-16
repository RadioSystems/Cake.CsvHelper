using System;
using System.IO;
using System.Text;
using Cake.Core.IO;
using Cake.CsvHelper.Tests.Fixtures;
using Cake.CsvHelper.Tests.Properties;
using Should;
using Xunit;
using Xunit.Sdk;

namespace Cake.CsvHelper.Tests {
    public sealed class CsvHelperTests {
        public sealed class TheReadCsvMethod {
            [Fact]
            public void Should_Throw_If_CsvFile_Is_Null() {
                // Given
                var fixture = new CsvHelpersFixture(false);
                
                // When 
                var result = Record.Exception(() => fixture.Read());
                
                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("csvFile");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null() {
                // Given
                var fixture = new CsvHelpersFixture();
                fixture.Settings = null;

                // When 
                var result = Record.Exception(() => fixture.Read());

                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("settings");
            }

            [Fact]
            public void Should_Throw_If_File_Does_Not_Exist() {
                // Given
                var fixture = new CsvHelpersFixture(false);
                fixture.CsvFilePath = "/Working/test.csv";

                // When 
                var result = Record.Exception(() => fixture.Read());

                // Then
                result.ShouldBeType<FileNotFoundException>();
            }
        }

        public sealed class TheWriteCsvNoMapMethod {
            [Fact]
            public void Should_Throw_If_CsvFile_Is_Null() {
                // Given
                var fixture = new CsvHelpersFixture();
                fixture.ResultPath = null;

                // When 
                var result = Record.Exception(() => fixture.WriteNoMapping());

                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("csvFile");
            }

            [Fact]
            public void Should_Throw_If_Records_Are_Null() {
                // Given
                var fixture = new CsvHelpersFixture(peopleExists: false);

                // When 
                var result = Record.Exception(() => fixture.WriteNoMapping());

                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("records");
            }
            
            [Fact]
            public void Should_Throw_If_Settings_Are_Null() {
                // Given
                var fixture = new CsvHelpersFixture();
                fixture.Settings = null;

                // When 
                var result = Record.Exception(() => fixture.WriteNoMapping());

                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("settings");
            }
            
            [Fact(Skip = "Experimental")]
            public void Should_Write_Records_To_CsvFile_With_No_Map() {
                // Given
                var fixture = new CsvHelpersFixture(false);
                
                // When
                fixture.WriteNoMapping();

                // Then
                var resultFile = fixture.FileSystem.GetFile(fixture.ResultPath);
                resultFile.Exists.ShouldEqual(true);
                string resultString;
                using(var resultStream = resultFile.OpenRead())
                using (var streamReader = new StreamReader(resultStream, Encoding.UTF8)) {
                    resultString = streamReader.ReadToEnd();
                    resultString.Trim().ShouldEqual(Resources.CsvHelper_CsvFile.Trim());
                }
            }
        }

        public sealed class TheWriteCsvWithMapMethod {
            [Fact]
            public void Should_Throw_If_CsvFile_Is_Null() {
                // Given
                var fixture = new CsvHelpersFixture(false);
                fixture.ResultPath = null;

                // When 
                var result = Record.Exception(() => fixture.WriteWithMapping());

                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("csvFile");
            }

            [Fact]
            public void Should_Throw_If_Records_Are_Null() {
                // Given
                var fixture = new CsvHelpersFixture(peopleExists: false);

                // When 
                var result = Record.Exception(() => fixture.WriteWithMapping());

                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("records");
            }

            [Fact]
            public void Should_Throw_If_ClassMap_Is_Null() {
                // Given
                var fixture = new CsvHelpersFixture();
                fixture.ClassMap = null;
                // When 
                var result = Record.Exception(() => fixture.WriteWithMapping());

                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("classMap");
            }

            [Fact]
            public void Should_Throw_If_Mapping_Is_Null() {
                // Given
                var fixture = new CsvHelpersFixture();
                fixture.DictionaryMap = null;
                // When 
                var result = Record.Exception(() => fixture.WriteWithMapping(true));

                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("mapping");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null() {
                // Given
                var fixture = new CsvHelpersFixture();
                fixture.Settings = null;

                // When 
                var result = Record.Exception(() => fixture.WriteWithMapping());

                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("settings");
            }

            [Fact(Skip = "Experimental")]
            public void Should_Write_Records_To_CsvFile_With_Class_Map() {
                // Given
                var fixture = new CsvHelpersFixture(false);

                // When
                fixture.WriteWithMapping();

                // Then
                var resultFile = fixture.FileSystem.GetFile(fixture.ResultPath);
                resultFile.Exists.ShouldEqual(true);
                string resultString;
                using (var resultStream = resultFile.OpenRead())
                using (var streamReader = new StreamReader(resultStream, Encoding.UTF8)) {
                    resultString = streamReader.ReadToEnd();
                }
                resultString.Trim().ShouldEqual(Resources.CsvHelper_MappedFile.Trim());
            }

            [Fact(Skip = "Experimental")]
            public void Should_Write_Records_To_CsvFile_With_Dictionary_Map() {
                // Given
                var fixture = new CsvHelpersFixture(false);

                // When
                fixture.WriteWithMapping();

                // Then
                var resultFile = fixture.FileSystem.GetFile(fixture.ResultPath);
                resultFile.Exists.ShouldEqual(true);
                string resultString;
                using (var resultStream = resultFile.OpenRead())
                using (var streamReader = new StreamReader(resultStream, Encoding.UTF8)) {
                    resultString = streamReader.ReadToEnd();
                }
                resultString.Trim().ShouldEqual(Resources.CsvHelper_MappedFile.Trim());
            }
        }
    }
}
