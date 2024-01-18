using System;
using System.IO;
using System.Text;
using Cake.Core.IO;
using Cake.CsvHelper.Tests.Fixtures;
using Cake.CsvHelper.Tests.Properties;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace Cake.CsvHelper.Tests
{
    public sealed class CsvHelperTests
    {
        public sealed class TheReadCsvMethod
        {
            [Fact]
            public void Should_Throw_If_CsvFile_Is_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture(false);

                // When
                var result = Record.Exception(() => fixture.Read());

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("csvFile");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture { Settings = null! };

                // When
                var result = Record.Exception(() => fixture.Read());

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("settings");
            }

            [Fact]
            public void Should_Throw_If_File_Does_Not_Exist()
            {
                // Given
                var fixture = new CsvHelpersFixture(false)
                {
                    CsvFilePath = "/Working/test.csv",
                };

                // When
                var result = Record.Exception(() => fixture.Read());

                // Then
                result.ShouldBeOfType<FileNotFoundException>();
            }
        }

        public sealed class TheWriteCsvNoMapMethod
        {
            [Fact]
            public void Should_Throw_If_CsvFile_Is_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture
                {
                    ResultPath = null!,
                };

                // When
                var result = Record.Exception(() => fixture.WriteNoMapping());

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("csvFile");
            }

            [Fact]
            public void Should_Throw_If_Records_Are_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture(peopleExists: false);

                // When
                var result = Record.Exception(() => fixture.WriteNoMapping());

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("records");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture
                {
                    Settings = null!,
                };

                // When
                var result = Record.Exception(() => fixture.WriteNoMapping());

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("settings");
            }

            [Fact(Skip = "Experimental")]
            public void Should_Write_Records_To_CsvFile_With_No_Map()
            {
                // Given
                var fixture = new CsvHelpersFixture(false);

                // When
                fixture.WriteNoMapping();

                // Then
                var resultFile = fixture.FileSystem.GetFile(fixture.ResultPath);
                resultFile.Exists.ShouldBeTrue();
                string resultString;
                using (var resultStream = resultFile.OpenRead())
                using (var streamReader = new StreamReader(resultStream, Encoding.UTF8))
                {
                    resultString = streamReader.ReadToEnd();
                    resultString.Trim().ShouldBe(Resources.CsvHelper_CsvFile.Trim());
                }
            }
        }

        public sealed class TheWriteCsvWithMapMethod
        {
            [Fact]
            public void Should_Throw_If_CsvFile_Is_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture(false)
                {
                    ResultPath = null!,
                };

                // When
                var result = Record.Exception(() => fixture.WriteWithMapping());

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("csvFile");
            }

            [Fact]
            public void Should_Throw_If_Records_Are_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture(peopleExists: false);

                // When
                var result = Record.Exception(() => fixture.WriteWithMapping());

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("records");
            }

            [Fact]
            public void Should_Throw_If_ClassMap_Is_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture
                {
                    ClassMap = null!,
                };

                // When
                var result = Record.Exception(() => fixture.WriteWithMapping());

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("classMap");
            }

            [Fact]
            public void Should_Throw_If_Mapping_Is_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture
                {
                    DictionaryMap = null!,
                };

                // When
                var result = Record.Exception(() => fixture.WriteWithMapping(true));

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("mapping");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new CsvHelpersFixture
                {
                    Settings = null!,
                };

                // When
                var result = Record.Exception(() => fixture.WriteWithMapping());

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("settings");
            }

            [Fact(Skip = "Experimental")]
            public void Should_Write_Records_To_CsvFile_With_Class_Map()
            {
                // Given
                var fixture = new CsvHelpersFixture(false);

                // When
                fixture.WriteWithMapping();

                // Then
                var resultFile = fixture.FileSystem.GetFile(fixture.ResultPath);
                resultFile.Exists.ShouldBeTrue();
                string resultString;
                using (var resultStream = resultFile.OpenRead())
                using (var streamReader = new StreamReader(resultStream, Encoding.UTF8))
                {
                    resultString = streamReader.ReadToEnd();
                }

                resultString.Trim().ShouldBe(Resources.CsvHelper_MappedFile.Trim());
            }
        }
    }
}
