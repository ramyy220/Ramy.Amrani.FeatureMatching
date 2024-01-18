using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObjectDetectionConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
           
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: dotnet run <path_to_object_image> <path_to_scenes_directory>");
                return;
            }

            string objectImagePath = args[0];
            string scenesDirectoryPath = args[1];

            // Vérifier que le chemin de l'image de l'objet existe
            if (!File.Exists(objectImagePath))
            {
                Console.WriteLine($"The object image path does not exist: {objectImagePath}");
                return;
            }

            // Vérifier que le répertoire des scènes existe
            if (!Directory.Exists(scenesDirectoryPath))
            {
                Console.WriteLine($"The scenes directory path does not exist: {scenesDirectoryPath}");
                return;
            }

            // Lire l'image de l'objet
            var objectImageData = await File.ReadAllBytesAsync(objectImagePath);
            var imageScenesData = new List<byte[]>();

            // Lire toutes les images de scène
            foreach (var imagePath in Directory.EnumerateFiles(scenesDirectoryPath))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                imageScenesData.Add(imageBytes);
            }

            // Supposons que ObjectDetection est une classe qui implémente la détection d'objet
            var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenesAsync(objectImageData, imageScenesData);

            // Imprimer les points détectés pour chaque résultat
            foreach (var objectDetectionResult in detectObjectInScenesResults)
            {
                Console.WriteLine($"Points: {JsonSerializer.Serialize(objectDetectionResult.Points)}");
            }
        }
    }

    // Vous devez définir cette classe pour effectuer la détection d'objets.
    // Elle doit implémenter la méthode DetectObjectInScenesAsync conformément à vos besoins.
    public class ObjectDetection
    {
        public async Task<IList<ObjectDetectionResult>> DetectObjectInScenesAsync(byte[] objectImageData, IList<byte[]> imagesSceneData)
        {
            // Implémentation de la détection d'objet
            throw new NotImplementedException();
        }
    }

    // La structure ObjectDetectionResult doit correspondre à celle attendue par votre implémentation.
    public record ObjectDetectionResult
    {
        public byte[] ImageData { get; set; }
        public IList<ObjectDetectionPoint> Points { get; set; }
    }

    public record ObjectDetectionPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
