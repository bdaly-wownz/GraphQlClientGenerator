using GraphQlClientGenerator;
namespace Console1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var schema = await GraphQlGenerator.RetrieveSchema(HttpMethod.Get, "https://localhost:7234/graphql");
            var config = new GraphQlGeneratorConfiguration
            {
                TargetNamespace = "SchemaToPocoPOC.Output",
                IncludeDeprecatedFields = true,
                CodeDocumentationType = CodeDocumentationType.DescriptionAttribute,
                GenerationOrder = GenerationOrder.DefinedBySchema,
                PropertyGeneration = PropertyGenerationOption.AutoProperty,
                InputObjectMode = InputObjectMode.Poco,
                JsonPropertyGeneration = JsonPropertyGenerationOption.Never,
                EnableNullableReferences = true,
                DataClassMemberNullability = DataClassMemberNullability.DefinedBySchema,
                GeneratePartialClasses = false,

            };
            var generator = new GraphQlGenerator(config);
            var generatedClasses = generator.GenerateFullClientCSharpFile(schema);
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                File.WriteAllText(System.IO.Path.Join("..\\..\\..\\", ".\\GeneratedClasses.cs"), generatedClasses);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
        }
    }
}